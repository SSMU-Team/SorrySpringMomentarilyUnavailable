; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Sorry Spring is Momentarily Unavailable"
#define MyAppVersion "1.0"
#define MyAppPublisher "SnowFlakes Game Studio"
#define MyAppURL "https://www.youtube.com/channel/UClC1_MVBa633e_wX3xdsXFQ"
#define MyAppExeName "SSMU.exe"
#define BuildFolder "..\Build\Windows\SSMU\"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{0CDC52D5-22F2-4FEC-B0DB-A86709997FF8}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
; AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
ArchitecturesInstallIn64BitMode=x64
DefaultDirName="{commonpf}\Sorry Spring is Momentarily Unavailable"
DisableProgramGroupPage=yes
LicenseFile=..\LICENCE.txt
InfoBeforeFile=BEFORE_INTALLATION.txt
InfoAfterFile=AFTER_INSTALLATION.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=Output\
OutputBaseFilename=Setup_SSMU
SetupIconFile=icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#BuildFolder}{#MyAppExeName}"; DestDir: "{app}";
Source: "{#BuildFolder}*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

