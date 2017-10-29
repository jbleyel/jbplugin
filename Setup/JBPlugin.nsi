;NSIS Modern User Interface
;Basic Example Script
;Written by Joost Verburg

;--------------------------------
;Include Modern UI

!include "MUI2.nsh"
!include "FileFunc.nsh"
 
;--------------------------------
;General
;Name and file

Name "JBPlugin"

RequestExecutionLevel admin

!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION 2.3.0
!define COMPANY "Joerg Bleyel"
!define URL http://www.myappsolut.de
  
;Request application privileges
RequestExecutionLevel admin

;--------------------------------
;Interface Settings

!define MUI_ABORTWARNING 

;--------------------------------
;Pages

Page custom CheckInstalled.NET

;!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE License.txt
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English
!insertmacro MUI_LANGUAGE German

OutFile "Setup.exe"
  
InstallDir "$PROGRAMFILES\JBplugin"
CRCCheck on
XPStyle on
ShowInstDetails hide
VIProductVersion 2.3.0.0
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductName JBPlugin
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyName "${COMPANY}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyWebsite "${URL}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileDescription "JBPluginSetup"
VIAddVersionKey /LANG=${LANG_ENGLISH} LegalCopyright "(c) 2017 by J.Bleyel"
InstallDirRegKey HKLM "${REGKEY}" Path
SilentUnInstall silent

Section -Main SEC0000
    SetOutPath $INSTDIR
    SetOverwrite on
    File u.reg
    File jbplugin.dll
    File ZeroconfService.dll
	#File ICSharpCode.SharpZipLib.dll
    File doimport.exe
    File exporter.exe
    SetOutPath $INSTDIR\de
    File de\jbplugin.resources.dll
    File de\exporter.resources.dll
    SetOutPath $INSTDIR
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\regasm.exe" /codebase /tlb /silent "$INSTDIR\jbplugin.dll"'
    WriteRegStr HKLM "${REGKEY}\Components" Main 1
SectionEnd


Section -post SEC0001
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o -un.Main UNSEC0000
	ExecWait 'regedit /s "$INSTDIR\u.reg"'
    Delete /REBOOTOK $INSTDIR\ZeroconfService.dll
    Delete /REBOOTOK $INSTDIR\System.Data.SQLite.dll
    Delete /REBOOTOK $INSTDIR\jbplugin.dll
    DeleteRegValue HKLM "${REGKEY}\Components" Main
SectionEnd

Section -un.post UNSEC0001
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK $INSTDIR\uninstall.exe
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"
    RmDir /REBOOTOK $INSTDIR
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro SELECT_UNSECTION Main ${UNSEC0000}
FunctionEnd

Function CheckInstalled.NET

	ReadRegStr $0 HKEY_LOCAL_MACHINE "Software\Microsoft\NET Framework Setup\NDP\v3.5" "Install"
	${If} $0 == 0
		MessageBox MB_OK|MB_ICONEXCLAMATION "Microsoft .NET Framework 3.5 is not installed! Setup will Quit!" IDOK End
	${EndIf}

	FindProcDLL::FindProc "dvdpro.exe"

	${If} $R0 == 1
		MessageBox MB_OK|MB_ICONEXCLAMATION "DVD Profiler is running. Please close the Application and restart the setup." IDOK End
	${EndIf}    
    
	ReadRegStr $0 HKEY_CURRENT_USER "Software\Invelos Software\DVD Profiler" "AppPath"
	${If} $0 == ""
		MessageBox MB_YESNO "DVD Profiler not found! This is no error! Continue Yes/No!" IDYES DVDPROA IDNO End
	${EndIf}

DVDPROA:

return

  end:
  quit
 
FunctionEnd
