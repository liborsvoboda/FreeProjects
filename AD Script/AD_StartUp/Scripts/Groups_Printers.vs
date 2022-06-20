'NESAHAT BEGIN
Dim objShell,objNetwork,clDrives,objApp
Set objShell = CreateObject("WScript.Shell")
Set objNetwork = CreateObject("WScript.Network") 
Set clDrives = objNetwork.EnumNetworkDrives
Set objApp = CreateObject("Shell.Application")


'Function check Member
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
'NESAHAT END





'NAPOVEDA
'NASTAVENI TISKARNY SKUPINE


IF IsMember("skupina") = True THEN
  ' objNetwork.AddWindowsPrinterConnection "\\pc\HP 3015dn-IT"  -  pripoji sitovou tiskarnu
  ' objNetwork.SetDefaultPrinter "\\pc\HP 3015dn-IT"  -  Nastavi tiskarnu jako Defaultni
END IF

