Imports Microsoft.VisualBasic
Imports System.Text
Public Class KeyGenerate
    Dim Key_Letters As String
    Dim Key_Numbers As String
    Dim Key_Chars As Integer
    Dim LettersArray As Char()
    Dim NumbersArray As Char()

    Public WriteOnly Property KeyLetters() As String
        Set(ByVal Value As String)
            Key_Letters = Value
        End Set
    End Property

    Public WriteOnly Property KeyNumbers() As String
        Set(ByVal Value As String)
            Key_Numbers = Value
        End Set
    End Property

    Public WriteOnly Property KeyChars() As Integer
        Set(ByVal Value As Integer)
            Key_Chars = Value
        End Set
    End Property
    Public Function Generate() As String
        Dim i_key As Integer
        Dim Random1 As Single
        Dim arrIndex As Int16
        Dim sb As New StringBuilder
        Dim RandomLetter As String

        LettersArray = Key_Letters.ToCharArray
        NumbersArray = Key_Numbers.ToCharArray

        For i_key = 1 To Key_Chars
            Randomize()
            Random1 = Rnd()
            arrIndex = -1
            If (CType(Random1 * 111, Integer)) Mod 2 = 0 Then
                Do While arrIndex < 0
                    arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * Random1)
                Loop
                RandomLetter = LettersArray(arrIndex)
                If (CType(arrIndex * Random1 * 99, Integer)) Mod 2 <> 0 Then
                    RandomLetter = LettersArray(arrIndex).ToString
                    RandomLetter = RandomLetter.ToUpper
                End If
                sb.Append(RandomLetter)
            Else
                Do While arrIndex < 0
                    arrIndex = _
                      Convert.ToInt16(NumbersArray.GetUpperBound(0) _
                      * Random1)
                Loop
                sb.Append(NumbersArray(arrIndex))
            End If
        Next
        Return sb.ToString
    End Function
End Class



'Dim KeyGen As RandomKeyGenerator
'Dim NumKeys As Integer
'Dim i_Keys As Integer
'Dim RandomKey As String

'''' MODIFY THIS TO GET MORE KEYS    - LAITH - 27/07/2005 22:48:30 -
'        NumKeys = 20

'        KeyGen = New RandomKeyGenerator
'        KeyGen.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
'        KeyGen.KeyNumbers = "0123456789"
'        KeyGen.KeyChars = 12
'        For i_Keys = 1 To NumKeys
'            RandomKey = KeyGen.Generate()
'            Console.WriteLine(RandomKey)
'        Next
'        Console.WriteLine("Press any key to exit...")
'        Console.Read()