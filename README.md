# MBR Parser for Visual Basic
A MBR (Master Boot Record) parser for Visual Basic .NET (vb.net).

## General Notes
- When initializing class, the MBR Sector must be sent as a `Byte() ` array and must be exactly 512 bytes in size.
- Some MBR values, like `Disk Serial Number`, `Active Partition Flag`, `File System ID`, and `Boot Signature`, can be used as `Byte` or `Byte()` arrays and as hexadecimal `String`s by adding `AsHexStr` at the end of each function name.
- To simply check if the MBR is bootable, use the special function `GetIsMBRBootable()`, which returns a `Boolean` value of True if it is bootable and can be used in conditional statements.
- In partition-related functions, the parameter `Partition_0to3` must be between 0 and 3 (0 = 1st partition, 1 = 2nd partition, 2 = 3rd partition, 3 = 4th partition), as the MBR supports up to 4 partitions.

## Installation
Just place this file (`MBRParser.vb`) into your Visual Basic .NET project.

## Example Usage
```
Dim sector(511) As Byte '511 = 0-511 = 512 bytes
Dim parser As MBRParser

Using fs As New IO.FileStream("C:\Users\user\Desktop\aa.bin", IO.FileMode.Open, IO.FileAccess.Read)
  fs.Read(sector, 0, 512) ' read 512 bytes starting from the beginning of the file into the array
End Using

parser = New MBRParser(sector) 'Now The parser is ready

'Examples of getting data from MBR
TextBox1.Text = parser.GetSerialNumberAsHexStr
MsgBox(parser.GetSignature) 'Get the signature as Byte array
MsgBox(parser.GetSignatureAsHexStr) 'Gets the signature as hex string

If parser.GetIsMBRBootable Then
  MsgBox("Is Bootable")
Else
  MsgBox("Not Bootable")
End If
```

## Functions List
Note: To use the functions you must first initialize the parser.
Example:
```
Dim parser As New MBRParser(sector)
```
where the sector is a byte array with an exact length of 512 bytes.


| Function    | Parameters      | Example               | Notes                         | Explaination                                |
|-------------|-----------------|-----------------------|-------------------------------|---------------------------------------------|
| `GetByteAt` | index As UInt16 | `parser.GetByteAt(4)` | index Parameter must be 0-511 | Gets the byte at position 5 (starts from 0) |

## Requirements
- .NET Framework 2.0 or newer.
