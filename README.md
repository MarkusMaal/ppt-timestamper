# PowerPoint timestamper
This tool allows you to recover slide creation timestamps from corrupted .PPT files. This is possible due to the way how in these files sections of data are stored in what Microsoft call "atoms". More information can be found in the `PowerPoint97-2007BinaryFileFormat(ppt)Specification.pdf` PDF document.

## Usage

Pass the corrupted PowerPoint 97-2003 file as an argument and the timestamp scanning process will begin, during which a growing list of timestamps are displayed.

The timestamps are displayed in yyyy-MM-DD HH:mm:ss format using the computer's local timezone. In brackets, a Unix epoch in seconds is also displayed.

Example: `./PptTimestamper corrupt.ppt`

Outputs:

```
Timestamp   1 : 2012-05-20 12:32:38Z [Epoch: 1337506358s]                                                                                                       
Timestamp   2 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   3 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   4 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   5 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   6 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   7 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]                                                                                                       
Timestamp   8 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp   9 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp  10 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]                                                                                                       
Timestamp  11 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp  12 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
Timestamp  13 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]                                                                                                       
Timestamp  14 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]                                                                                                       
Timestamp  15 : 2012-05-20 12:46:03Z [Epoch: 1337507163s]
```
