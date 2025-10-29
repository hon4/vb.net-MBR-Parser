Public Class MBRParser
    ' hon MBR Parser
    ' v1.0.0
    ' License: GPL-2.0

    Private MBRSector(511) As Byte

    ' Get the MBR
    Public Sub New(ByVal MBRData() As Byte)
        If MBRData Is Nothing OrElse Not MBRData.Length = 512 Then
            Throw New ArgumentException("MBR Sector Byte array must be exactly 512 bytes.")
        End If
        Array.Copy(MBRData, MBRSector, 512)
    End Sub

    'General Internal Use Functions
    Private Function ByteArray2HexString(ByVal ByteArray As Byte()) As String
        Dim ret As String = ""
        For Each b As Byte In ByteArray
            ret &= b.ToString("X2")
        Next
        Return ret
    End Function


    'Start Functions
    Public Function GetByteAt(ByVal index As UInt16) As Byte
        If index < 0 Or index >= 512 Then
            Throw New ArgumentOutOfRangeException("index")
        End If
        Return MBRSector(index)
    End Function

    'Get Disk SerialNumber
    Public Function GetSerialNumber() As Byte()
        Dim ret(3) As Byte
        Array.Copy(MBRSector, 440, ret, 0, 4)
        Return ret
    End Function

    Public Function GetSerialNumberAsHexStr() As String
        Return ByteArray2HexString(GetSerialNumber)
    End Function

    'Get Signature (boot Signature) (0xAA55 = Bootable)
    Public Function GetSignature() As Byte()
        Dim ret(1) As Byte
        Array.Copy(MBRSector, 510, ret, 0, 2)
        Return ret
    End Function

    Public Function GetSignatureAsHexStr() As String
        Return ByteArray2HexString(GetSignature)
    End Function

    Public Function GetIsMBRBootable() As Boolean
        Dim signature As Byte() = GetSignature()
        If signature(0) = &H55 AndAlso signature(1) = &HAA Then
            Return True
        Else
            Return False
        End If
    End Function

    'Get Active Partition Flag (0x80 = Active / 0x00 = Not Active)
    Public Function GetActivePartitionFlag(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(446 + (Partition_0to3 * 16))
    End Function

    Public Function GetActivePartitionFlagAsHexStr(ByVal Partition_0to3 As Byte) As String
        'Partition 0to3 will be checkd in GetPartitionFlag
        Return ByteArray2HexString({GetActivePartitionFlag(Partition_0to3)})
    End Function

    Public Function GetPartitionStartHead(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(447 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionStartSector_bits0to5_cylinder_bits6to7(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(448 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionStartCylinder_lower8bits(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(449 + (Partition_0to3 * 16))
    End Function

    'Known/The most common FileSystemIDs: 0x0E = FAT16 (With LBA), 0x0C = FAT32 (With LBA), 0x07 = NTFS
    'For More info of FileSystemIDs see: https://en.wikipedia.org/wiki/Partition_type
    Public Function GetPartitionFileSystemID(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(450 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionFileSystemIDAsHexStr(ByVal Partition_0to3 As Byte) As String
        'Partition 0to3 will be checkd in GetPartitionFileSystemID
        Return ByteArray2HexString({GetPartitionFileSystemID(Partition_0to3)})
    End Function

    Public Function GetPartitionEndHead(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(451 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionEndSector_bits0to5_cylinder_bits6to7(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(452 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionEndCylinder_lower8bits(ByVal Partition_0to3 As Byte) As Byte
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Return MBRSector(453 + (Partition_0to3 * 16))
    End Function

    Public Function GetPartitionFirstSector_LBA(ByVal Partition_0to3 As Byte) As UInt32
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Dim ret(3) As Byte
        Array.Copy(MBRSector, 454 + (Partition_0to3 * 16), ret, 0, 4)
        Return BitConverter.ToUInt32(ret, 0)
    End Function

    Public Function GetPartitionToalSectors(ByVal Partition_0to3 As Byte) As UInt32
        If Partition_0to3 > 3 Then
            Throw New ArgumentOutOfRangeException("Partition_0to3")
        End If
        Dim ret(3) As Byte
        Array.Copy(MBRSector, 458 + (Partition_0to3 * 16), ret, 0, 4)
        Return BitConverter.ToUInt32(ret, 0)
    End Function
End Class
