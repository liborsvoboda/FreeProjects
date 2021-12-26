Set wshShell = WScript.CreateObject( "WScript.Shell" )
ServerShare = "\\192.168.1.1\connect"
UserName = "trustore"
Password = "truestore"

Set NetworkObject = CreateObject("WScript.Network")
Set FSO = CreateObject("Scripting.FileSystemObject")

'NetworkObject.RemoveNetworkDrive ServerShare, True, False
On Error Resume Next
NetworkObject.MapNetworkDrive "", ServerShare, False ,UserName, Password
On Error Resume Next

Set Directory = FSO.GetFolder(ServerShare)
'For Each FileName In Directory.Files
'    WScript.Echo FileName.Name
 WshShell.Run "xcopy \\192.168.1.1\connect\TruStore.cli C:\SERVICES\xampp\htdocs\HotLine\temp\ /c /R /Y " , 0, true
'Next

Set FileName = Nothing
Set Directory = Nothing
Set FSO = Nothing

NetworkObject.RemoveNetworkDrive ServerShare, True, False
On Error Resume Next

Set ShellObject = Nothing
Set NetworkObject = Nothing



