On Error Resume Next 
Set objSysInfo = CreateObject("ADSystemInfo") 
 
' ########### This section connects to Active Directory as the currently logged on user 
 strUser = objSysInfo.UserName
 
Set objUser = GetObject("LDAP://" & strUser)  
 
' ########### This section sets up the variables we want to call in the script (items on the left; whereas the items on the right are the active directory database field names) - ie strVariablename = objuser.ad.databasename 
 
strGiven = objuser.givenName 
strSurname = objuser.sn  
strFax = objuser.facsimileTelephoneNumber  
strTitle = objUser.Title 
strDepartment = objUser.Department 
strCompany = objUser.Company 
strPhone = "+"+Mid(objUser.telephoneNumber, 3,3)+" "+Mid(objUser.telephoneNumber, 6,3)+" "+Mid(objUser.telephoneNumber, 9,3)+" "+Mid(objUser.telephoneNumber, 12,3) 
strMobile = "+"+Mid(objUser.mobile, 3,3)+" "+Mid(objUser.mobile, 6,3)+" "+Mid(objUser.mobile, 9,3)+" "+Mid(objUser.mobile, 12,3)
strEmail =objuser.mail 

strWeb = objuser.wWWHomePage 
'strWeb ="www.tvd.cz"

strNotes = objuser.info 
strExt = objuser.ipPhone 
strDDI = objuser.homephone 
strStreet = objUser.StreetAddress
strLocation = objUser.l
strPostCode = objUser.PostalCode
 
' ########### Sets up word template 
 
Set objWord = CreateObject("Word.Application") 
Set objDoc = objWord.Documents.Add() 
Set objSelection = objWord.Selection 
 
objSelection.Style = "No Spacing" 
Set objEmailOptions = objWord.EmailOptions 
Set objSignatureObject = objEmailOptions.EmailSignature 
Set objSignatureEntries = objSignatureObject.EmailSignatureEntries 
 
' ########### Calls the variables from above section and inserts into word template, also sets initial font typeface, colour etc. 
 
objSelection.Font.Name = "Trebuchet MS" 
objSelection.Font.Size = 11 
objselection.Font.Bold = True
objSelection.Font.Color = RGB (000,000,000) 
 
'objSelection.TypeParagraph()
objSelection.TypeText strGiven & " " & strSurname 
objSelection.Font.Color = RGB (229,009,127) 
objSelection.TypeText " | "

objSelection.Font.Size = 9 

objselection.Font.Bold = false
objSelection.Font.Color = RGB (000,000,000) 
objSelection.TypeText strTitle & CHR(11)

objselection.Font.Bold = True
objSelection.Font.Color = RGB (229,009,127) 
objSelection.Font.Size = 8 
objSelection.TypeText "________________________________________________________________________" & Chr(11) 
'objSelection.Font.Size = 9 
objSelection.TypeText strCompany
objSelection.Font.Color = RGB (229,009,127) 
objSelection.TypeText " | "
objSelection.Font.Color = RGB (000,000,000) 
objSelection.TypeText strStreet
objSelection.Font.Color = RGB (229,009,127) 
objSelection.TypeText " | "
objSelection.Font.Color = RGB (000,000,000) 
objSelection.TypeText strPostCode & " " & strLocation & " "

'Set objLink = objSelection.Hyperlinks.Add(objSelection.InlineShapes.AddPicture("\\pc\netlogon\AD_StartUp\Scripts\images\email_tvd_normal.jpg"))
objSelection.TypeText Chr(11) 


objSelection.Font.Size = 9
objselection.Font.Bold = false
objSelection.Font.Color = RGB (000,000,000) 
objSelection.TypeText "Tel.: " & strPhone 
objSelection.Font.Color = RGB (229,009,127) 
objSelection.TypeText " | "
objSelection.Font.Color = RGB (000,000,000) 
objSelection.TypeText "Mob.: " & strMobile
objSelection.Font.Color = RGB (255,000,255) 
objSelection.TypeText " | "
objSelection.Font.Color = RGB (000,000,000) 
'objSelection.TypeText strWeb & Chr(11) 


Set objLink = objSelection.Hyperlinks.Add(objSelection.Range, strWeb) 
  objLink.Range.Font.Name = "Trebuchet MS" 
  objLink.Range.Font.Size = 9 
  objLink.Range.Font.Bold = false 
  'objSelection.TypeParagraph()


'objSelection.TypeText Chr(11) 

'objselection.TypeText strEmailTEXT 
'Set objLink = objSelection.Hyperlinks.Add(objSelection.Range, "mailto: " & strEmail, , , strEmail) 
'objSelection.TypeParagraph() 
'Set objLink = objSelection.Hyperlinks.Add(objSelection.InlineShapes.AddPicture("\\pc\netlogon\AD_StartUp\Scripts\images\email_tvd.jpg"))

 
Set objSelection = objDoc.Range() 
objSignatureEntries.Add "Podpis TVD", objSelection 
objSignatureObject.NewMessageSignature = "Podpis TVD" 
objSignatureObject.ReplyMessageSignature = "Podpis TVD"
objDoc.Saved = True 
objWord.Quit