Dim objShell,objNetwork,clDrives,objApp,objFso 
Set objShell = CreateObject("WScript.Shell")
Set objNetwork = CreateObject("WScript.Network") 
Set clDrives = objNetwork.EnumNetworkDrives
Set objApp = CreateObject("Shell.Application")
Set wshShell = WScript.CreateObject( "WScript.Shell" )
Set objFso = CreateObject("Scripting.FileSystemObject")

Function IsMember(strGroup)
 
  Dim strGroupDN
  Dim objUser, objGroup
  Dim booIsMember

  Set objADSystemInfo = CreateObject("ADSystemInfo")
  Set objUser = GetObject("LDAP://" & objADSystemInfo.UserName)
  On Error Resume Next
  booIsMember = False
  For Each strGroupDN In objUser.GetEx("memberOf")
    Err.Clear
    Set objGroup = GetObject("LDAP://" & strGroupDN)
    If Err.Number = 0 Then
      If LCase(objGroup.Get("name")) = LCase(strGroup) Then
        booIsMember = True
        Exit For
      End If
    End If
    Set objGroup = Nothing
  Next
  On Error Goto 0
 
  IsMember = booIsMember
End Function


Function IsComputerMember(strGroup)
 
  Dim strGroupDN
  Dim objComputer, objGroup
  Dim booIsMember

  Set objADSystemInfo = CreateObject("ADSystemInfo")
  Set objComputer = GetObject("LDAP://" & objADSystemInfo.ComputerName)
 
  On Error Resume Next
  booIsMember = False

  For Each strGroupDN In objComputer.GetEx("memberOf")
    Err.Clear
    Set objGroup = GetObject("LDAP://" & strGroupDN)
    If Err.Number = 0 Then
      If LCase(objGroup.Get("name")) = LCase(strGroup) Then
        booIsMember = True
        Exit For
      End If
    End If
    Set objGroup = Nothing
  Next
  On Error Goto 0
  IsComputerMember = booIsMember
End Function
'NESAHAT END


'NAPOVEDA
' spusteni pro vsechny PC

	'odstraneni predchozi ulohy
	'WshShell.Run "schtasks /delete /tn AD_POLICY_%USERNAME% /f " , 0


WshShell.Run "\\pc\netlogon\AD_StartUp\Scripts\run\DesktopInfo\DesktopInfo.exe " , 0
WshShell.Run "%windir%\system32\gpupdate.exe" , 0




IF IsComputerMember("TEST_COMPUTER_GROUP") = True THEN

END IF


IF IsMember("TEST_USER_GROUP") = True THEN

END IF


IF objNetwork.UserName = "TEST_USERNAME" THEN 

END IF
