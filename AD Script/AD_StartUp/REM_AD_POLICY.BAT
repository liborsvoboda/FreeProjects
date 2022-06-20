echo off
REM regedit.exe /S \\pc\netlogon\AD_StartUp\Scripts\intranet.zone
schtasks /create /tn "AD_POLICY_%USERNAME%" /tr "%logonserver%\netlogon\AD_StartUp\Scripts\AD_Policy.vbs" /sc ONLOGON /f
schtasks /RUN /tn "AD_POLICY_%USERNAME%" 
