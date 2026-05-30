// See https://aka.ms/new-console-template for more information

using System.Globalization;

if (args.Length == 0) 
    Console.WriteLine("Please provide filename!");
var filename = args[0];

var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

var shiftRegister = "\0\0\0\0\0\0\0\0"u8.ToArray();
var oXmlDetector = "ypes].xm"u8.ToArray();

// SlideTimeAtom10 (12011)
// (source: PowerPoint97-2007BinaryFileFormat(ppt)Specification.pdf)
byte[] stampMagic = [0, 0, 0xEB, 0x2E, 0x08, 0, 0, 0];

var slides = 0;
var culture = CultureInfo.GetCultureInfo("en-US");
var lastPerc = -1.0;
while (fs.Position < fs.Length)
{
    var readByte = fs.ReadByte();
    shiftRegister = shiftRegister.Skip(1).Append((byte)readByte).ToArray();
    var match = true;
    if (fs.Position % 0x100 == 0)
    {
        var perc = Math.Round(fs.Position / (double)fs.Length * 100f);
        if (Math.Abs(perc - lastPerc) > 0.1)
        {
            Console.Write($"Scanning... ({perc.ToString(culture)}% complete)".PadRight(Console.WindowWidth, ' ') +
                          "\r");
        }
        lastPerc = perc;
    }

    foreach (var (i, b) in stampMagic.Index())
    {
        if (b != shiftRegister[i])
        {
            match = false;
            break;
        }
    }

    if (fs.Position == 0x30)
    {
        var bad = true;

        foreach (var (i, b) in oXmlDetector.Index())
        {
            if (b != shiftRegister[i])
            {
                bad = false;
                break;
            }
        }

        if (bad)
        {
            Console.WriteLine("OpenXML formats are not supported");
            Environment.Exit(1);
        }
    }

    if (match)
    {
        var fileTime = new byte[8];
        
        // ensures highDateTime and lowDateTime are read correctly according
        // to [MS-DTYP] specification
        fs.ReadExactly(fileTime, 4, 4);
        fs.ReadExactly(fileTime, 0, 4);
        try
        {
            var date = DateTimeOffset.FromFileTime(BitConverter.ToInt64(fileTime, 0));
            slides++;
            Console.WriteLine("Timestamp "+  slides.ToString().PadLeft(3, ' ') + " : " + date.ToString("yyyy-MM-dd HH:mm:ss") + "Z [Epoch: " + date.ToUnixTimeSeconds() + "s]");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Found invalid stamp @ 0x{fs.Position:X}");
        }
    }
}
Console.Write("".PadRight(Console.WindowWidth, ' ') + "\r");
if (slides == 0)
{
    Console.WriteLine("No slide created timestamps were found. Is this file a template?");
}