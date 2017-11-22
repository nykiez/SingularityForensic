<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<?xml-stylesheet type="text/xsl" href="is.xsl" ?>
<!DOCTYPE msi [
   <!ELEMENT msi   (summary,table*)>
   <!ATTLIST msi version    CDATA #REQUIRED>
   <!ATTLIST msi xmlns:dt   CDATA #IMPLIED
                 codepage   CDATA #IMPLIED
                 compression (MSZIP|LZX|none) "LZX">
   
   <!ELEMENT summary       (codepage?,title?,subject?,author?,keywords?,comments?,
                            template,lastauthor?,revnumber,lastprinted?,
                            createdtm?,lastsavedtm?,pagecount,wordcount,
                            charcount?,appname?,security?)>
                            
   <!ELEMENT codepage      (#PCDATA)>
   <!ELEMENT title         (#PCDATA)>
   <!ELEMENT subject       (#PCDATA)>
   <!ELEMENT author        (#PCDATA)>
   <!ELEMENT keywords      (#PCDATA)>
   <!ELEMENT comments      (#PCDATA)>
   <!ELEMENT template      (#PCDATA)>
   <!ELEMENT lastauthor    (#PCDATA)>
   <!ELEMENT revnumber     (#PCDATA)>
   <!ELEMENT lastprinted   (#PCDATA)>
   <!ELEMENT createdtm     (#PCDATA)>
   <!ELEMENT lastsavedtm   (#PCDATA)>
   <!ELEMENT pagecount     (#PCDATA)>
   <!ELEMENT wordcount     (#PCDATA)>
   <!ELEMENT charcount     (#PCDATA)>
   <!ELEMENT appname       (#PCDATA)>
   <!ELEMENT security      (#PCDATA)>                            
                                
   <!ELEMENT table         (col+,row*)>
   <!ATTLIST table
                name        CDATA #REQUIRED>

   <!ELEMENT col           (#PCDATA)>
   <!ATTLIST col
                 key       (yes|no) #IMPLIED
                 def       CDATA #IMPLIED>
                 
   <!ELEMENT row            (td+)>
   
   <!ELEMENT td             (#PCDATA)>
   <!ATTLIST td
                 href       CDATA #IMPLIED
                 dt:dt     (string|bin.base64) #IMPLIED
                 md5        CDATA #IMPLIED>
]>
<msi version="2.0" xmlns:dt="urn:schemas-microsoft-com:datatypes" codepage="65001">
	
	<summary>
		<codepage>1252</codepage>
		<title>Installation Database</title>
		<subject></subject>
		<author>##ID_STRING2##</author>
		<keywords>Installer,MSI,Database</keywords>
		<comments>Contact:  Your local administrator</comments>
		<template>Intel;1033</template>
		<lastauthor>Administrator</lastauthor>
		<revnumber>{D5A7A7B4-6848-4238-8912-8B853DD528D3}</revnumber>
		<lastprinted/>
		<createdtm>06/21/1999 21:00</createdtm>
		<lastsavedtm>07/15/2000 00:50</lastsavedtm>
		<pagecount>200</pagecount>
		<wordcount>0</wordcount>
		<charcount/>
		<appname>InstallShield Express</appname>
		<security>1</security>
	</summary>
	
	<table name="ActionText">
		<col key="yes" def="s72">Action</col>
		<col def="L64">Description</col>
		<col def="L128">Template</col>
		<row><td>Advertise</td><td>##IDS_ACTIONTEXT_Advertising##</td><td/></row>
		<row><td>AllocateRegistrySpace</td><td>##IDS_ACTIONTEXT_AllocatingRegistry##</td><td>##IDS_ACTIONTEXT_FreeSpace##</td></row>
		<row><td>AppSearch</td><td>##IDS_ACTIONTEXT_SearchInstalled##</td><td>##IDS_ACTIONTEXT_PropertySignature##</td></row>
		<row><td>BindImage</td><td>##IDS_ACTIONTEXT_BindingExes##</td><td>##IDS_ACTIONTEXT_File##</td></row>
		<row><td>CCPSearch</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td/></row>
		<row><td>CostFinalize</td><td>##IDS_ACTIONTEXT_ComputingSpace3##</td><td/></row>
		<row><td>CostInitialize</td><td>##IDS_ACTIONTEXT_ComputingSpace##</td><td/></row>
		<row><td>CreateFolders</td><td>##IDS_ACTIONTEXT_CreatingFolders##</td><td>##IDS_ACTIONTEXT_Folder##</td></row>
		<row><td>CreateShortcuts</td><td>##IDS_ACTIONTEXT_CreatingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut##</td></row>
		<row><td>DeleteServices</td><td>##IDS_ACTIONTEXT_DeletingServices##</td><td>##IDS_ACTIONTEXT_Service##</td></row>
		<row><td>DuplicateFiles</td><td>##IDS_ACTIONTEXT_CreatingDuplicate##</td><td>##IDS_ACTIONTEXT_FileDirectorySize##</td></row>
		<row><td>FileCost</td><td>##IDS_ACTIONTEXT_ComputingSpace2##</td><td/></row>
		<row><td>FindRelatedProducts</td><td>##IDS_ACTIONTEXT_SearchForRelated##</td><td>##IDS_ACTIONTEXT_FoundApp##</td></row>
		<row><td>GenerateScript</td><td>##IDS_ACTIONTEXT_GeneratingScript##</td><td>##IDS_ACTIONTEXT_1##</td></row>
		<row><td>ISLockPermissionsCost</td><td>##IDS_ACTIONTEXT_ISLockPermissionsCost##</td><td/></row>
		<row><td>ISLockPermissionsInstall</td><td>##IDS_ACTIONTEXT_ISLockPermissionsInstall##</td><td/></row>
		<row><td>InstallAdminPackage</td><td>##IDS_ACTIONTEXT_CopyingNetworkFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize##</td></row>
		<row><td>InstallFiles</td><td>##IDS_ACTIONTEXT_CopyingNewFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize2##</td></row>
		<row><td>InstallODBC</td><td>##IDS_ACTIONTEXT_InstallODBC##</td><td/></row>
		<row><td>InstallSFPCatalogFile</td><td>##IDS_ACTIONTEXT_InstallingSystemCatalog##</td><td>##IDS_ACTIONTEXT_FileDependencies##</td></row>
		<row><td>InstallServices</td><td>##IDS_ACTIONTEXT_InstallServices##</td><td>##IDS_ACTIONTEXT_Service2##</td></row>
		<row><td>InstallValidate</td><td>##IDS_ACTIONTEXT_Validating##</td><td/></row>
		<row><td>LaunchConditions</td><td>##IDS_ACTIONTEXT_EvaluateLaunchConditions##</td><td/></row>
		<row><td>MigrateFeatureStates</td><td>##IDS_ACTIONTEXT_MigratingFeatureStates##</td><td>##IDS_ACTIONTEXT_Application##</td></row>
		<row><td>MoveFiles</td><td>##IDS_ACTIONTEXT_MovingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize3##</td></row>
		<row><td>PatchFiles</td><td>##IDS_ACTIONTEXT_PatchingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize4##</td></row>
		<row><td>ProcessComponents</td><td>##IDS_ACTIONTEXT_UpdateComponentRegistration##</td><td/></row>
		<row><td>PublishComponents</td><td>##IDS_ACTIONTEXT_PublishingQualifiedComponents##</td><td>##IDS_ACTIONTEXT_ComponentIDQualifier##</td></row>
		<row><td>PublishFeatures</td><td>##IDS_ACTIONTEXT_PublishProductFeatures##</td><td>##IDS_ACTIONTEXT_FeatureColon##</td></row>
		<row><td>PublishProduct</td><td>##IDS_ACTIONTEXT_PublishProductInfo##</td><td/></row>
		<row><td>RMCCPSearch</td><td>##IDS_ACTIONTEXT_SearchingQualifyingProducts##</td><td/></row>
		<row><td>RegisterClassInfo</td><td>##IDS_ACTIONTEXT_RegisterClassServer##</td><td>##IDS_ACTIONTEXT_ClassId##</td></row>
		<row><td>RegisterComPlus</td><td>##IDS_ACTIONTEXT_RegisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppIdAppTypeRSN##</td></row>
		<row><td>RegisterExtensionInfo</td><td>##IDS_ACTIONTEXT_RegisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension2##</td></row>
		<row><td>RegisterFonts</td><td>##IDS_ACTIONTEXT_RegisterFonts##</td><td>##IDS_ACTIONTEXT_Font##</td></row>
		<row><td>RegisterMIMEInfo</td><td>##IDS_ACTIONTEXT_RegisterMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension##</td></row>
		<row><td>RegisterProduct</td><td>##IDS_ACTIONTEXT_RegisteringProduct##</td><td>##IDS_ACTIONTEXT_1b##</td></row>
		<row><td>RegisterProgIdInfo</td><td>##IDS_ACTIONTEXT_RegisteringProgIdentifiers##</td><td>##IDS_ACTIONTEXT_ProgID2##</td></row>
		<row><td>RegisterTypeLibraries</td><td>##IDS_ACTIONTEXT_RegisterTypeLibs##</td><td>##IDS_ACTIONTEXT_LibId##</td></row>
		<row><td>RegisterUser</td><td>##IDS_ACTIONTEXT_RegUser##</td><td>##IDS_ACTIONTEXT_1c##</td></row>
		<row><td>RemoveDuplicateFiles</td><td>##IDS_ACTIONTEXT_RemovingDuplicates##</td><td>##IDS_ACTIONTEXT_FileDir##</td></row>
		<row><td>RemoveEnvironmentStrings</td><td>##IDS_ACTIONTEXT_UpdateEnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction2##</td></row>
		<row><td>RemoveExistingProducts</td><td>##IDS_ACTIONTEXT_RemoveApps##</td><td>##IDS_ACTIONTEXT_AppCommandLine##</td></row>
		<row><td>RemoveFiles</td><td>##IDS_ACTIONTEXT_RemovingFiles##</td><td>##IDS_ACTIONTEXT_FileDir2##</td></row>
		<row><td>RemoveFolders</td><td>##IDS_ACTIONTEXT_RemovingFolders##</td><td>##IDS_ACTIONTEXT_Folder1##</td></row>
		<row><td>RemoveIniValues</td><td>##IDS_ACTIONTEXT_RemovingIni##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue##</td></row>
		<row><td>RemoveODBC</td><td>##IDS_ACTIONTEXT_RemovingODBC##</td><td/></row>
		<row><td>RemoveRegistryValues</td><td>##IDS_ACTIONTEXT_RemovingRegistry##</td><td>##IDS_ACTIONTEXT_KeyName##</td></row>
		<row><td>RemoveShortcuts</td><td>##IDS_ACTIONTEXT_RemovingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut1##</td></row>
		<row><td>Rollback</td><td>##IDS_ACTIONTEXT_RollingBack##</td><td>##IDS_ACTIONTEXT_1d##</td></row>
		<row><td>RollbackCleanup</td><td>##IDS_ACTIONTEXT_RemovingBackup##</td><td>##IDS_ACTIONTEXT_File2##</td></row>
		<row><td>SelfRegModules</td><td>##IDS_ACTIONTEXT_RegisteringModules##</td><td>##IDS_ACTIONTEXT_FileFolder##</td></row>
		<row><td>SelfUnregModules</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td>##IDS_ACTIONTEXT_FileFolder2##</td></row>
		<row><td>SetODBCFolders</td><td>##IDS_ACTIONTEXT_InitializeODBCDirs##</td><td/></row>
		<row><td>StartServices</td><td>##IDS_ACTIONTEXT_StartingServices##</td><td>##IDS_ACTIONTEXT_Service3##</td></row>
		<row><td>StopServices</td><td>##IDS_ACTIONTEXT_StoppingServices##</td><td>##IDS_ACTIONTEXT_Service4##</td></row>
		<row><td>UnmoveFiles</td><td>##IDS_ACTIONTEXT_RemovingMoved##</td><td>##IDS_ACTIONTEXT_FileDir3##</td></row>
		<row><td>UnpublishComponents</td><td>##IDS_ACTIONTEXT_UnpublishQualified##</td><td>##IDS_ACTIONTEXT_ComponentIdQualifier2##</td></row>
		<row><td>UnpublishFeatures</td><td>##IDS_ACTIONTEXT_UnpublishProductFeatures##</td><td>##IDS_ACTIONTEXT_Feature##</td></row>
		<row><td>UnpublishProduct</td><td>##IDS_ACTIONTEXT_UnpublishingProductInfo##</td><td/></row>
		<row><td>UnregisterClassInfo</td><td>##IDS_ACTIONTEXT_UnregisterClassServers##</td><td>##IDS_ACTIONTEXT_ClsID##</td></row>
		<row><td>UnregisterComPlus</td><td>##IDS_ACTIONTEXT_UnregisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppId##</td></row>
		<row><td>UnregisterExtensionInfo</td><td>##IDS_ACTIONTEXT_UnregisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension##</td></row>
		<row><td>UnregisterFonts</td><td>##IDS_ACTIONTEXT_UnregisteringFonts##</td><td>##IDS_ACTIONTEXT_Font2##</td></row>
		<row><td>UnregisterMIMEInfo</td><td>##IDS_ACTIONTEXT_UnregisteringMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension2##</td></row>
		<row><td>UnregisterProgIdInfo</td><td>##IDS_ACTIONTEXT_UnregisteringProgramIds##</td><td>##IDS_ACTIONTEXT_ProgID##</td></row>
		<row><td>UnregisterTypeLibraries</td><td>##IDS_ACTIONTEXT_UnregTypeLibs##</td><td>##IDS_ACTIONTEXT_Libid2##</td></row>
		<row><td>WriteEnvironmentStrings</td><td>##IDS_ACTIONTEXT_EnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction##</td></row>
		<row><td>WriteIniValues</td><td>##IDS_ACTIONTEXT_WritingINI##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue2##</td></row>
		<row><td>WriteRegistryValues</td><td>##IDS_ACTIONTEXT_WritingRegistry##</td><td>##IDS_ACTIONTEXT_KeyNameValue##</td></row>
	</table>

	<table name="AdminExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>InstallAdminPackage</td><td/><td>3900</td><td>InstallAdminPackage</td><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>4010</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdminUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AdminWelcome</td><td/><td>1010</td><td>AdminWelcome</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>50</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1020</td><td>SetupProgress</td><td/></row>
	</table>

	<table name="AdvtExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>4910</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdvtUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="AppId">
		<col key="yes" def="s38">AppId</col>
		<col def="S255">RemoteServerName</col>
		<col def="S255">LocalService</col>
		<col def="S255">ServiceParameters</col>
		<col def="S255">DllSurrogate</col>
		<col def="I2">ActivateAtStorage</col>
		<col def="I2">RunAsInteractiveUser</col>
	</table>

	<table name="AppSearch">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="s72">Signature_</col>
		<row><td>DOTNETVERSION45FULL</td><td>DotNet45Full</td></row>
	</table>

	<table name="BBControl">
		<col key="yes" def="s50">Billboard_</col>
		<col key="yes" def="s50">BBControl</col>
		<col def="s50">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L50">Text</col>
	</table>

	<table name="Billboard">
		<col key="yes" def="s50">Billboard</col>
		<col def="s38">Feature_</col>
		<col def="S50">Action</col>
		<col def="I2">Ordering</col>
	</table>

	<table name="Binary">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<row><td>ISExpHlp.dll</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\ISExpHlp.dll</td></row>
		<row><td>ISSELFREG.DLL</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\isregsvr.dll</td></row>
		<row><td>NewBinary1</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary10</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CompleteSetupIco.ibd</td></row>
		<row><td>NewBinary11</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary12</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DestIcon.ibd</td></row>
		<row><td>NewBinary13</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\NetworkInstall.ico</td></row>
		<row><td>NewBinary14</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DontInstall.ico</td></row>
		<row><td>NewBinary15</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Install.ico</td></row>
		<row><td>NewBinary16</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallFirstUse.ico</td></row>
		<row><td>NewBinary17</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallPartial.ico</td></row>
		<row><td>NewBinary18</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallStateMenu.ico</td></row>
		<row><td>NewBinary2</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\New.ibd</td></row>
		<row><td>NewBinary3</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Up.ibd</td></row>
		<row><td>NewBinary4</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\WarningIcon.ibd</td></row>
		<row><td>NewBinary5</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\welcome.jpg</td></row>
		<row><td>NewBinary6</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary7</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\ReinstIco.ibd</td></row>
		<row><td>NewBinary8</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\RemoveIco.ibd</td></row>
		<row><td>NewBinary9</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\SetupIcon.ibd</td></row>
		<row><td>SetAllUsers.dll</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\SetAllUsers.dll</td></row>
	</table>

	<table name="BindImage">
		<col key="yes" def="s72">File_</col>
		<col def="S255">Path</col>
	</table>

	<table name="CCPSearch">
		<col key="yes" def="s72">Signature_</col>
	</table>

	<table name="CheckBox">
		<col key="yes" def="s72">Property</col>
		<col def="S64">Value</col>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td></row>
		<row><td>LAUNCHPROGRAM</td><td>1</td></row>
		<row><td>LAUNCHREADME</td><td>1</td></row>
	</table>

	<table name="Class">
		<col key="yes" def="s38">CLSID</col>
		<col key="yes" def="s32">Context</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_Default</col>
		<col def="L255">Description</col>
		<col def="S38">AppId_</col>
		<col def="S255">FileTypeMask</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="S32">DefInprocHandler</col>
		<col def="S255">Argument</col>
		<col def="s38">Feature_</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="ComboBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="CompLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s38">ComponentId</col>
		<col def="I2">Type</col>
	</table>

	<table name="Complus">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="I2">ExpType</col>
	</table>

	<table name="Component">
		<col key="yes" def="s72">Component</col>
		<col def="S38">ComponentId</col>
		<col def="s72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">Condition</col>
		<col def="S72">KeyPath</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISScanAtBuildFile</col>
		<col def="S255">ISRegFileToMergeAtBuild</col>
		<col def="S0">ISDotNetInstallerArgsInstall</col>
		<col def="S0">ISDotNetInstallerArgsCommit</col>
		<col def="S0">ISDotNetInstallerArgsUninstall</col>
		<col def="S0">ISDotNetInstallerArgsRollback</col>
		<row><td>AdbWinApi.dll</td><td>{4621D4A3-7F71-4825-8F89-754AF75AAED1}</td><td>ADB</td><td>258</td><td/><td>adbwinapi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>AdbWinUsbApi.dll</td><td>{06A356DA-E4A1-4110-B49F-F66F24E22E3F}</td><td>ADB</td><td>258</td><td/><td>adbwinusbapi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>BouncyCastle.Crypto.dll2</td><td>{CC53A54E-58C4-42A8-9510-533AB4967CC5}</td><td>INSTALLDIR</td><td>258</td><td/><td>bouncycastle.crypto.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.PInvoke.dll</td><td>{C6D2DC55-98DE-4CBE-8A31-DF4B1E18D7F6}</td><td>COMMON</td><td>258</td><td/><td>cdfc.pinvoke.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Parse.Android.dll</td><td>{08015A66-9280-41E7-A7DB-55540870445F}</td><td>ENTITIES</td><td>258</td><td/><td>cdfc.parse.android.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Parse.Local.dll</td><td>{8C8931F3-3E03-44A2-A315-F2458C90EF72}</td><td>ENTITIES</td><td>258</td><td/><td>cdfc.parse.local.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Parse.Signature.dll</td><td>{93DBF566-52E1-4DD5-A6A4-87A932B73F54}</td><td>ENTITIES</td><td>258</td><td/><td>cdfc.parse.signature.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Parse.dll</td><td>{7963D0DB-BDBB-494E-9125-DF590480B295}</td><td>ENTITIES</td><td>258</td><td/><td>cdfc.parse.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Previewers.Contracts.dll</td><td>{645A9F6A-011D-4416-BA23-C9C904F76B49}</td><td>COMMON</td><td>258</td><td/><td>cdfc.previewers.contracts.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFC.Singularity.Forensics.dll</td><td>{3A72E6D3-8D70-420A-933A-95C54EA77116}</td><td>ENTITIES</td><td>258</td><td/><td>cdfc.singularity.forensics.d</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCControls.dll</td><td>{62C5972B-5760-4831-A693-60CDFD215552}</td><td>CONTROLS</td><td>258</td><td/><td>cdfccontrols.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCCultures.dll</td><td>{78E6120B-95DD-4CAA-B35B-C701B1FDCD3C}</td><td>COMMON</td><td>258</td><td/><td>cdfccultures.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCHexaEditor.dll</td><td>{14184CEC-05AC-4A0F-A0A5-ED3386F02EA6}</td><td>CONTROLS</td><td>258</td><td/><td>cdfchexaeditor.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCMessageBoxes.dll</td><td>{4A31B737-3D6D-4DEB-AC0F-7576552D0737}</td><td>CONTROLS</td><td>258</td><td/><td>cdfcmessageboxes.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCUIContracts.dll1</td><td>{85FC7260-CF9B-4A69-8A2D-9A4D9D1CDB04}</td><td>COMMON</td><td>258</td><td/><td>cdfcuicontracts.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>CDFCUIContracts.dll3</td><td>{47E4FE05-59C4-4D7E-87C6-AC4EDF7E25F2}</td><td>INSTALLDIR</td><td>258</td><td/><td>cdfcuicontracts.dll3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Cflab.DataTransport.dll2</td><td>{A89B32F7-6BBB-43C0-9DDA-4C7C176597B8}</td><td>INSTALLDIR</td><td>258</td><td/><td>cflab.datatransport.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>DirectOutIn.dll2</td><td>{4719E34A-7C7D-40B4-B163-BFAE7A7226D1}</td><td>INSTALLDIR</td><td>258</td><td/><td>directoutin.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>EventLogger.dll</td><td>{D454F6A4-69D8-4DFA-B976-18336C5A601E}</td><td>COMMON</td><td>258</td><td/><td>eventlogger.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td>{B0F5DA60-C6B4-4382-91D2-76EB135D6048}</td><td>ProgramFiles64Folder</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td>{B15BA45E-DFD8-4DD0-A7B6-1D27A7451C93}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT100</td><td>{6CFD52D8-FC3F-441B-9D69-90E2D7970DB6}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT101</td><td>{198D8E85-B8D3-4CD5-99E1-EF3F9511A39B}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT102</td><td>{12ED86B0-08A8-47CE-832F-A117BBEA2E90}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT103</td><td>{2BCADC66-F1B7-46C4-A7BB-FC2473C6A53C}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT104</td><td>{3F4F3738-EB86-4AF3-BDBB-D2A6E778635F}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT105</td><td>{18BA8620-9D14-4411-8220-7CF22973F840}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT106</td><td>{A277CADD-2E16-4271-B57B-1230B7C2B460}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT107</td><td>{FD0EB3E4-4F0A-43A2-8325-B856FC9FA3BC}</td><td>JFR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT108</td><td>{B5BAC4EF-F5B4-4588-B758-370F099526CD}</td><td>JFR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT109</td><td>{3D483C22-0CC9-4311-8434-0B0C08023B7B}</td><td>JFR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td>{29BC036A-02C0-418E-96E3-2346A154AA2B}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT110</td><td>{6ED36E57-0E6E-4481-B9F8-0CC6D620DDCA}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT111</td><td>{1055B324-7C0B-469B-9F8B-924F9D847BEA}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT112</td><td>{E46A57D5-40EE-4C8C-8591-1FA0F0E6FFFF}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT113</td><td>{B10CCCBB-4895-4947-AF20-B3399D541FBC}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT114</td><td>{FB729CE5-BACB-42BB-ACEF-589143CB9D95}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT115</td><td>{5EEF30FC-ED5A-46D4-957C-7B79CD96545C}</td><td>MANAGEMENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT116</td><td>{0E414BD2-0123-446A-B2BD-5017A1ABA897}</td><td>MANAGEMENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT117</td><td>{C400CEB8-21ED-4A91-A6F1-7BEFCA7ABABC}</td><td>MANAGEMENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT118</td><td>{09B47F23-75CB-4473-BA50-D72E068DD8D1}</td><td>MANAGEMENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT119</td><td>{FBAF0833-49ED-4652-B951-617A5FE81D1F}</td><td>MANAGEMENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT12</td><td>{77F80EF0-BB4A-4B04-AF4D-08A1E52B413E}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT120</td><td>{249364B3-39F0-4D5B-8309-4D0D2C88DCAF}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT121</td><td>{B06E9DDE-F8E4-4E0A-A1BE-43574F5226E2}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT122</td><td>{CF6ADDBF-7C54-4FDD-9559-A45882184A58}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT123</td><td>{9A7ABB7B-D571-4975-A1A5-360BA55B1779}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT124</td><td>{80A1DA09-6D19-47BF-A6B5-983CA594D35F}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT125</td><td>{58FAA455-44C0-47A5-8A6A-99DDDC382A24}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT126</td><td>{4471DA56-2BD2-45F8-B0DE-ACD8E8544A39}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT127</td><td>{DC0A17A3-5662-4CC2-A4BD-242F776E7F9E}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT128</td><td>{508CF11A-4481-4E44-929D-256F5087B043}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT129</td><td>{0512A334-CC9D-42A5-82FC-69627A634A30}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td>{DE0BD275-FC3A-4098-94BB-25E1C48DE8E0}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT130</td><td>{D5BD6402-AD5E-43E8-AA45-A7C1B56F3252}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT131</td><td>{DD1DB471-9C48-4765-9CEA-DD33DFBB7DA2}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT132</td><td>{17B4C3D5-0921-4D28-9356-2A39D83146AD}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT133</td><td>{8E12753A-D4B9-4EEF-B43C-F33B5EB524CD}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT134</td><td>{B83689A3-57E6-4592-AB18-A3864EC4F93B}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT135</td><td>{6803D955-6F0F-4FB8-B8D5-58AC25B026A6}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT136</td><td>{6E53BC01-0DF4-4913-9A94-6ED6F77FE3D7}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT137</td><td>{F2B78C99-C6FE-406F-B17C-37C4353E95E7}</td><td>SECURITY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT138</td><td>{2B6E2E84-A688-4368-A122-591557001A11}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT139</td><td>{53E2C69B-7C18-486A-B0EA-50A08AB7F7BF}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td>{C1E04DDB-3B9A-47C7-8775-427566265C2D}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT140</td><td>{76A5B911-589D-42E8-8A65-44D4B06C783D}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT141</td><td>{44E150B3-68FD-4F6C-9A39-71E3CAF9D0FC}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT142</td><td>{3FFCFDF2-725A-42E9-9376-AD3C76ED12BB}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT143</td><td>{D31BABF4-3811-4FF9-891C-3C0994D63C4C}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT144</td><td>{47E3C0D3-8041-47A9-8DC9-4A8180AAB03B}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT145</td><td>{ECFDC2B5-A382-4C0D-A46C-718274727325}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT146</td><td>{4F2C1256-F33F-4361-BD6E-3339F8CD2AB2}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT147</td><td>{2CDBC7FC-3525-4134-99B0-F7F1D0B65C02}</td><td>LANGUAGES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT148</td><td>{63A1EDD7-5DAC-4A17-AC69-C7E693503063}</td><td>EN_US</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT149</td><td>{E16EFC44-0ADB-423B-A03C-F250A2C59A49}</td><td>EN_US</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td>{8859918B-9932-4E86-A423-9A7C7CFEF623}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT150</td><td>{293AEA65-A363-4F94-82CE-AA6995FA1D5C}</td><td>EN_US</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT155</td><td>{D207EB1C-9507-452D-B847-C367CBA9B65E}</td><td>OUTSIDEIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT156</td><td>{9CEE13EF-7C5A-451D-AA7D-577872D05344}</td><td>OUTSIDEIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT157</td><td>{5AFFFD0D-3D7A-477E-9697-71CE3602002D}</td><td>OUTSIDEIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT158</td><td>{69DD95A9-D403-4F54-8351-74B98F23F614}</td><td>OUTSIDEIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT159</td><td>{B24725E7-45AE-4530-95E7-69AC58834737}</td><td>OUTSIDEIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td>{CC7BD9D6-7688-4D1B-9F49-7A5B92EB5084}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT163</td><td>{4A3C9158-82C5-42CF-9986-F515A88F6A39}</td><td>RESOURCES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT169</td><td>{17D75B9F-7556-4550-8938-98A8742A17F2}</td><td>TOOLS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td>{6561732A-6802-484C-BEFB-936E5264D0CE}</td><td>ENTITIES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT170</td><td>{8F105BE2-271A-4B63-A5C5-7796BBF1AE61}</td><td>ADB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT171</td><td>{74425CF2-22DF-4628-89AA-D73E6213B4D0}</td><td>APK</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT172</td><td>{A0839A54-D3A5-4EA5-A7DF-EC137B12A672}</td><td>APK</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT173</td><td>{52DC7438-5CA9-4656-842C-279648DE3CFA}</td><td>APK</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td>{34D54E99-521C-45C8-B35D-4634933DB3DF}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT183</td><td>{9F711B8A-EB06-487A-964D-DC4BCDDCD53F}</td><td>X64</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT184</td><td>{DAE323D7-20BD-4B7D-856D-DF9C5B7347E7}</td><td>X86</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT185</td><td>{3119F31D-BC08-4ABF-8582-93085F883C16}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT186</td><td>{EE9348D3-7B5F-4C0D-9B50-F322CE1E8D99}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT187</td><td>{9D2239A7-CBF6-4941-B4D9-542A2CCA6B2C}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT188</td><td>{EC474C05-4F05-4888-B4E8-8990E0BE14AB}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT189</td><td>{FCE6B5E4-D02C-4748-A9AD-4B8647A8DC42}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td>{789AF641-3525-4248-A3F6-E3E354AEFBEA}</td><td>BIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT190</td><td>{D17D2581-CFB9-4143-8F60-ACE5766E8289}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT191</td><td>{D8EF38AF-72C0-482E-AB0E-9F5D718F73BD}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT192</td><td>{B1212CB4-475D-49E3-92B9-BB081CD0250D}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT193</td><td>{B29C5154-0CE4-4DB6-912E-5129C40C54A7}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT194</td><td>{E34B3A77-51DB-4D6C-B432-87707BA0B790}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT195</td><td>{66E21F24-EDA0-4286-A6C5-ABF41BE4168B}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT196</td><td>{4274A6CA-184B-474A-BE23-B5FA5A0D1829}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT197</td><td>{C9D9CB01-FA20-48DE-8A22-4CF8C5FF896F}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT198</td><td>{7D9E5718-A015-4FB5-B34E-51A342809D36}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT199</td><td>{E1604827-CAE5-4C13-BF59-3F6FC04247D2}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td>{5D5D1FE0-B724-44F5-85CA-297C12B197AE}</td><td>CLIENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT200</td><td>{AA67E105-3E80-4620-B977-EA8C3F95BA99}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT201</td><td>{5941B7C3-BB97-48EC-A2EF-0C9F76060F69}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT202</td><td>{EA914F30-5438-45D7-95B0-6E7D7F026FD9}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT203</td><td>{D6798B2A-A311-485B-A279-5FF7137D50D8}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT204</td><td>{4DE33A0A-2A88-4B1E-98AB-F6EA74C1C92D}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT205</td><td>{B96CFF0A-1FA8-4E5C-9774-FACEC99324DE}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT206</td><td>{3570C0D6-905B-4158-9C8E-7059576FA37D}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT207</td><td>{38FD4F98-4055-4A0C-825B-494BFCDEBDD9}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td>{ACEF2D5F-3B7B-4EE2-ACD9-3CD674A83CD6}</td><td>CLIENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td>{CBFB1FA4-A3DA-4FF7-8497-6EB52ACCF965}</td><td>CLIENT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td>{FFE05AE3-5655-4C43-BB7D-78D69277A7BE}</td><td>BIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td>{3F7703B8-9081-417D-8B59-ABE283D14508}</td><td>DTPLUGIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td>{2A67DC85-BBDD-4FD2-A6CC-B7A85D4594BE}</td><td>FILE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td>{6602B8A0-4F66-4053-9017-879F7817122D}</td><td>WINRAR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td>{DBED78A3-E40C-40D8-8164-B7D02431E962}</td><td>BIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td>{00866FFA-70AA-48D1-B813-AC665EC9F73B}</td><td>PLUGIN2</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td>{26063F02-99F5-45FA-8B91-42DEFF323069}</td><td>BIN</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td>{B8C39FDB-174E-4832-9600-0B7588B352B4}</td><td>ATTACHMENTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td>{902C3DDB-42B2-4FC4-9963-54D843B187EF}</td><td>SERVER</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td>{CECBDB43-BFB3-4CAB-A9A2-6D6DD36121AF}</td><td>JRE</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td>{DF709408-8B13-409A-8177-0AD9E50F1FE8}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td>{CC7AF295-291D-435B-AFB6-777E941B0CD1}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT34</td><td>{1516D0BC-C196-4D47-AB8F-17A36DB82B07}</td><td>AMD64</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT35</td><td>{7FE145D8-C6BF-4462-BA68-AD8DDFA93235}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT36</td><td>{11BB9498-5BF5-4C48-8E7E-3D12EB9D8F19}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT37</td><td>{E5F9A147-E66A-4D31-802C-1538D436ED5F}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT38</td><td>{7074BF3B-CE14-4174-8A02-1B7169B8B615}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT39</td><td>{AA044931-92B2-4DC2-ADD8-332DEC843874}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT4</td><td>{6F74967B-5DEA-42E6-AF55-2CF99101EE8D}</td><td>ATTACHMENTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT40</td><td>{9E65ADE5-9525-4E56-9B77-66334ECBF732}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT41</td><td>{267B0679-5EF1-4828-B0BA-49245268453B}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT42</td><td>{16A9F7D3-F0DE-4FCC-8B9B-63251FEA6E5D}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT43</td><td>{9B5B5F49-703A-4D1E-9D7F-134C463F201E}</td><td>CMM</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT44</td><td>{B0C541DE-5519-465B-9063-F3835EF4DACF}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT45</td><td>{F0B5D814-ADA6-4D93-931C-45F45A3C8D01}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT46</td><td>{B7D8E0E7-ACF6-4696-B4B2-789E107A7D16}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT47</td><td>{1F2F3163-7014-4D4C-9A91-FEC5B4AB6028}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT48</td><td>{3249F17A-87B8-4165-AB80-FA51EC78B3AE}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT49</td><td>{A6147F2C-EB51-4B75-A53A-E657A9026F56}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td>{EF9EBABC-C201-4068-92E1-BE5B3C04653C}</td><td>ATTACHMENTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT50</td><td>{DF62A8B5-3AD3-4EBF-948C-D2782589961A}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT51</td><td>{26E37400-1B66-487A-A7FA-330CE58AFC9B}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT52</td><td>{1ED242C8-7BCE-4C38-84FB-808BE2CD5CF0}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT53</td><td>{AC3BC70B-824E-428C-99A8-7574F387DDF0}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT54</td><td>{75DB821F-F0CD-436D-A220-DADDFF0DFF24}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT55</td><td>{BF5B7779-15B0-47B0-80B3-038A32371FB0}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT56</td><td>{8FFDD71E-E77F-4C93-850B-A6A930DCF503}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT57</td><td>{0B1624F2-056B-4FD5-9467-3FA7C84B5ADC}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT58</td><td>{AE17245D-2CEF-4281-A254-4489D65DB9F5}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT59</td><td>{F0B9CBD3-9FFB-4CD7-90C2-898C3879F2AE}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT60</td><td>{9642B36D-871B-454F-9755-AF16963592F6}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT61</td><td>{1CDBDDF2-C084-4259-8801-3DC626F3BA43}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT62</td><td>{4EFA9BDB-3935-490B-B5FC-AF35CC616D14}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT63</td><td>{481C8BE6-C3A8-44B5-B056-95591AC390E2}</td><td>DEPLOY</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT64</td><td>{8EDC27A6-D1A2-4AB3-BB55-965604170B5E}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT65</td><td>{F7EE2B8D-35A2-4653-B585-32EA528D97DA}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT66</td><td>{84CFEF66-6716-450F-83A6-3C62CC4CAAD0}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT67</td><td>{CC41DE68-A97E-4F71-A813-0C169A547D98}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT68</td><td>{CDA1FA2E-C30B-4CF2-8DEC-97DC54F417B6}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT69</td><td>{8E677002-A0C4-4CAE-A6DF-29CD4694AA7A}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT70</td><td>{776D52C4-8342-42F2-9CFF-394C5FAC5634}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT71</td><td>{FB74AA30-98A9-41D8-92BD-113F7E7BBB15}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT72</td><td>{998CE903-EEBD-4F59-A6D4-8C8449A1128B}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT73</td><td>{E065B41B-62BE-49BB-B85C-123DB05756C1}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT74</td><td>{914C79E3-20C7-4CDF-BF4D-55B11DE9A8CC}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT75</td><td>{E54D974B-305A-4C93-B2F3-295449A15025}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT76</td><td>{640BD53D-02C7-4627-9921-F0F4AE227606}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT77</td><td>{CFA989FE-B754-4B66-AC01-3605276CB248}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT78</td><td>{1493AD40-AFE1-4B87-BA25-ADDA6EB92D55}</td><td>EXT</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT79</td><td>{41DFB5E1-B83E-4283-B0AD-7DBE3CDB28DB}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td>{8FE7970D-03E8-413C-B66B-0CE9CC280D77}</td><td>COMMON</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT80</td><td>{23EBEF0D-66B3-4D3E-A2DD-B8C02DD1F92A}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT81</td><td>{F0EDB72C-A53F-4BF3-94B6-1A3A184FE1BD}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT82</td><td>{4A0E14EE-D900-4444-9D43-7B23D6710870}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT83</td><td>{8A897D5C-0A05-493B-8EB2-DF7988C00CBE}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT84</td><td>{96255646-9146-46BE-91B5-1CD91A6EB38D}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT85</td><td>{8043C773-D997-4D21-8F4F-71000BEB66CD}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT86</td><td>{08CC2007-3D58-4245-9968-5BF29D3D636C}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT87</td><td>{8AE7161C-0DC7-4CAA-BEF0-B62467C280F1}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT88</td><td>{45A82737-D4D1-4307-898A-0AD398151D52}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT89</td><td>{E57247AE-765D-4669-B4DD-57D4D9691687}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td>{7F814204-0D13-488C-B8F2-322FEBB90CAA}</td><td>CONTROLS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT90</td><td>{B2949C80-09B1-43F5-87DF-FE208089FA86}</td><td>FONTS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT91</td><td>{6244FA23-DEB7-463C-A0C0-A8C6B81A8B88}</td><td>LIB</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT92</td><td>{496010B1-066C-474F-9E4A-E07474164B27}</td><td>I386</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT93</td><td>{F20594BB-FA59-43EA-8D09-A37917EF862A}</td><td>I386</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT94</td><td>{D65831E9-3F26-4679-B3AD-629ECCF0DDEE}</td><td>IMAGES</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT95</td><td>{00BE6931-6664-4226-BB6F-0AF97107E435}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT96</td><td>{6DFC98DE-3AB2-4BBC-9FED-69CAFFE3929F}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT97</td><td>{D790D639-C40C-4DF5-8119-A69DC03ED9C6}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT98</td><td>{67491D72-F214-4C21-B6B6-0CF1E1916F51}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT99</td><td>{1435E825-6F20-4417-A0A5-043FA5B267A4}</td><td>CURSORS</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>IS_ININSTALL_SHORTCUT</td><td>{9C22F9E8-604C-4888-BFE8-42AFA6849FCD}</td><td>INSTALLDIR</td><td>258</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>JAWTAccessBridge_32.dll</td><td>{766386DE-9FAB-4C5F-8986-B48D503B1D1A}</td><td>BIN</td><td>258</td><td/><td>jawtaccessbridge_32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>JavaAccessBridge_32.dll</td><td>{8AFB5B12-AA8D-49FF-AF7A-96231FE7C778}</td><td>BIN</td><td>258</td><td/><td>javaaccessbridge_32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>MahApps.Metro.dll1</td><td>{C44725E5-959A-4366-ACC0-18C54FD548D1}</td><td>RESOURCES</td><td>258</td><td/><td>mahapps.metro.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>MahApps.Metro.dll3</td><td>{79274EC7-AEF8-4840-96C0-525DEF2BEE3A}</td><td>INSTALLDIR</td><td>258</td><td/><td>mahapps.metro.dll3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Practices.ServiceLocation.dll2</td><td>{44228CEC-0645-4D2E-9409-993842E69995}</td><td>INSTALLDIR</td><td>258</td><td/><td>microsoft.practices.servicel4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Newtonsoft.Json.dll2</td><td>{62CC40DB-DEFB-4EE4-B718-62E59D3EAF0B}</td><td>INSTALLDIR</td><td>258</td><td/><td>newtonsoft.json.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Ookii.Dialogs.Wpf.dll</td><td>{9D3B0D79-CD6D-46FC-8694-1EF0869B2A1D}</td><td>CONTROLS</td><td>258</td><td/><td>ookii.dialogs.wpf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Prism.Mef.Wpf.dll2</td><td>{96103081-9FC6-4F59-A580-F9D35D3F0442}</td><td>INSTALLDIR</td><td>258</td><td/><td>prism.mef.wpf.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Prism.Wpf.dll2</td><td>{76414353-4C49-4AD5-A9EF-D5FA4A77815E}</td><td>INSTALLDIR</td><td>258</td><td/><td>prism.wpf.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Prism.dll2</td><td>{1A9A540F-7C90-47CA-B815-C8149520C835}</td><td>INSTALLDIR</td><td>258</td><td/><td>prism.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SQLite.Interop.dll</td><td>{626425A0-303C-486D-A155-A245C94DE393}</td><td>X64</td><td>258</td><td/><td>sqlite.interop.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SQLite.Interop.dll1</td><td>{71FF1A09-CE77-4B17-8F10-FDD2BB0C2CFB}</td><td>X86</td><td>258</td><td/><td>sqlite.interop.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.Fonts.dll</td><td>{6297FA0D-E511-4A6E-A0CA-1A254789DB87}</td><td>RESOURCES</td><td>258</td><td/><td>singularity.fonts.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.Previewers.dll</td><td>{FDB8E8E1-EAE1-4026-A784-27D93541997F}</td><td>CONTROLS</td><td>258</td><td/><td>singularity.previewers.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.AdbViewer.dll</td><td>{4BBE2436-F4B1-4B9C-8ACD-6A5346DA73E5}</td><td>CONTROLS</td><td>258</td><td/><td>singularity.ui.adbviewer.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.Controls.dll</td><td>{409AC66C-13D9-425F-B93D-53804A7A34A2}</td><td>CONTROLS</td><td>258</td><td/><td>singularity.ui.controls.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.Converters.dll</td><td>{D2B39C3F-10A9-4C76-ADC1-F12F822D5533}</td><td>CONTROLS</td><td>258</td><td/><td>singularity.ui.converters.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.Info.dll2</td><td>{378CECCC-1A4F-4E4F-B532-58BD7AB14F59}</td><td>INSTALLDIR</td><td>258</td><td/><td>singularity.ui.info.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.Infrastructure.dll</td><td>{CB91E3E8-9D05-4BB8-A859-2DAD3B03D2BC}</td><td>COMMON</td><td>258</td><td/><td>singularity.ui.infrastructur</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.MessageBoxes.dll</td><td>{D019ECDB-8AB7-4903-BE2C-DEB960253644}</td><td>CONTROLS</td><td>258</td><td/><td>singularity.ui.messageboxes.</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Singularity.UI.Themes.dll</td><td>{63F819C0-E729-4B98-934F-40DCDC463918}</td><td>RESOURCES</td><td>258</td><td/><td>singularity.ui.themes.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SingularityForensic.dll</td><td>{47DFEBC6-168F-4348-A7E4-951DA7263863}</td><td>ENTITIES</td><td>258</td><td/><td>singularityforensic.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SingularityShell.exe2</td><td>{3DB03845-2CA4-48B8-9078-DFAC4A076465}</td><td>INSTALLDIR</td><td>258</td><td/><td>singularityshell.exe2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SingularityShell.vshost.exe2</td><td>{A7CE12B7-913D-4E26-85B2-69AD1248171B}</td><td>INSTALLDIR</td><td>258</td><td/><td>singularityshell.vshost.exe2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Data.Sqlite.dll2</td><td>{639ADEBF-DC76-465A-8642-014E95FD9E24}</td><td>INSTALLDIR</td><td>258</td><td/><td>system.data.sqlite.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.dll2</td><td>{358055A1-ABD3-4D8C-A17A-81EA84673827}</td><td>INSTALLDIR</td><td>258</td><td/><td>system.windows.interactivity2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>UserMsgUs.dll</td><td>{B575371D-1E37-4AB5-9B7F-E8E7714EB517}</td><td>ENTITIES</td><td>258</td><td/><td>usermsgus.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>WindowsAccessBridge_32.dll</td><td>{0E64C803-3409-442E-8BE4-E8C1C859E071}</td><td>BIN</td><td>258</td><td/><td>windowsaccessbridge_32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>adb.exe</td><td>{DC608126-5D6A-4C8B-B2D0-C3770EE0DBBB}</td><td>ADB</td><td>258</td><td/><td>adb.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>awt.dll</td><td>{F1663D31-8DDF-449D-A0A9-D138658C81CB}</td><td>BIN</td><td>258</td><td/><td>awt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>bci.dll</td><td>{A0EA2CC6-C598-4C85-8AA3-67DADF99BC74}</td><td>BIN</td><td>258</td><td/><td>bci.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>cdfcphoto.dll</td><td>{A676BD42-65ED-4DFB-A52A-1903C5DC3371}</td><td>ENTITIES</td><td>258</td><td/><td>cdfcphoto.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>cdfcqd.dll1</td><td>{088F4CFF-8D49-42D4-B311-D07D2BF4C6E7}</td><td>ENTITIES</td><td>258</td><td/><td>cdfcqd.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>cdfcqd.dll3</td><td>{4DC083E9-7862-42A4-8A65-95CE8010867A}</td><td>INSTALLDIR</td><td>258</td><td/><td>cdfcqd.dll3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dcpr.dll</td><td>{95025DEA-877D-427A-8E77-7E1CEED7C2D2}</td><td>BIN</td><td>258</td><td/><td>dcpr.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>debmp.dll</td><td>{89D45893-6AED-4BF5-AD58-791104FDC9AA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>debmp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>decora_sse.dll</td><td>{86EBFDF5-1D40-43B3-ADED-E656E8AA0F1D}</td><td>BIN</td><td>258</td><td/><td>decora_sse.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dehex.dll</td><td>{5A4AE57D-5E21-4BDC-898D-74F49CD39549}</td><td>OUTSIDEIN</td><td>258</td><td/><td>dehex.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>deploy.dll</td><td>{C59147BA-838F-433B-8D7F-DA10C1B21AFA}</td><td>BIN</td><td>258</td><td/><td>deploy.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>deployJava1.dll</td><td>{4200AA5A-400C-4D40-B51D-EBEC7FE6FD50}</td><td>DTPLUGIN</td><td>258</td><td/><td>deployjava1.dll</td><td>20</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dess.dll</td><td>{C8E6C23A-3637-47CE-ABD2-C00AB0DFD486}</td><td>OUTSIDEIN</td><td>258</td><td/><td>dess.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>detree.dll</td><td>{1A56E472-B703-402D-BE8D-311ACA718B2D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>detree.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>devect.dll</td><td>{8BCD6056-5C93-4970-AC2B-3621622EEDF1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>devect.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dewp.dll</td><td>{3C5EE3BC-A283-4957-94AA-BD30A9D2ABD0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>dewp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dt_shmem.dll</td><td>{A92E463A-7963-4B0D-A3E6-94CB3469879C}</td><td>BIN</td><td>258</td><td/><td>dt_shmem.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>dt_socket.dll</td><td>{739671D2-2061-4BD5-A4C7-DA501A42504B}</td><td>BIN</td><td>258</td><td/><td>dt_socket.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>eula.dll</td><td>{CC02B5DF-3EF6-4538-9198-7984656570F3}</td><td>BIN</td><td>258</td><td/><td>eula.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>fontmanager.dll</td><td>{FF917C76-29A0-46F7-BD85-BAC3C7CF9783}</td><td>BIN</td><td>258</td><td/><td>fontmanager.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>fxplugins.dll</td><td>{DDAC2992-C757-4ABC-A03E-23B9C924293C}</td><td>BIN</td><td>258</td><td/><td>fxplugins.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>glass.dll</td><td>{8F02C1DF-132A-434D-93B0-76168F1D6BF3}</td><td>BIN</td><td>258</td><td/><td>glass.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>glib_lite.dll</td><td>{3C27E96D-3F8B-4F51-A687-DB82625573A3}</td><td>BIN</td><td>258</td><td/><td>glib_lite.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>gstreamer_lite.dll</td><td>{6B5CE061-227C-4A1A-B2B1-EABF795A481D}</td><td>BIN</td><td>258</td><td/><td>gstreamer_lite.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>hprof.dll</td><td>{B665C81B-B2C0-47CA-B4C6-06D5D264F56C}</td><td>BIN</td><td>258</td><td/><td>hprof.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibfpx2.dll</td><td>{D93B3221-911B-437E-9499-D3D7E2786C80}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibfpx2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibgp42.dll</td><td>{2737A106-A6D6-4A40-9633-FC0936260DFA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibgp42.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibjpg2.dll</td><td>{D90CB78B-DA17-4D84-B1AC-FCCAD5B99F29}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibjpg2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibpcd2.dll</td><td>{13AA983F-56B5-47CE-9739-4C9194A77845}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibpcd2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibpsd2.dll</td><td>{9D4F23C0-289F-4AB8-81EE-30DA22F76632}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibpsd2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibxbm2.dll</td><td>{EA98592C-3864-45C4-95BF-C1F1AD3615E7}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibxbm2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibxpm2.dll</td><td>{20342E6B-AD7B-4020-8184-298A262FCDFB}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibxpm2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ibxwd2.dll</td><td>{6652CCA3-6F63-4AC6-856E-438CCFB6024D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ibxwd2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd32.dll</td><td>{829A5FEF-C7F6-4FF4-958F-472DE8B557FD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd42.dll</td><td>{9F913280-0E4D-4BD2-8D3C-4D70D163FCA1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd42.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd52.dll</td><td>{3A7DB9C0-E83E-4DCA-9001-12C7B39B9AE1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd52.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd62.dll</td><td>{8933B690-44E2-4269-AB59-2536C6744913}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd62.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd72.dll</td><td>{F2921F08-14CE-452C-9B20-0ABACC584B26}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd72.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcd82.dll</td><td>{A0C6F3A8-5DB4-4B30-85D2-6E057746877D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcd82.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcdr2.dll</td><td>{26FAE952-4EF5-4260-949F-0B8131AF567B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcdr2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcm52.dll</td><td>{38465439-DA05-450B-9035-194234269D92}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcm52.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcm72.dll</td><td>{AF9C7BA8-B5B8-499C-974E-EE3EE5ABB291}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcm72.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imcmx2.dll</td><td>{39A2CB11-96D4-4177-9AAF-180DB0CECE86}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imcmx2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imdsf2.dll</td><td>{F9B9DBDF-95E5-42D2-99F1-CB9CD63EDABF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imdsf2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imfmv2.dll</td><td>{34EFD586-DE85-4670-8F74-5A57DEE9753E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imfmv2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imgdf2.dll</td><td>{B6F34889-4714-4F08-911B-593F26C37AEC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imgdf2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imgem2.dll</td><td>{47DE4FE0-CF35-4948-8F4F-445F0C291FA1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imgem2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imigs2.dll</td><td>{84E5F12C-185A-446C-8D5F-A86584A07BC9}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imigs2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>immet2.dll</td><td>{A486AA06-AD77-4B51-B446-84CC8DFEE863}</td><td>OUTSIDEIN</td><td>258</td><td/><td>immet2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>impif2.dll</td><td>{0CFB03C5-362E-4616-AF47-8D73977C8854}</td><td>OUTSIDEIN</td><td>258</td><td/><td>impif2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imps_2.dll</td><td>{F75D35FD-E3FC-46B5-B6CD-C7EAE0FBDA79}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imps_2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>impsi2.dll</td><td>{8F3B8D39-3924-42EE-BDCC-C739E9BBDCFB}</td><td>OUTSIDEIN</td><td>258</td><td/><td>impsi2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>impsz2.dll</td><td>{EB40B472-0A9E-4908-8877-32EA21E43E20}</td><td>OUTSIDEIN</td><td>258</td><td/><td>impsz2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>imrnd2.dll</td><td>{98C71B50-0002-4D9C-BE53-EB12980507A3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>imrnd2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>instrument.dll</td><td>{A4117AB0-84D7-48D6-AD57-088349A9F8DE}</td><td>BIN</td><td>258</td><td/><td>instrument.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>iphgw2.dll</td><td>{BDE2BD88-558D-409A-8288-2EB46EDE7589}</td><td>OUTSIDEIN</td><td>258</td><td/><td>iphgw2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>isgdi32.dll</td><td>{5805FE40-DDB8-4279-9109-666EE24F9CC6}</td><td>OUTSIDEIN</td><td>258</td><td/><td>isgdi32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>j2pcsc.dll</td><td>{73344FC2-136D-472E-99FD-2A90CE7E4967}</td><td>BIN</td><td>258</td><td/><td>j2pcsc.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>j2pkcs11.dll</td><td>{3A03A546-F992-48F6-A443-06019032E7DC}</td><td>BIN</td><td>258</td><td/><td>j2pkcs11.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jaas_nt.dll</td><td>{08E7A012-AA62-49EB-A210-79A281768E00}</td><td>BIN</td><td>258</td><td/><td>jaas_nt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jabswitch.exe</td><td>{86AE0C88-A319-4EAF-A796-0E04971E05A1}</td><td>BIN</td><td>258</td><td/><td>jabswitch.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>java.dll</td><td>{AEE563E8-7409-4906-A171-016F4A07F1E3}</td><td>BIN</td><td>258</td><td/><td>java.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>java.exe</td><td>{E5562C50-C17F-437F-B387-056A35ED8836}</td><td>BIN</td><td>258</td><td/><td>java.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>java_crw_demo.dll</td><td>{23E70A32-9211-45D9-A57E-AA2071BB97EE}</td><td>BIN</td><td>258</td><td/><td>java_crw_demo.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>java_rmi.exe</td><td>{60EB1C20-5EB2-460F-92E2-5E5689DAE9B3}</td><td>BIN</td><td>258</td><td/><td>java_rmi.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javacpl.exe</td><td>{5933D903-1195-41D1-A749-E566D0D51BF3}</td><td>BIN</td><td>258</td><td/><td>javacpl.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javafx_font.dll</td><td>{9426C1F4-E944-4DBA-8C70-21235F34BCF6}</td><td>BIN</td><td>258</td><td/><td>javafx_font.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javafx_font_t2k.dll</td><td>{2EA21887-20AF-4F45-9520-D0DB35D26F22}</td><td>BIN</td><td>258</td><td/><td>javafx_font_t2k.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javafx_iio.dll</td><td>{84FFEF6A-AC00-467A-98C5-5C05ABE4769E}</td><td>BIN</td><td>258</td><td/><td>javafx_iio.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javaw.exe</td><td>{ADC933C9-1006-4DD5-865D-28A6EAD79806}</td><td>BIN</td><td>258</td><td/><td>javaw.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>javaws.exe</td><td>{011D0AD5-63D4-49B8-8D0B-3586D167F53F}</td><td>BIN</td><td>258</td><td/><td>javaws.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jawt.dll</td><td>{9207B36C-AABA-4139-803E-611F795E6F1B}</td><td>BIN</td><td>258</td><td/><td>jawt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jdwp.dll</td><td>{114C3858-737A-41A8-BAC9-B925B9305A42}</td><td>BIN</td><td>258</td><td/><td>jdwp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jfr.dll</td><td>{C3E96E69-23F5-48FF-B9A9-5CD472B9008B}</td><td>BIN</td><td>258</td><td/><td>jfr.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jfxmedia.dll</td><td>{A27004C5-9E8E-4AD0-A80A-B00220987BD5}</td><td>BIN</td><td>258</td><td/><td>jfxmedia.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jfxwebkit.dll</td><td>{31CAE922-DF4E-4B54-BAC1-B00AD92B8979}</td><td>BIN</td><td>258</td><td/><td>jfxwebkit.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jjs.exe</td><td>{E387B98D-B25C-49B4-98DE-37EBE768D4B7}</td><td>BIN</td><td>258</td><td/><td>jjs.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jli.dll</td><td>{44BA1A7B-1380-4A90-88B1-D18000E21603}</td><td>BIN</td><td>258</td><td/><td>jli.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jp2iexp.dll</td><td>{98785F38-6AF8-49D1-ABA7-DEB696ECDDC1}</td><td>BIN</td><td>258</td><td/><td>jp2iexp.dll</td><td>20</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jp2launcher.exe</td><td>{ADD78491-FEE7-4129-8464-E7FB91F92C2C}</td><td>BIN</td><td>258</td><td/><td>jp2launcher.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jp2native.dll</td><td>{AA08BE24-3C63-4241-8807-22FEE7821AD8}</td><td>BIN</td><td>258</td><td/><td>jp2native.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jp2ssv.dll</td><td>{711B1D4B-79D5-44CE-973F-2C5A369BF88E}</td><td>BIN</td><td>258</td><td/><td>jp2ssv.dll</td><td>20</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jpeg.dll</td><td>{6726BC29-D7B7-43D5-AF1E-36F93DC2404E}</td><td>BIN</td><td>258</td><td/><td>jpeg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jsdt.dll</td><td>{3AA09C7F-D5D1-4DF1-AD7F-4CA6B0034313}</td><td>BIN</td><td>258</td><td/><td>jsdt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jsound.dll</td><td>{1F4A8F69-643C-4761-BAD1-AB664825A9DA}</td><td>BIN</td><td>258</td><td/><td>jsound.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jsoundds.dll</td><td>{50737DCD-B7EA-40E3-93A7-3CCA1E24E2C5}</td><td>BIN</td><td>258</td><td/><td>jsoundds.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>jvm.dll</td><td>{926F26F3-24EC-4873-90D6-CC9AB7BD488B}</td><td>CLIENT</td><td>258</td><td/><td>jvm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>kcms.dll</td><td>{2BB5907C-BB2B-4195-A550-A653069993D7}</td><td>BIN</td><td>258</td><td/><td>kcms.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>keytool.exe</td><td>{45FB9F84-6867-47B6-97A6-C0E65F77F1E2}</td><td>BIN</td><td>258</td><td/><td>keytool.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>kinit.exe</td><td>{02836002-F111-4BEB-81DF-8BA8B5C9DFA3}</td><td>BIN</td><td>258</td><td/><td>kinit.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>klist.exe</td><td>{5D030ED6-82C7-40ED-B02A-C32B39154324}</td><td>BIN</td><td>258</td><td/><td>klist.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ktab.exe</td><td>{18E2D76A-343D-4145-9631-757E9A779237}</td><td>BIN</td><td>258</td><td/><td>ktab.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>lcms.dll</td><td>{F9DDE4C6-41DC-46A5-8C94-9B2FB9A0BB2B}</td><td>BIN</td><td>258</td><td/><td>lcms.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>management.dll</td><td>{C59A3DEB-0E2E-45C8-AA54-7B16B4D23A10}</td><td>BIN</td><td>258</td><td/><td>management.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>mlib_image.dll</td><td>{A7019D67-FF8A-412D-BFD9-E35A921DBA63}</td><td>BIN</td><td>258</td><td/><td>mlib_image.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>msvcp120.dll</td><td>{302E3C2A-2349-4814-AA05-6EFAABCDDD47}</td><td>BIN</td><td>258</td><td/><td>msvcp120.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>msvcr100.dll</td><td>{61687F69-6EB9-4F89-A51C-0F6CE71CB589}</td><td>BIN</td><td>258</td><td/><td>msvcr100.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>msvcr100.dll1</td><td>{5BC8C34C-7D85-4879-B694-C00993C14A80}</td><td>PLUGIN2</td><td>258</td><td/><td>msvcr100.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>msvcr120.dll</td><td>{B2BF413F-AA6C-4AAE-8E01-0F0A1328347E}</td><td>BIN</td><td>258</td><td/><td>msvcr120.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>net.dll</td><td>{6366E0CD-2B65-478A-A0CA-8D5CEFED23F8}</td><td>BIN</td><td>258</td><td/><td>net.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>nio.dll</td><td>{DF353EAB-9617-43DF-8162-852D8CC42E35}</td><td>BIN</td><td>258</td><td/><td>nio.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>npdeployJava1.dll</td><td>{D938D90D-CCC5-41D0-89FF-9A1DA0774729}</td><td>DTPLUGIN</td><td>258</td><td/><td>npdeployjava1.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>npjp2.dll</td><td>{3098B3C5-ED17-406D-A868-DA3FCCED2241}</td><td>PLUGIN2</td><td>258</td><td/><td>npjp2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>npt.dll</td><td>{EB1DD831-3B26-4833-8425-0F5669952121}</td><td>BIN</td><td>258</td><td/><td>npt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ocemul.dll</td><td>{FDB068EC-2BA7-4CAC-83DE-EF3FD8D77716}</td><td>OUTSIDEIN</td><td>258</td><td/><td>ocemul.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>orbd.exe</td><td>{7BDFFB48-788E-48E7-8C29-6DCBE273675D}</td><td>BIN</td><td>258</td><td/><td>orbd.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>oswin32.dll</td><td>{8A706B23-E290-4E3C-8BFE-079832A1864D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>oswin32.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>pack200.exe</td><td>{B487EC40-62E0-4AD0-9859-17238DA3850E}</td><td>BIN</td><td>258</td><td/><td>pack200.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>policytool.exe</td><td>{3D073AE6-E34E-4648-A83C-1B43E5DC2CF0}</td><td>BIN</td><td>258</td><td/><td>policytool.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>prism_common.dll</td><td>{F40BBEA9-1F56-47B4-8607-AEAEEFE7C9C0}</td><td>BIN</td><td>258</td><td/><td>prism_common.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>prism_d3d.dll</td><td>{6D2BCB6B-7AD1-4733-ABB8-1E06384BD70C}</td><td>BIN</td><td>258</td><td/><td>prism_d3d.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>prism_sw.dll</td><td>{00184D87-CCF9-4535-8030-BFC1CF0D5675}</td><td>BIN</td><td>258</td><td/><td>prism_sw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>resource.dll</td><td>{5F31595D-FFB7-4378-AAD8-D9C4962D3A10}</td><td>BIN</td><td>258</td><td/><td>resource.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>rmid.exe</td><td>{B13F4580-0836-453A-B0A9-4785C9D1ED43}</td><td>BIN</td><td>258</td><td/><td>rmid.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>rmiregistry.exe</td><td>{DBD31CB0-248F-4D27-A0AE-979A9C9AAF44}</td><td>BIN</td><td>258</td><td/><td>rmiregistry.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccanno.dll</td><td>{B30CB7B0-9230-4475-8DE7-8DEB080276D5}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccanno.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccca.dll</td><td>{3E0BAD90-5EC1-4938-8E10-928AA0EEF1C0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccca.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccch.dll</td><td>{6D1437CF-4FAC-4230-BFC8-6858FCA29FCC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccch.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccda.dll</td><td>{2C1C9B97-B430-476B-85B3-2711013DD1DF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccda.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccdu.dll</td><td>{A503DE83-0B2E-4A6C-B852-0812C2934D19}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccdu.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccfa.dll</td><td>{777D2BBF-3415-42F9-B7A5-C205CDAD6336}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccfa.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccfi.dll</td><td>{28044BD9-9D13-419B-AA60-2DD578213BFA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccfi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccfmt.dll</td><td>{FD8726FD-C4E8-4848-ABF9-33185024D1E8}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccfmt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccfnt.dll</td><td>{F663DFDB-6D01-49C9-942D-B260050CCFBF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccfnt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccfut.dll</td><td>{C71695B0-EECB-4EC3-AF2A-DD8CAB2DD6BC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccfut.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccind.dll</td><td>{D50E0EB9-D788-4CD9-9073-E373CCC1379F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccind.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>scclo.dll</td><td>{87BB0619-3D2C-4744-96EF-6DD80CBF265E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>scclo.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccole.dll</td><td>{13A4DD76-8625-4F99-8CE2-B25A29417D13}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccole.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccra.dll</td><td>{7B16B8F4-AA68-416B-A606-24989C5BF838}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccra.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccsd.dll</td><td>{2FCA87BC-C9D0-4117-82F0-4B6B4AA836D3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccsd.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccta.dll</td><td>{F8577136-CE5F-4B3A-99AD-4566932B24E3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccta.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccut.dll</td><td>{3DB88BBF-5B6F-4005-ACFD-D1A9B88DFCD3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccut.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccvw.dll</td><td>{910B70E2-8EDE-48DE-BD0E-84B52CD8857D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccvw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sccxt.dll</td><td>{182DD41F-38FF-46C6-B010-536C571DAD77}</td><td>OUTSIDEIN</td><td>258</td><td/><td>sccxt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>servertool.exe</td><td>{5324B455-583B-4F43-B2DC-810CD719E7B7}</td><td>BIN</td><td>258</td><td/><td>servertool.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>splashscreen.dll</td><td>{9792D980-723C-468B-B00E-47A325C48DB9}</td><td>BIN</td><td>258</td><td/><td>splashscreen.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ssv.dll</td><td>{430C7255-11FC-4907-B748-2642E3B0855F}</td><td>BIN</td><td>258</td><td/><td>ssv.dll</td><td>20</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ssvagent.exe</td><td>{5081B06D-73A5-4F2E-9D38-90E83EC4CCB6}</td><td>BIN</td><td>258</td><td/><td>ssvagent.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sunec.dll</td><td>{ACC9510E-BC38-46EA-A071-95C075281D5C}</td><td>BIN</td><td>258</td><td/><td>sunec.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>sunmscapi.dll</td><td>{35E5C609-102B-4549-B6F3-6AB13127E63D}</td><td>BIN</td><td>258</td><td/><td>sunmscapi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>t2k.dll</td><td>{D959D7D3-DCAB-4319-B3D9-8FF32BD13B93}</td><td>BIN</td><td>258</td><td/><td>t2k.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>tnameserv.exe</td><td>{624DE9B1-088A-4E2C-AF65-E90D0A780F3D}</td><td>BIN</td><td>258</td><td/><td>tnameserv.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>unpack.dll</td><td>{1A67FF54-D9EA-49D2-A06E-A0A00EFDF60B}</td><td>BIN</td><td>258</td><td/><td>unpack.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>unpack200.exe</td><td>{9EED6F98-B66D-412B-A239-C4D61BB14527}</td><td>BIN</td><td>258</td><td/><td>unpack200.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>verify.dll</td><td>{001ACBDC-833E-4054-918A-B9034B6E718C}</td><td>BIN</td><td>258</td><td/><td>verify.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsacad.dll</td><td>{DFE1168E-D8D9-42D3-A179-A4DEA4E4AE8A}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsacad.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsacs.dll</td><td>{FEAFECC5-772A-4D01-B466-04092B6E632B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsacs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsami.dll</td><td>{63A3C30C-EE91-432F-B916-DCEB245A9A19}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsami.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsarc.dll</td><td>{E59821AA-4801-4E07-A291-1EBB6DA1731A}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsarc.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsasf.dll</td><td>{F07F3BB8-E87D-4C57-8C44-5AA48DFAA2E4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsasf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsbdr.dll</td><td>{B569D038-3E53-4977-8856-13934D9FC10C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsbdr.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsbmp.dll</td><td>{04C899CB-5055-4DC1-B28A-ACB85DC18FD5}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsbmp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vscdrx.dll</td><td>{03F1C53A-DCE3-4118-80B5-9291A8BCE42D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vscdrx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vscgm.dll</td><td>{7DACC05D-4B63-4F27-B789-F581CE561A87}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vscgm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdbs.dll</td><td>{2963F2C9-B79A-4AA7-B918-9D8F9E5F4A57}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdbs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdez.dll</td><td>{A47DAF82-E850-4E0B-A09F-DBCF86295E1B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdez.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdif.dll</td><td>{0E7625B3-CCD4-4333-A99D-CE1447DAF905}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdif.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdrw.dll</td><td>{EE16228F-4504-4AAD-8786-5D64A3B2A64C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdrw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdx.dll</td><td>{120317A6-D867-4D1B-8392-DBDD055EB0DB}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdxla.dll</td><td>{C5C269B7-AE45-4D32-B749-E7A920186290}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdxla.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsdxlm.dll</td><td>{F6CDB9D7-CDA0-4AB9-BE84-B5F357E2618C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsdxlm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsemf.dll</td><td>{A2633DEF-34D2-429C-AF4C-356507392484}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsemf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsen4.dll</td><td>{F8A04690-F26E-41ED-AD51-E5436B4EBDD6}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsen4.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsens.dll</td><td>{FB212329-DE46-4A6F-A5B8-DE68D40030CC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsens.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsenw.dll</td><td>{48BFDAC4-EAB0-4845-AC6F-B9A950BC9D03}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsenw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vseshr.dll</td><td>{5CF50167-CDAA-433C-BECC-529995FFF309}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vseshr.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsexe2.dll</td><td>{D78FF230-8FBE-4759-96C7-F9E1415C3E2A}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsexe2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsfax.dll</td><td>{6383CD58-557F-44E7-BB98-1FAC6B714FD9}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsfax.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsfcd.dll</td><td>{FC579B28-7DA6-4488-BBCC-6B3823BFA75F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsfcd.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsfcs.dll</td><td>{0682F7F5-4EA9-4598-B48F-F22C3D29E1CA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsfcs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsfft.dll</td><td>{98067B84-FD76-4754-8983-A67BE91BB893}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsfft.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsflw.dll</td><td>{651EEBD1-D76D-49AF-92D8-0BE474729EAD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsflw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsfwk.dll</td><td>{BD80F3A5-B3CF-49A5-A0C7-5C769F7AC706}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsfwk.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsgdsf.dll</td><td>{56F832AB-333B-46CA-B8DA-5B039913B7AA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsgdsf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsgif.dll</td><td>{65EF62EE-A0B1-40E0-A7AA-F408E646539F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsgif.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsgzip.dll</td><td>{6D0418BE-7666-4B93-9CF7-6128A54C3142}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsgzip.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vshgs.dll</td><td>{0B5F1B38-B172-4A63-885B-85D1EA62F72B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vshgs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vshtml.dll</td><td>{48973825-0FE6-4BB7-BB39-E8E17A30878E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vshtml.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vshwp.dll</td><td>{60F1C3D3-2B43-458F-BF58-BA69439F963B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vshwp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vshwp2.dll</td><td>{B435D92F-DB79-48DD-938B-2C9BD76F7FBD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vshwp2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsich.dll</td><td>{A78BE3FC-2342-4B03-979F-851A3425C9E1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsich.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsich6.dll</td><td>{610A890A-23E2-443A-8F7C-1D0876CE4332}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsich6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsid3.dll</td><td>{0554EA30-3270-470D-8436-7AEAC73B57F2}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsid3.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsimg.dll</td><td>{5EF934F5-91E1-46D6-9DE6-435A7F0AA185}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsimg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsiwok.dll</td><td>{F04FAB14-0B0E-47ED-98FD-4CDCB07D191A}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsiwok.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsiwok13.dll</td><td>{EC652041-BD77-4E4F-BAD8-704B67E6C04F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsiwok13.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsiwon.dll</td><td>{F32E09D8-73A2-4B0D-8F4E-8012C592F1A0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsiwon.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsiwop.dll</td><td>{F3742AEA-F04A-46CD-B3DC-B86285A4FB23}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsiwop.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsiwp.dll</td><td>{E0D8A212-B4A5-49CB-9730-C0D2EABBD368}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsiwp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsjbg2.dll</td><td>{258F2CD9-1ADE-4CFA-AB81-5AE332F3FDFF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsjbg2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsjp2.dll</td><td>{7840D2EF-6087-497C-A1CE-7AE8FF89C54F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsjp2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsjw.dll</td><td>{AF97C15C-42B5-4603-8A0D-FBFF4563A175}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsjw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsleg.dll</td><td>{12A102BA-F132-42DD-846A-A645F6FFDFB0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsleg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vslwp7.dll</td><td>{F78B50C3-AEFA-487B-9382-BEE99CB02C78}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vslwp7.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vslzh.dll</td><td>{B3DB3634-3F67-4868-81E2-312B44B0133E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vslzh.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsm11.dll</td><td>{AA505036-C1A3-478A-96C9-F800EF564D32}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsm11.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmanu.dll</td><td>{DF7437F5-E47D-432D-8EDB-5B36DADBEB51}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmanu.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmbox.dll</td><td>{C179353A-ED48-483A-9345-11ACD704FEEC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmbox.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmcw.dll</td><td>{93E8B749-747C-459E-9549-3CFAA35D0DDB}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmcw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmdb.dll</td><td>{ED04A99F-1FFD-4962-BB7C-8A583192E86B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmdb.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmif.dll</td><td>{B32AC5B1-D98B-4EF5-8ADB-C5C67B660687}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmif.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmime.dll</td><td>{451A48C5-8E5E-475A-826C-CFE170D7CDE3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmime.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmm.dll</td><td>{46A10A37-CD66-4BA7-AE31-3E76F98DD60C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmm4.dll</td><td>{A8B63838-DE4D-4C5E-9A6F-747A4A2970BC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmm4.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmmfn.dll</td><td>{14C02FE9-2C11-4841-9283-3FF43F4F409C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmmfn.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmp.dll</td><td>{91D428C2-0216-4A63-9FA6-9B7A89E03919}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmpp.dll</td><td>{56BE817B-D3F9-4D51-ADEE-4F9EAE7B1C2D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmpp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmsg.dll</td><td>{5667D833-3D0D-4C11-8830-08AC4837C4EF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmsg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmsw.dll</td><td>{D1B3DA19-0432-483A-A68C-C7F4F224A0B2}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmsw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmwkd.dll</td><td>{A1930F19-2E1A-402B-A6AE-3B3A634A515B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmwkd.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmwks.dll</td><td>{BED8A946-D44E-4DE9-AEB3-CC7E7E5F66FE}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmwks.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmwp2.dll</td><td>{A07DF62C-E447-4C01-8DD6-697180BF4E87}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmwp2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmwpf.dll</td><td>{13E820ED-1681-4798-AA43-40DBBD9A30BE}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmwpf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsmwrk.dll</td><td>{556010CA-B83B-42D6-82C9-3C35C0066475}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsmwrk.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsnsf.dll</td><td>{F055AA7C-C037-44F9-8987-77414EA6C6F1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsnsf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsolm.dll</td><td>{D38BE07A-1881-4E79-B941-05C8B5A8F833}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsolm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsone.dll</td><td>{3441AD3A-9E4A-4B52-AFAF-4BBC03725E86}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsone.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsow.dll</td><td>{34BB64FE-5E75-4E1E-81FF-FC3B1DA70AA8}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsow.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspbm.dll</td><td>{BF1AFFF0-6671-410E-AF1E-E2CB13049482}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspbm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspcl.dll</td><td>{F04F426C-1C49-438F-9031-10B19652D1F3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspcl.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspcx.dll</td><td>{AA6B6E28-61E3-4B52-8082-64E0427135E0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspcx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspdf.dll</td><td>{EED86051-939E-4A2A-99FC-FC6C1D7E917C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspdf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspdfi.dll</td><td>{2E0DD6D2-917E-4130-AC4D-6C786401884F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspdfi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspdx.dll</td><td>{176DCF90-B420-4728-A3BE-A01D7721ECA4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspdx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspfs.dll</td><td>{E3D38562-ED34-4B24-B02B-C24D46167AAC}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspfs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspgl.dll</td><td>{9407ECF6-A97C-4C49-892D-0098DB08624C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspgl.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspic.dll</td><td>{1305A255-03F3-4EF4-8C43-CCDF8EE9BEF4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspic.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspict.dll</td><td>{0B286030-BD6B-4B0F-8A18-CF9A075178D7}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspict.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspng.dll</td><td>{C701AF09-F225-4D0E-B36A-8343D57B32EF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspng.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspntg.dll</td><td>{7A0B2432-6833-4871-A5EA-A1575116C786}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspntg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspp12.dll</td><td>{E57F0E25-F0CD-40C6-B6F0-482D48E2313F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspp12.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspp2.dll</td><td>{FA2ACCA9-42C3-4DF5-AEA3-2836853265B8}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspp2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspp7.dll</td><td>{1BD6CD91-EE9C-4D32-B5D4-AFB53D4A8CCD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspp7.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspp97.dll</td><td>{CB08C0B1-B2F5-4B49-B6EC-824A8DEE9DCA}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspp97.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsppl.dll</td><td>{DBC4BEBC-23F1-4F3F-B7CE-8BE0BC33F2C0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsppl.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspsp6.dll</td><td>{21C46248-F13A-4172-8277-60A74DF988D7}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspsp6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspst.dll</td><td>{CB88D26A-BE7D-4C4D-A47C-2B6DD8C1EEB1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspst.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vspstf.dll</td><td>{D1796EEB-CCFD-416E-80B2-CA967FD57204}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vspstf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsqa.dll</td><td>{FB35B2D0-82D0-41E6-89AD-EE6BB61AB2CE}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsqa.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsqad.dll</td><td>{0EEF4760-2B59-40A3-8A71-D0311D67F4C1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsqad.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsqp6.dll</td><td>{2B52EBE0-4248-4CDE-8E1A-0172F177EC0F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsqp6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsqp9.dll</td><td>{0886393E-FFE5-46DF-B681-65B14D6EE507}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsqp9.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsqt.dll</td><td>{C9563079-DBC8-4FAE-807B-D18A1DBDD79F}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsqt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsrar.dll</td><td>{F27A409D-A3D6-4118-B85E-451FA26C880E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsrar.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsras.dll</td><td>{B8FCE839-85C6-4131-89E4-01599F994166}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsras.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsrbs.dll</td><td>{9F0AADE9-17EC-4880-AE13-04A1029B3DA4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsrbs.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsrft.dll</td><td>{A56CA461-107F-4EC8-ADA6-F886413EEA02}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsrft.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsrfx.dll</td><td>{4CBF887C-CA88-42D0-BEA1-33A70804E88D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsrfx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsriff.dll</td><td>{2BB3AE35-C7C1-489A-A98D-A6D2D4CB428A}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsriff.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsrtf.dll</td><td>{08B91F12-69FD-4DDC-983F-023039567E8C}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsrtf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssam.dll</td><td>{D5BE1EEF-A031-4EE3-9004-8EF0B60B6407}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssam.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssc5.dll</td><td>{91537609-DA1E-4F8E-8CAF-9EC38678D5D9}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssc5.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssdw.dll</td><td>{782E69F7-0688-4B6D-9E79-904024635830}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssdw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsshw3.dll</td><td>{D828F2AB-AF9A-4E4B-95B9-ADD1BE0BED84}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsshw3.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssmd.dll</td><td>{676C86A0-88A4-4603-8160-C55495CA20A3}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssmd.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssms.dll</td><td>{A50369C4-09B7-4554-B687-E5B59B7C519B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssms.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssmt.dll</td><td>{DCBD26AC-E135-4DD8-9F92-C988EEAB2337}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssmt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssnap.dll</td><td>{DCF2315C-4A4B-406E-B93C-C72D336F8AA4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssnap.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsso6.dll</td><td>{3DC880CF-A716-418F-88F2-B89AE87DE942}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsso6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssoc.dll</td><td>{9B8B831F-4733-4272-9E58-15EDC205AD34}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssoc.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssoc6.dll</td><td>{9DF8F8E9-EBC9-440F-B36F-F6FB8FA84F33}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssoc6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssoi.dll</td><td>{15ACD744-81F9-43B8-B05E-A3E36A0E95BF}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssoi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssoi6.dll</td><td>{99F6FE98-C6F5-4C7C-A163-541BF5B54A62}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssoi6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vssow.dll</td><td>{C4D3974A-96A8-4C24-AFEA-68370F45D091}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vssow.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsspt.dll</td><td>{15A610C2-1E59-4096-892F-A61260EAC0B5}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsspt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsssml.dll</td><td>{17EB38DC-D422-4E44-B37B-F4CB5901D078}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsssml.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsswf.dll</td><td>{9CE714B2-E00E-4EB0-A50E-2D4C54BF0E16}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsswf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstaz.dll</td><td>{996A09B3-9336-495E-9CF4-9F21177B7145}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstaz.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstext.dll</td><td>{47B80320-FBD5-46E9-BA8B-5ACE1802CE86}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstext.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstga.dll</td><td>{B574F37D-132F-4FF8-8710-84CCB3C4768B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstga.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstif6.dll</td><td>{6519FD29-DC6F-4BAE-8DEE-3C7B412A181B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstif6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstw.dll</td><td>{4740815F-965B-4455-B0F3-A19E6A648AB2}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vstxt.dll</td><td>{1484A45A-31B2-4E17-B859-6F19DA248536}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vstxt.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsvcrd.dll</td><td>{FB73F4C2-AAD8-4B47-99D0-89F2397642FB}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsvcrd.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsviso.dll</td><td>{F1371B97-B74C-42DF-A4D6-8C253D560589}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsviso.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsvsdx.dll</td><td>{455EAB4B-E7EE-4B60-BB03-4440AED95F54}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsvsdx.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsvw3.dll</td><td>{113E48D9-41CF-4B92-9117-788FBB66E072}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsvw3.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsw12.dll</td><td>{62BA362C-0B42-46FD-B1AE-2B61BDDEC6C2}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsw12.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsw6.dll</td><td>{FC3D78CD-5596-4AED-8D84-D8F88610E7F5}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsw6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsw97.dll</td><td>{6614DDB2-4BE0-417C-BFE3-183958AD2540}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsw97.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswbmp.dll</td><td>{021E2EDB-A0AD-432C-B075-61557E3001C4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswbmp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswg2.dll</td><td>{A45FCECA-3E2E-408F-8A4A-8A6D20C4B591}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswg2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswk4.dll</td><td>{4AB57F0A-0E9E-44A4-8DF3-9A89DD43819B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswk4.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswk6.dll</td><td>{FDBF6CEF-3905-4CE6-8FB0-DEEACD48C86E}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswk6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswks.dll</td><td>{5BE0D119-2D31-405B-9AEA-BCD03C506187}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswks.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswm.dll</td><td>{BEEE8466-8571-42FC-9588-AAAD2DD6AB89}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswm.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswmf.dll</td><td>{F90D8C51-AEDB-47AA-8EEC-2799DD4D8585}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswmf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswml.dll</td><td>{63EA1249-F872-43D1-AE3C-386DC41EE96B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswml.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsword.dll</td><td>{1ED0FCE8-80E3-4847-982E-E9490E4FA744}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsword.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswork.dll</td><td>{A6E407D0-0365-4545-AF8F-B33113D7BC86}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswork.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswp5.dll</td><td>{3EE1E48F-FF30-4254-84C9-A395C2ED0BF1}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswp5.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswp6.dll</td><td>{47ADE249-F4A7-4459-A4CE-816E98C45CE8}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswp6.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpf.dll</td><td>{78133105-A22D-48A5-8836-57E823D34BC0}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpf.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpg.dll</td><td>{4316A1ED-E9A4-4EEC-8B98-A53C8CFF6BE5}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpg.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpg2.dll</td><td>{857F084E-78F0-46EC-9FBB-0C2BC88DD0FD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpg2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpl.dll</td><td>{34598AA4-D7FA-44BE-A8E7-7C721D775C07}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpl.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpml.dll</td><td>{40270DB8-78D2-46F9-AA85-F85FC800EBD6}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpml.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vswpw.dll</td><td>{DA5DE193-F454-4C95-807E-787EA27544FD}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vswpw.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsws.dll</td><td>{F19FF7EF-2DCB-4BF8-B3DA-C3222552FD0B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsws.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsws2.dll</td><td>{836468EE-626C-4742-8AB0-3968D91E5502}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsws2.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxl12.dll</td><td>{47141C88-056C-40F7-8B0C-D11396EB4BD6}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxl12.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxl5.dll</td><td>{72228D35-4A5C-4720-A116-C6C8517E4731}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxl5.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxlsb.dll</td><td>{72368DC7-D788-4AC6-B888-7F1F46D2478B}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxlsb.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxml.dll</td><td>{38432E99-3C96-438D-A14A-8AFD20341CB8}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxml.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxps.dll</td><td>{750377EF-29A3-4797-994D-20ED3B4894A6}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxps.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsxy.dll</td><td>{133B69B7-E79E-45F6-A849-12368703D091}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsxy.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vsyim.dll</td><td>{FCA27EB4-49AA-4ACD-943A-E44A9DD2C202}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vsyim.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>vszip.dll</td><td>{4B5DD446-F01E-4B8F-8F9F-16DE786980B4}</td><td>OUTSIDEIN</td><td>258</td><td/><td>vszip.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>w2k_lsa_auth.dll</td><td>{4B1D682A-FED8-425A-8356-5FAEAC94A777}</td><td>BIN</td><td>258</td><td/><td>w2k_lsa_auth.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>wsdetect.dll</td><td>{AFC659D1-ED3B-424C-9486-F6CBCD04E4F5}</td><td>BIN</td><td>258</td><td/><td>wsdetect.dll</td><td>20</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>wvcore.dll</td><td>{4E803DFE-AC01-407D-B8FE-FBB257E02E9D}</td><td>OUTSIDEIN</td><td>258</td><td/><td>wvcore.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>zip.dll</td><td>{BED1C59A-4CD4-43FD-9269-A98F70F9CE0B}</td><td>BIN</td><td>258</td><td/><td>zip.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
	</table>

	<table name="Condition">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="i2">Level</col>
		<col def="S255">Condition</col>
	</table>

	<table name="Control">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control</col>
		<col def="s20">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="S72">Property</col>
		<col def="L0">Text</col>
		<col def="S50">Control_Next</col>
		<col def="L50">Help</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISControlId</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="S72">Binary_</col>
		<row><td>AdminChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>AdminChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>458755</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_BrowseDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_ChangeDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>7</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsAdminInstallBrowse_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>3</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsAdminInstallBrowse_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>AdminNetworkLocation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>PushButton</td><td>286</td><td>124</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>SetupPathEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_SpecifyNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>40</td><td>131075</td><td/><td>##IDS__IsAdminInstallPoint_EnterNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocationFormatted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Install##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>LBBrowse</td><td>Text</td><td>21</td><td>90</td><td>100</td><td>10</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>SetupPathEdit</td><td>PathEdit</td><td>21</td><td>102</td><td>330</td><td>17</td><td>3</td><td>TARGETDIR</td><td/><td>Browse</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_Wizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_ServerImage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Icon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>CancelSetup</td><td>No</td><td>PushButton</td><td>135</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_No##</td><td>Yes</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Text</td><td>Text</td><td>48</td><td>15</td><td>194</td><td>30</td><td>131075</td><td/><td>##IDS__IsCancelDlg_ConfirmCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Yes</td><td>PushButton</td><td>62</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_Yes##</td><td>No</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomSetup</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Tree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>203</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Change##</td><td>Help</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Details</td><td>PushButton</td><td>93</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Space##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_SelectFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgText</td><td>Text</td><td>9</td><td>51</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_ClickFeatureIcon##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>FeatureGroup</td><td>GroupBox</td><td>235</td><td>67</td><td>131</td><td>120</td><td>1</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Help</td><td>PushButton</td><td>22</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Help##</td><td>Details</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Text</td><td>8</td><td>190</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>Text</td><td>241</td><td>80</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_MultilineDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Location</td><td>Text</td><td>8</td><td>203</td><td>291</td><td>20</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeaturePath##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Size</td><td>Text</td><td>241</td><td>133</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureSize##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Tree</td><td>SelectionTree</td><td>8</td><td>70</td><td>220</td><td>118</td><td>7</td><td>_BrowseProperty</td><td/><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomSetupTips</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetupDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DontInstall</td><td>Icon</td><td>21</td><td>155</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary14</td></row>
		<row><td>CustomSetupTips</td><td>DontInstallText</td><td>Text</td><td>60</td><td>155</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_WillNotBeInstalled##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>FirstInstallText</td><td>Text</td><td>60</td><td>180</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Advertise##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Install</td><td>Icon</td><td>21</td><td>105</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary15</td></row>
		<row><td>CustomSetupTips</td><td>InstallFirstUse</td><td>Icon</td><td>21</td><td>180</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary16</td></row>
		<row><td>CustomSetupTips</td><td>InstallPartial</td><td>Icon</td><td>21</td><td>130</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary17</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateMenu</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary18</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateText</td><td>Text</td><td>21</td><td>91</td><td>300</td><td>10</td><td>3</td><td/><td>##IDS_SetupTips_InstallState##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>CustomSetupTips</td><td>InstallText</td><td>Text</td><td>60</td><td>105</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_AllInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>MenuText</td><td>Text</td><td>50</td><td>52</td><td>300</td><td>36</td><td>3</td><td/><td>##IDS_SetupTips_IconInstallState##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>NetworkInstall</td><td>Icon</td><td>21</td><td>205</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary13</td></row>
		<row><td>CustomSetupTips</td><td>NetworkInstallText</td><td>Text</td><td>60</td><td>205</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Network##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_SetupTips_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>PartialText</td><td>Text</td><td>60</td><td>130</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_SubFeaturesInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomerInformation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>NameLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyEdit</td><td>Edit</td><td>21</td><td>100</td><td>237</td><td>17</td><td>3</td><td>COMPANYNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma80##</td><td>SerialLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyLabel</td><td>Text</td><td>21</td><td>89</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_Organization##</td><td>CompanyEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_PleaseEnterInfo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Text</td><td>21</td><td>161</td><td>300</td><td>14</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_CustomerInformation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameEdit</td><td>Edit</td><td>21</td><td>63</td><td>237</td><td>17</td><td>3</td><td>USERNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma50##</td><td>CompanyLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameLabel</td><td>Text</td><td>21</td><td>52</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_UserName##</td><td>NameEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>63</td><td>170</td><td>300</td><td>50</td><td>2</td><td>ApplicationUsers</td><td>##IDS__IsRegisterUserDlg_16##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Text</td><td>21</td><td>127</td><td>109</td><td>10</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_SerialNumber##</td><td>SerialNumber</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>MaskedEdit</td><td>21</td><td>138</td><td>237</td><td>17</td><td>2</td><td>ISX_SERIALNUM</td><td/><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DatabaseFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CHANGE##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DatabaseFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DatabaseFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_DatabaseFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS_DatabaseFolder_InstallDatabaseTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS__DatabaseFolder_DatabaseDir##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DestinationFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__DestinationFolder_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DestFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DestinationFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_DestinationFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS__DestinationFolder_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS_INSTALLDIR##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DiskSpaceRequirements</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_SpaceRequired##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgText</td><td>Text</td><td>10</td><td>185</td><td>358</td><td>41</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_VolumesTooSmall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_DiskSpaceRequirements##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>List</td><td>VolumeCostList</td><td>8</td><td>55</td><td>358</td><td>125</td><td>393223</td><td/><td>##IDS__IsFeatureDetailsDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>FilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>33</td><td>3</td><td/><td>##IDS__IsFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Exit</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Exit##</td><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Ignore##</td><td>Exit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>87</td><td>331</td><td>135</td><td>7</td><td>FileInUseProcess</td><td/><td>Retry</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Retry</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Retry##</td><td>Ignore</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>InstallChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>4128779</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_BrowseDestFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_ChangeCurrentFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsBrowseFolderDlg_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsBrowseFolderDlg_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Copyright</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Text</td><td>135</td><td>144</td><td>228</td><td>73</td><td>65539</td><td/><td>##IDS__IsWelcomeDlg_WarningCopyright##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_WelcomeProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_InstallProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Agree</td><td>RadioButtonGroup</td><td>8</td><td>190</td><td>291</td><td>40</td><td>3</td><td>AgreeToLicense</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>LicenseAgreement</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ISPrintButton</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_ReadLicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_LicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>PushButton</td><td>301</td><td>188</td><td>65</td><td>17</td><td>3</td><td/><td>##IDS_PRINT_BUTTON##</td><td>Agree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Memo</td><td>ScrollableText</td><td>8</td><td>55</td><td>358</td><td>130</td><td>7</td><td/><td/><td/><td/><td>0</td><td/><td>&lt;ISProductFolder&gt;\Redist\0409\Eula.rtf</td><td/></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>MaintenanceType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_MaitenanceOptions##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_ProgramMaintenance##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Ico1</td><td>Icon</td><td>35</td><td>75</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary6</td></row>
		<row><td>MaintenanceType</td><td>Ico2</td><td>Icon</td><td>35</td><td>135</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary7</td></row>
		<row><td>MaintenanceType</td><td>Ico3</td><td>Icon</td><td>35</td><td>195</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary8</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>21</td><td>55</td><td>290</td><td>170</td><td>3</td><td>_IsMaintenance</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text1</td><td>Text</td><td>80</td><td>72</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_ChangeFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text2</td><td>Text</td><td>80</td><td>135</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_RepairMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text3</td><td>Text</td><td>80</td><td>192</td><td>260</td><td>35</td><td>131075</td><td/><td>##IDS__IsMaintenanceDlg_RemoveProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_WizardWelcome##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>50</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>MsiRMFilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Restart</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>14</td><td>3</td><td/><td>##IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>66</td><td>331</td><td>130</td><td>3</td><td>FileInUseProcess</td><td/><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Restart</td><td>RadioButtonGroup</td><td>19</td><td>187</td><td>343</td><td>40</td><td>3</td><td>RestartManagerOption</td><td/><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>OutOfSpace</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_DiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>43</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_HighlightedVolumes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_OutOfDiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>List</td><td>VolumeCostList</td><td>21</td><td>95</td><td>332</td><td>120</td><td>393223</td><td/><td>##IDS__IsDiskSpaceDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsPatchDlg_Update##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_WelcomePatchWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>54</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_PatchClickUpdate##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>3</td><td/><td/><td>DlgTitle</td><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadmeInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS__IsReadmeDlg_Cancel##</td><td>Readme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>232</td><td>16</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_PleaseReadInfo##</td><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>3</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>193</td><td>13</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_ReadMeInfo##</td><td>DlgDesc</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Readme</td><td>ScrollableText</td><td>10</td><td>55</td><td>353</td><td>166</td><td>3</td><td/><td/><td>Banner</td><td/><td>0</td><td/><td>&lt;ISProductFolder&gt;\Redist\0409\Readme.rtf</td><td/></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>GroupBox1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadyToInstall</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Text</td><td>38</td><td>198</td><td>211</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Company##</td><td>SerialNumberText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Text</td><td>19</td><td>80</td><td>81</td><td>10</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_CurrentSettings##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyReadyDlg_WizardReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText1</td><td>Text</td><td>21</td><td>54</td><td>330</td><td>24</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_BackOrCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Text</td><td>21</td><td>99</td><td>330</td><td>20</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ModifyReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyRepair##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyInstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Text</td><td>19</td><td>92</td><td>330</td><td>133</td><td>65541</td><td/><td/><td>SetupTypeText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyReadyDlg_Install##</td><td>InstallPerMachine</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>PushButton</td><td>63</td><td>123</td><td>248</td><td>17</td><td>8388610</td><td/><td>##IDS__IsRegisterUserDlg_Anyone##</td><td>InstallPerUser</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>PushButton</td><td>63</td><td>143</td><td>248</td><td>17</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Text</td><td>38</td><td>211</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Serial##</td><td>CurrentSettingsText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Text</td><td>23</td><td>97</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SetupType##</td><td>SetupTypeText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Text</td><td>37</td><td>114</td><td>306</td><td>14</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SelectedSetupType##</td><td>TargetFolderText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Text</td><td>24</td><td>136</td><td>306</td><td>11</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_DestFolder##</td><td>TargetFolderText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Text</td><td>37</td><td>151</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Installdir##</td><td>UserInformationText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Text</td><td>23</td><td>171</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserInfo##</td><td>UserNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Text</td><td>38</td><td>184</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserName##</td><td>CompanyNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>RemoveNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadyToRemove</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>24</td><td>131075</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickRemove##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText1</td><td>Text</td><td>21</td><td>79</td><td>330</td><td>23</td><td>3</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickBack##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText2</td><td>Text</td><td>21</td><td>102</td><td>330</td><td>24</td><td>3</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_RemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyRemoveAllDlg_Remove##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFatalError_Finish##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_RestoreOrContinueLater##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsFatalError_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsFatalError_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>CheckBox</td><td>135</td><td>164</td><td>10</td><td>9</td><td>2</td><td>ISCHECKFORPRODUCTUPDATES</td><td>CheckBox1</td><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Text</td><td>152</td><td>162</td><td>190</td><td>30</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_YesCheckForUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>CheckBox</td><td>151</td><td>114</td><td>10</td><td>9</td><td>2</td><td>LAUNCHPROGRAM</td><td/><td>CheckLaunchReadme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>CheckBox</td><td>151</td><td>148</td><td>10</td><td>9</td><td>2</td><td>LAUNCHREADME</td><td/><td>CheckBoxUpdates</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>182</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckLaunchProgram</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Text</td><td>164</td><td>112</td><td>98</td><td>15</td><td>65538</td><td/><td>##IDS__IsExitDialog_LaunchProgram##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Text</td><td>164</td><td>148</td><td>120</td><td>13</td><td>65538</td><td/><td>##IDS__IsExitDialog_ShowReadMe##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsExitDialog_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>182</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsExitDialog_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_InstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_UninstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Text</td><td>135</td><td>30</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_SetupFinished##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_PossibleUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Text</td><td>135</td><td>120</td><td>228</td><td>45</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_InternetConnection##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>A</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Abort##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>C</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>ErrorIcon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>SetupError</td><td>ErrorText</td><td>Text</td><td>50</td><td>15</td><td>200</td><td>50</td><td>131075</td><td/><td>##IDS__IsErrorDlg_ErrorText##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>I</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Ignore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>N</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_NO##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>O</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>R</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Retry##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>Y</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Yes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>Text</td><td>135</td><td>125</td><td>228</td><td>12</td><td>65539</td><td/><td>##IDS__IsInitDlg_1##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>Text</td><td>135</td><td>109</td><td>220</td><td>36</td><td>65539</td><td/><td>##IDS__IsInitDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInitialization</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsInitDlg_WelcomeWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>30</td><td>196611</td><td/><td>##IDS__IsInitDlg_PreparingWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsUserExit_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_RestoreOrContinue##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsUserExit_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsUserExit_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>ProgressBar</td><td>59</td><td>113</td><td>275</td><td>12</td><td>65537</td><td/><td>##IDS__IsProgressDlg_ProgressDone##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>Text</td><td>59</td><td>100</td><td>275</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>SetupProgress</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_InstallingProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_Uninstalling##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbSec</td><td>Text</td><td>192</td><td>139</td><td>32</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_SecHidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbStatus</td><td>Text</td><td>59</td><td>85</td><td>70</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_Status##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>SetupIcon</td><td>Icon</td><td>21</td><td>51</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary9</td></row>
		<row><td>SetupProgress</td><td>ShowTime</td><td>Text</td><td>170</td><td>139</td><td>17</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_Hidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>TextTime</td><td>Text</td><td>59</td><td>139</td><td>110</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_HiddenTimeRemaining##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupResume</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_WizardResume##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Text</td><td>135</td><td>46</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_ResumeSuspended##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_Resuming##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>SetupType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompText</td><td>Text</td><td>80</td><td>80</td><td>246</td><td>30</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompleteIco</td><td>Icon</td><td>34</td><td>80</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary10</td></row>
		<row><td>SetupType</td><td>CustText</td><td>Text</td><td>80</td><td>171</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CustomIco</td><td>Icon</td><td>34</td><td>171</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseSetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgText</td><td>Text</td><td>22</td><td>49</td><td>326</td><td>10</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_SelectSetupType##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_SetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>MinIco</td><td>Icon</td><td>34</td><td>125</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>MinText</td><td>Text</td><td>80</td><td>125</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>20</td><td>59</td><td>264</td><td>139</td><td>1048579</td><td>_IsSetupTypeMin</td><td/><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SplashBitmap</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Image</td><td>Bitmap</td><td>13</td><td>12</td><td>349</td><td>211</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
	</table>

	<table name="ControlCondition">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Action</col>
		<col key="yes" def="s255">Condition</col>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Hide</td><td>SHOWCOPYRIGHT="No"</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Show</td><td>SHOWCOPYRIGHT="Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Disable</td><td>AgreeToLicense &lt;&gt; "Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Enable</td><td>AgreeToLicense = "Yes"</td></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Show</td><td>ProgressType0="Modify"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Show</td><td>ProgressType0="Repair"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Show</td><td>ProgressType0="install"</td></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Disable</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Enable</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>NOT SERIALNUMSHOW</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Show</td><td>ProgressType2="installed" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Show</td><td>ProgressType2="uninstalled" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Show</td><td>ProgressType2="installed"</td></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Show</td><td>ProgressType2="uninstalled"</td></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Show</td><td>ProgressType3="installs"</td></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Show</td><td>ProgressType3="uninstalls"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Show</td><td>ProgressType1="Installing"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Show</td><td>ProgressType1="Uninstalling"</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Hide</td><td>RESUME</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Show</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Hide</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Show</td><td>RESUME</td></row>
	</table>

	<table name="ControlEvent">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col key="yes" def="s255">Argument</col>
		<col key="yes" def="S255">Condition</col>
		<col def="I2">Ordering</col>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>NewDialog</td><td>AdminWelcome</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>SpawnDialog</td><td>AdminChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>3</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>No</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>NOT Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>SelectionBrowse</td><td>InstallChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>SelectionBrowse</td><td>DiskSpaceRequirements</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Help</td><td>SpawnDialog</td><td>CustomSetupTips</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>[_IsSetupTypeMin]</td><td>Custom</td><td>1</td><td>0</td></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>EndDialog</td><td>Exit</td><td>(SERIALNUMVALRETRYLIMIT) And (SERIALNUMVALRETRYLIMIT&lt;0) And (SERIALNUMVALRETURN&lt;&gt;SERIALNUMVALSUCCESSRETVAL)</td><td>2</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>NewDialog</td><td>DestinationFolder</td><td>(Not SERIALNUMVALRETURN) OR (SERIALNUMVALRETURN=SERIALNUMVALSUCCESSRETVAL)</td><td>3</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>1</td><td>ApplicationUsers = "AllUsers" And Privileged</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>{}</td><td>ApplicationUsers = "OnlyCurrentUser" And Privileged</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>DATABASEDIR</td><td>1</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>NewDialog</td><td>SetupType</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>NOT Installed</td><td>0</td></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>INSTALLDIR</td><td>1</td><td>2</td></row>
		<row><td>DestinationFolder</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>1</td><td>0</td></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Exit</td><td>EndDialog</td><td>Exit</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>EndDialog</td><td>Ignore</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Retry</td><td>EndDialog</td><td>Retry</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>[_BrowseProperty]</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>NewDialog</td><td>SplashBitmap</td><td>Display_IsBitmapDlg</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>NewDialog</td><td>InstallWelcome</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>DoAction</td><td>ISPrint</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>NewDialog</td><td>CustomerInformation</td><td>AgreeToLicense = "Yes"</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Back</td><td>NewDialog</td><td>MaintenanceWelcome</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsMaintenance = "Change"</td><td>12</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsMaintenance = "Reinstall"</td><td>13</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToRemove</td><td>_IsMaintenance = "Remove"</td><td>11</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>_IsMaintenance = "Reinstall"</td><td>10</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>ReinstallMode</td><td>[ReinstallModeText]</td><td>_IsMaintenance = "Reinstall"</td><td>9</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Modify</td><td>_IsMaintenance = "Change"</td><td>2</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Repair</td><td>_IsMaintenance = "Reinstall"</td><td>1</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Modifying</td><td>_IsMaintenance = "Change"</td><td>3</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Repairing</td><td>_IsMaintenance = "Reinstall"</td><td>4</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>modified</td><td>_IsMaintenance = "Change"</td><td>6</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>5</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>modifies</td><td>_IsMaintenance = "Change"</td><td>7</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>8</td></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>RMShutdownAndRestart</td><td>0</td><td>RestartManagerOption="CloseRestart"</td><td>2</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>ACTION = "ADMIN"</td><td>0</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>DestinationFolder</td><td>ACTION &lt;&gt; "ADMIN"</td><td>0</td></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>PATCH And REINSTALL=""</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>ReinstallMode</td><td>omus</td><td>PATCH And REINSTALLMODE=""</td><td>2</td></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>CustomSetup</td><td>Installed OR _IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>DestinationFolder</td><td>NOT Installed</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed AND _IsMaintenance = "Reinstall"</td><td>3</td></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ALLUSERS]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[MSIINSTALLPERUSER]</td><td>{}</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ALLUSERS]</td><td>2</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[MSIINSTALLPERUSER]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>Remove</td><td>ALL</td><td>1</td><td>1</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType1]</td><td>Uninstalling</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType2]</td><td>uninstalled</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType3]</td><td>uninstalls</td><td>1</td><td>0</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1") And NOT ISENABLEDWUSFINISHDIALOG</td><td>6</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupError</td><td>A</td><td>EndDialog</td><td>ErrorAbort</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>C</td><td>EndDialog</td><td>ErrorCancel</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>I</td><td>EndDialog</td><td>ErrorIgnore</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>N</td><td>EndDialog</td><td>ErrorNo</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>O</td><td>EndDialog</td><td>ErrorOk</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>R</td><td>EndDialog</td><td>ErrorRetry</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>Y</td><td>EndDialog</td><td>ErrorYes</td><td>1</td><td>0</td></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>SetupType</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>SetupType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsSetupTypeMin &lt;&gt; "Custom"</td><td>1</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>100</td><td>_IsSetupTypeMin="Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>200</td><td>_IsSetupTypeMin="Typical"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>300</td><td>_IsSetupTypeMin="Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[ISRUNSETUPTYPEADDLOCALEVENT]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameCustom]</td><td>_IsSetupTypeMin = "Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameMinimal]</td><td>_IsSetupTypeMin = "Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameTypical]</td><td>_IsSetupTypeMin = "Typical"</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>NewDialog</td><td>InstallWelcome</td><td>1</td><td>0</td></row>
	</table>

	<table name="CreateFolder">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>ADB</td><td>AdbWinApi.dll</td></row>
		<row><td>ADB</td><td>AdbWinUsbApi.dll</td></row>
		<row><td>ADB</td><td>ISX_DEFAULTCOMPONENT170</td></row>
		<row><td>ADB</td><td>adb.exe</td></row>
		<row><td>AMD64</td><td>ISX_DEFAULTCOMPONENT34</td></row>
		<row><td>APK</td><td>ISX_DEFAULTCOMPONENT171</td></row>
		<row><td>APK</td><td>ISX_DEFAULTCOMPONENT172</td></row>
		<row><td>APK</td><td>ISX_DEFAULTCOMPONENT173</td></row>
		<row><td>ATTACHMENTS</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>ATTACHMENTS</td><td>ISX_DEFAULTCOMPONENT4</td></row>
		<row><td>ATTACHMENTS</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>BIN</td><td>ISX_DEFAULTCOMPONENT19</td></row>
		<row><td>BIN</td><td>ISX_DEFAULTCOMPONENT23</td></row>
		<row><td>BIN</td><td>ISX_DEFAULTCOMPONENT27</td></row>
		<row><td>BIN</td><td>ISX_DEFAULTCOMPONENT29</td></row>
		<row><td>BIN</td><td>JAWTAccessBridge_32.dll</td></row>
		<row><td>BIN</td><td>JavaAccessBridge_32.dll</td></row>
		<row><td>BIN</td><td>WindowsAccessBridge_32.dll</td></row>
		<row><td>BIN</td><td>awt.dll</td></row>
		<row><td>BIN</td><td>bci.dll</td></row>
		<row><td>BIN</td><td>dcpr.dll</td></row>
		<row><td>BIN</td><td>decora_sse.dll</td></row>
		<row><td>BIN</td><td>deploy.dll</td></row>
		<row><td>BIN</td><td>dt_shmem.dll</td></row>
		<row><td>BIN</td><td>dt_socket.dll</td></row>
		<row><td>BIN</td><td>eula.dll</td></row>
		<row><td>BIN</td><td>fontmanager.dll</td></row>
		<row><td>BIN</td><td>fxplugins.dll</td></row>
		<row><td>BIN</td><td>glass.dll</td></row>
		<row><td>BIN</td><td>glib_lite.dll</td></row>
		<row><td>BIN</td><td>gstreamer_lite.dll</td></row>
		<row><td>BIN</td><td>hprof.dll</td></row>
		<row><td>BIN</td><td>instrument.dll</td></row>
		<row><td>BIN</td><td>j2pcsc.dll</td></row>
		<row><td>BIN</td><td>j2pkcs11.dll</td></row>
		<row><td>BIN</td><td>jaas_nt.dll</td></row>
		<row><td>BIN</td><td>jabswitch.exe</td></row>
		<row><td>BIN</td><td>java.dll</td></row>
		<row><td>BIN</td><td>java.exe</td></row>
		<row><td>BIN</td><td>java_crw_demo.dll</td></row>
		<row><td>BIN</td><td>java_rmi.exe</td></row>
		<row><td>BIN</td><td>javacpl.exe</td></row>
		<row><td>BIN</td><td>javafx_font.dll</td></row>
		<row><td>BIN</td><td>javafx_font_t2k.dll</td></row>
		<row><td>BIN</td><td>javafx_iio.dll</td></row>
		<row><td>BIN</td><td>javaw.exe</td></row>
		<row><td>BIN</td><td>javaws.exe</td></row>
		<row><td>BIN</td><td>jawt.dll</td></row>
		<row><td>BIN</td><td>jdwp.dll</td></row>
		<row><td>BIN</td><td>jfr.dll</td></row>
		<row><td>BIN</td><td>jfxmedia.dll</td></row>
		<row><td>BIN</td><td>jfxwebkit.dll</td></row>
		<row><td>BIN</td><td>jjs.exe</td></row>
		<row><td>BIN</td><td>jli.dll</td></row>
		<row><td>BIN</td><td>jp2iexp.dll</td></row>
		<row><td>BIN</td><td>jp2launcher.exe</td></row>
		<row><td>BIN</td><td>jp2native.dll</td></row>
		<row><td>BIN</td><td>jp2ssv.dll</td></row>
		<row><td>BIN</td><td>jpeg.dll</td></row>
		<row><td>BIN</td><td>jsdt.dll</td></row>
		<row><td>BIN</td><td>jsound.dll</td></row>
		<row><td>BIN</td><td>jsoundds.dll</td></row>
		<row><td>BIN</td><td>kcms.dll</td></row>
		<row><td>BIN</td><td>keytool.exe</td></row>
		<row><td>BIN</td><td>kinit.exe</td></row>
		<row><td>BIN</td><td>klist.exe</td></row>
		<row><td>BIN</td><td>ktab.exe</td></row>
		<row><td>BIN</td><td>lcms.dll</td></row>
		<row><td>BIN</td><td>management.dll</td></row>
		<row><td>BIN</td><td>mlib_image.dll</td></row>
		<row><td>BIN</td><td>msvcp120.dll</td></row>
		<row><td>BIN</td><td>msvcr100.dll</td></row>
		<row><td>BIN</td><td>msvcr120.dll</td></row>
		<row><td>BIN</td><td>net.dll</td></row>
		<row><td>BIN</td><td>nio.dll</td></row>
		<row><td>BIN</td><td>npt.dll</td></row>
		<row><td>BIN</td><td>orbd.exe</td></row>
		<row><td>BIN</td><td>pack200.exe</td></row>
		<row><td>BIN</td><td>policytool.exe</td></row>
		<row><td>BIN</td><td>prism_common.dll</td></row>
		<row><td>BIN</td><td>prism_d3d.dll</td></row>
		<row><td>BIN</td><td>prism_sw.dll</td></row>
		<row><td>BIN</td><td>resource.dll</td></row>
		<row><td>BIN</td><td>rmid.exe</td></row>
		<row><td>BIN</td><td>rmiregistry.exe</td></row>
		<row><td>BIN</td><td>servertool.exe</td></row>
		<row><td>BIN</td><td>splashscreen.dll</td></row>
		<row><td>BIN</td><td>ssv.dll</td></row>
		<row><td>BIN</td><td>ssvagent.exe</td></row>
		<row><td>BIN</td><td>sunec.dll</td></row>
		<row><td>BIN</td><td>sunmscapi.dll</td></row>
		<row><td>BIN</td><td>t2k.dll</td></row>
		<row><td>BIN</td><td>tnameserv.exe</td></row>
		<row><td>BIN</td><td>unpack.dll</td></row>
		<row><td>BIN</td><td>unpack200.exe</td></row>
		<row><td>BIN</td><td>verify.dll</td></row>
		<row><td>BIN</td><td>w2k_lsa_auth.dll</td></row>
		<row><td>BIN</td><td>wsdetect.dll</td></row>
		<row><td>BIN</td><td>zip.dll</td></row>
		<row><td>CLIENT</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>CLIENT</td><td>ISX_DEFAULTCOMPONENT21</td></row>
		<row><td>CLIENT</td><td>ISX_DEFAULTCOMPONENT22</td></row>
		<row><td>CLIENT</td><td>jvm.dll</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT38</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT39</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT40</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT41</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT42</td></row>
		<row><td>CMM</td><td>ISX_DEFAULTCOMPONENT43</td></row>
		<row><td>COMMON</td><td>CDFC.PInvoke.dll</td></row>
		<row><td>COMMON</td><td>CDFC.Previewers.Contracts.dll</td></row>
		<row><td>COMMON</td><td>CDFCCultures.dll</td></row>
		<row><td>COMMON</td><td>CDFCUIContracts.dll1</td></row>
		<row><td>COMMON</td><td>EventLogger.dll</td></row>
		<row><td>COMMON</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>COMMON</td><td>Singularity.UI.Infrastructure.dll</td></row>
		<row><td>CONTROLS</td><td>CDFCControls.dll</td></row>
		<row><td>CONTROLS</td><td>CDFCHexaEditor.dll</td></row>
		<row><td>CONTROLS</td><td>CDFCMessageBoxes.dll</td></row>
		<row><td>CONTROLS</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>CONTROLS</td><td>Ookii.Dialogs.Wpf.dll</td></row>
		<row><td>CONTROLS</td><td>Singularity.Previewers.dll</td></row>
		<row><td>CONTROLS</td><td>Singularity.UI.AdbViewer.dll</td></row>
		<row><td>CONTROLS</td><td>Singularity.UI.Controls.dll</td></row>
		<row><td>CONTROLS</td><td>Singularity.UI.Converters.dll</td></row>
		<row><td>CONTROLS</td><td>Singularity.UI.MessageBoxes.dll</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT100</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT101</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT102</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT103</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT95</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT96</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT97</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT98</td></row>
		<row><td>CURSORS</td><td>ISX_DEFAULTCOMPONENT99</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT46</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT47</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT48</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT49</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT50</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT51</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT52</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT53</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT54</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT55</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT56</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT57</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT58</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT59</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT60</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT61</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT62</td></row>
		<row><td>DEPLOY</td><td>ISX_DEFAULTCOMPONENT63</td></row>
		<row><td>DTPLUGIN</td><td>ISX_DEFAULTCOMPONENT24</td></row>
		<row><td>DTPLUGIN</td><td>deployJava1.dll</td></row>
		<row><td>DTPLUGIN</td><td>npdeployJava1.dll</td></row>
		<row><td>ENTITIES</td><td>CDFC.Parse.Android.dll</td></row>
		<row><td>ENTITIES</td><td>CDFC.Parse.Local.dll</td></row>
		<row><td>ENTITIES</td><td>CDFC.Parse.Signature.dll</td></row>
		<row><td>ENTITIES</td><td>CDFC.Parse.dll</td></row>
		<row><td>ENTITIES</td><td>CDFC.Singularity.Forensics.dll</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT12</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT16</td></row>
		<row><td>ENTITIES</td><td>ISX_DEFAULTCOMPONENT17</td></row>
		<row><td>ENTITIES</td><td>SingularityForensic.dll</td></row>
		<row><td>ENTITIES</td><td>UserMsgUs.dll</td></row>
		<row><td>ENTITIES</td><td>cdfcphoto.dll</td></row>
		<row><td>ENTITIES</td><td>cdfcqd.dll1</td></row>
		<row><td>EN_US</td><td>ISX_DEFAULTCOMPONENT148</td></row>
		<row><td>EN_US</td><td>ISX_DEFAULTCOMPONENT149</td></row>
		<row><td>EN_US</td><td>ISX_DEFAULTCOMPONENT150</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT65</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT66</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT67</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT68</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT69</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT70</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT71</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT72</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT73</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT74</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT75</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT76</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT77</td></row>
		<row><td>EXT</td><td>ISX_DEFAULTCOMPONENT78</td></row>
		<row><td>FILE</td><td>ISX_DEFAULTCOMPONENT25</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT82</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT83</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT84</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT85</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT86</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT87</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT88</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT89</td></row>
		<row><td>FONTS</td><td>ISX_DEFAULTCOMPONENT90</td></row>
		<row><td>I386</td><td>ISX_DEFAULTCOMPONENT92</td></row>
		<row><td>I386</td><td>ISX_DEFAULTCOMPONENT93</td></row>
		<row><td>IMAGES</td><td>ISX_DEFAULTCOMPONENT94</td></row>
		<row><td>INSTALLDIR</td><td>AdbWinApi.dll</td></row>
		<row><td>INSTALLDIR</td><td>AdbWinUsbApi.dll</td></row>
		<row><td>INSTALLDIR</td><td>BouncyCastle.Crypto.dll2</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.PInvoke.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Parse.Android.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Parse.Local.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Parse.Signature.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Parse.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Previewers.Contracts.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFC.Singularity.Forensics.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFCControls.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFCCultures.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFCHexaEditor.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFCMessageBoxes.dll</td></row>
		<row><td>INSTALLDIR</td><td>CDFCUIContracts.dll1</td></row>
		<row><td>INSTALLDIR</td><td>CDFCUIContracts.dll3</td></row>
		<row><td>INSTALLDIR</td><td>Cflab.DataTransport.dll2</td></row>
		<row><td>INSTALLDIR</td><td>DirectOutIn.dll2</td></row>
		<row><td>INSTALLDIR</td><td>EventLogger.dll</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT100</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT101</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT102</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT103</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT104</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT105</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT106</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT107</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT108</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT109</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT110</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT111</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT112</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT113</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT114</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT115</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT116</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT117</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT118</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT119</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT12</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT120</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT121</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT122</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT123</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT124</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT125</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT126</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT127</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT128</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT129</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT130</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT131</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT132</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT133</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT134</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT135</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT136</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT137</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT138</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT139</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT140</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT141</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT142</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT143</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT144</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT145</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT146</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT147</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT148</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT149</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT150</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT155</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT156</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT157</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT158</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT159</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT16</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT163</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT169</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT17</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT170</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT171</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT172</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT173</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT183</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT184</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT185</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT186</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT187</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT188</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT189</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT19</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT190</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT191</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT192</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT193</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT194</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT195</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT196</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT197</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT198</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT199</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT200</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT201</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT202</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT203</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT204</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT205</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT206</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT207</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT21</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT22</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT23</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT24</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT25</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT26</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT27</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT28</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT29</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT30</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT33</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT34</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT35</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT36</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT37</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT38</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT39</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT4</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT40</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT41</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT42</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT43</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT44</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT45</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT46</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT47</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT48</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT49</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT50</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT51</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT52</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT53</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT54</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT55</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT56</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT57</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT58</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT59</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT60</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT61</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT62</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT63</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT64</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT65</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT66</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT67</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT68</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT69</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT70</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT71</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT72</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT73</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT74</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT75</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT76</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT77</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT78</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT79</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT80</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT81</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT82</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT83</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT84</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT85</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT86</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT87</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT88</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT89</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT90</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT91</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT92</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT93</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT94</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT95</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT96</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT97</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT98</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT99</td></row>
		<row><td>INSTALLDIR</td><td>IS_ININSTALL_SHORTCUT</td></row>
		<row><td>INSTALLDIR</td><td>JAWTAccessBridge_32.dll</td></row>
		<row><td>INSTALLDIR</td><td>JavaAccessBridge_32.dll</td></row>
		<row><td>INSTALLDIR</td><td>MahApps.Metro.dll1</td></row>
		<row><td>INSTALLDIR</td><td>MahApps.Metro.dll3</td></row>
		<row><td>INSTALLDIR</td><td>Microsoft.Practices.ServiceLocation.dll2</td></row>
		<row><td>INSTALLDIR</td><td>Newtonsoft.Json.dll2</td></row>
		<row><td>INSTALLDIR</td><td>Ookii.Dialogs.Wpf.dll</td></row>
		<row><td>INSTALLDIR</td><td>Prism.Mef.Wpf.dll2</td></row>
		<row><td>INSTALLDIR</td><td>Prism.Wpf.dll2</td></row>
		<row><td>INSTALLDIR</td><td>Prism.dll2</td></row>
		<row><td>INSTALLDIR</td><td>SQLite.Interop.dll</td></row>
		<row><td>INSTALLDIR</td><td>SQLite.Interop.dll1</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.Fonts.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.Previewers.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.AdbViewer.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.Controls.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.Converters.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.Info.dll2</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.Infrastructure.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.MessageBoxes.dll</td></row>
		<row><td>INSTALLDIR</td><td>Singularity.UI.Themes.dll</td></row>
		<row><td>INSTALLDIR</td><td>SingularityForensic.dll</td></row>
		<row><td>INSTALLDIR</td><td>SingularityShell.exe2</td></row>
		<row><td>INSTALLDIR</td><td>SingularityShell.vshost.exe2</td></row>
		<row><td>INSTALLDIR</td><td>System.Data.Sqlite.dll2</td></row>
		<row><td>INSTALLDIR</td><td>System.Windows.Interactivity.dll2</td></row>
		<row><td>INSTALLDIR</td><td>UserMsgUs.dll</td></row>
		<row><td>INSTALLDIR</td><td>WindowsAccessBridge_32.dll</td></row>
		<row><td>INSTALLDIR</td><td>adb.exe</td></row>
		<row><td>INSTALLDIR</td><td>awt.dll</td></row>
		<row><td>INSTALLDIR</td><td>bci.dll</td></row>
		<row><td>INSTALLDIR</td><td>cdfcphoto.dll</td></row>
		<row><td>INSTALLDIR</td><td>cdfcqd.dll1</td></row>
		<row><td>INSTALLDIR</td><td>cdfcqd.dll3</td></row>
		<row><td>INSTALLDIR</td><td>dcpr.dll</td></row>
		<row><td>INSTALLDIR</td><td>debmp.dll</td></row>
		<row><td>INSTALLDIR</td><td>decora_sse.dll</td></row>
		<row><td>INSTALLDIR</td><td>dehex.dll</td></row>
		<row><td>INSTALLDIR</td><td>deploy.dll</td></row>
		<row><td>INSTALLDIR</td><td>deployJava1.dll</td></row>
		<row><td>INSTALLDIR</td><td>dess.dll</td></row>
		<row><td>INSTALLDIR</td><td>detree.dll</td></row>
		<row><td>INSTALLDIR</td><td>devect.dll</td></row>
		<row><td>INSTALLDIR</td><td>dewp.dll</td></row>
		<row><td>INSTALLDIR</td><td>dt_shmem.dll</td></row>
		<row><td>INSTALLDIR</td><td>dt_socket.dll</td></row>
		<row><td>INSTALLDIR</td><td>eula.dll</td></row>
		<row><td>INSTALLDIR</td><td>fontmanager.dll</td></row>
		<row><td>INSTALLDIR</td><td>fxplugins.dll</td></row>
		<row><td>INSTALLDIR</td><td>glass.dll</td></row>
		<row><td>INSTALLDIR</td><td>glib_lite.dll</td></row>
		<row><td>INSTALLDIR</td><td>gstreamer_lite.dll</td></row>
		<row><td>INSTALLDIR</td><td>hprof.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibfpx2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibgp42.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibjpg2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibpcd2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibpsd2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibxbm2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibxpm2.dll</td></row>
		<row><td>INSTALLDIR</td><td>ibxwd2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd32.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd42.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd52.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd62.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd72.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcd82.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcdr2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcm52.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcm72.dll</td></row>
		<row><td>INSTALLDIR</td><td>imcmx2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imdsf2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imfmv2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imgdf2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imgem2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imigs2.dll</td></row>
		<row><td>INSTALLDIR</td><td>immet2.dll</td></row>
		<row><td>INSTALLDIR</td><td>impif2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imps_2.dll</td></row>
		<row><td>INSTALLDIR</td><td>impsi2.dll</td></row>
		<row><td>INSTALLDIR</td><td>impsz2.dll</td></row>
		<row><td>INSTALLDIR</td><td>imrnd2.dll</td></row>
		<row><td>INSTALLDIR</td><td>instrument.dll</td></row>
		<row><td>INSTALLDIR</td><td>iphgw2.dll</td></row>
		<row><td>INSTALLDIR</td><td>isgdi32.dll</td></row>
		<row><td>INSTALLDIR</td><td>j2pcsc.dll</td></row>
		<row><td>INSTALLDIR</td><td>j2pkcs11.dll</td></row>
		<row><td>INSTALLDIR</td><td>jaas_nt.dll</td></row>
		<row><td>INSTALLDIR</td><td>jabswitch.exe</td></row>
		<row><td>INSTALLDIR</td><td>java.dll</td></row>
		<row><td>INSTALLDIR</td><td>java.exe</td></row>
		<row><td>INSTALLDIR</td><td>java_crw_demo.dll</td></row>
		<row><td>INSTALLDIR</td><td>java_rmi.exe</td></row>
		<row><td>INSTALLDIR</td><td>javacpl.exe</td></row>
		<row><td>INSTALLDIR</td><td>javafx_font.dll</td></row>
		<row><td>INSTALLDIR</td><td>javafx_font_t2k.dll</td></row>
		<row><td>INSTALLDIR</td><td>javafx_iio.dll</td></row>
		<row><td>INSTALLDIR</td><td>javaw.exe</td></row>
		<row><td>INSTALLDIR</td><td>javaws.exe</td></row>
		<row><td>INSTALLDIR</td><td>jawt.dll</td></row>
		<row><td>INSTALLDIR</td><td>jdwp.dll</td></row>
		<row><td>INSTALLDIR</td><td>jfr.dll</td></row>
		<row><td>INSTALLDIR</td><td>jfxmedia.dll</td></row>
		<row><td>INSTALLDIR</td><td>jfxwebkit.dll</td></row>
		<row><td>INSTALLDIR</td><td>jjs.exe</td></row>
		<row><td>INSTALLDIR</td><td>jli.dll</td></row>
		<row><td>INSTALLDIR</td><td>jp2iexp.dll</td></row>
		<row><td>INSTALLDIR</td><td>jp2launcher.exe</td></row>
		<row><td>INSTALLDIR</td><td>jp2native.dll</td></row>
		<row><td>INSTALLDIR</td><td>jp2ssv.dll</td></row>
		<row><td>INSTALLDIR</td><td>jpeg.dll</td></row>
		<row><td>INSTALLDIR</td><td>jsdt.dll</td></row>
		<row><td>INSTALLDIR</td><td>jsound.dll</td></row>
		<row><td>INSTALLDIR</td><td>jsoundds.dll</td></row>
		<row><td>INSTALLDIR</td><td>jvm.dll</td></row>
		<row><td>INSTALLDIR</td><td>kcms.dll</td></row>
		<row><td>INSTALLDIR</td><td>keytool.exe</td></row>
		<row><td>INSTALLDIR</td><td>kinit.exe</td></row>
		<row><td>INSTALLDIR</td><td>klist.exe</td></row>
		<row><td>INSTALLDIR</td><td>ktab.exe</td></row>
		<row><td>INSTALLDIR</td><td>lcms.dll</td></row>
		<row><td>INSTALLDIR</td><td>management.dll</td></row>
		<row><td>INSTALLDIR</td><td>mlib_image.dll</td></row>
		<row><td>INSTALLDIR</td><td>msvcp120.dll</td></row>
		<row><td>INSTALLDIR</td><td>msvcr100.dll</td></row>
		<row><td>INSTALLDIR</td><td>msvcr100.dll1</td></row>
		<row><td>INSTALLDIR</td><td>msvcr120.dll</td></row>
		<row><td>INSTALLDIR</td><td>net.dll</td></row>
		<row><td>INSTALLDIR</td><td>nio.dll</td></row>
		<row><td>INSTALLDIR</td><td>npdeployJava1.dll</td></row>
		<row><td>INSTALLDIR</td><td>npjp2.dll</td></row>
		<row><td>INSTALLDIR</td><td>npt.dll</td></row>
		<row><td>INSTALLDIR</td><td>ocemul.dll</td></row>
		<row><td>INSTALLDIR</td><td>orbd.exe</td></row>
		<row><td>INSTALLDIR</td><td>oswin32.dll</td></row>
		<row><td>INSTALLDIR</td><td>pack200.exe</td></row>
		<row><td>INSTALLDIR</td><td>policytool.exe</td></row>
		<row><td>INSTALLDIR</td><td>prism_common.dll</td></row>
		<row><td>INSTALLDIR</td><td>prism_d3d.dll</td></row>
		<row><td>INSTALLDIR</td><td>prism_sw.dll</td></row>
		<row><td>INSTALLDIR</td><td>resource.dll</td></row>
		<row><td>INSTALLDIR</td><td>rmid.exe</td></row>
		<row><td>INSTALLDIR</td><td>rmiregistry.exe</td></row>
		<row><td>INSTALLDIR</td><td>sccanno.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccca.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccch.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccda.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccdu.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccfa.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccfi.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccfmt.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccfnt.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccfut.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccind.dll</td></row>
		<row><td>INSTALLDIR</td><td>scclo.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccole.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccra.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccsd.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccta.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccut.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccvw.dll</td></row>
		<row><td>INSTALLDIR</td><td>sccxt.dll</td></row>
		<row><td>INSTALLDIR</td><td>servertool.exe</td></row>
		<row><td>INSTALLDIR</td><td>splashscreen.dll</td></row>
		<row><td>INSTALLDIR</td><td>ssv.dll</td></row>
		<row><td>INSTALLDIR</td><td>ssvagent.exe</td></row>
		<row><td>INSTALLDIR</td><td>sunec.dll</td></row>
		<row><td>INSTALLDIR</td><td>sunmscapi.dll</td></row>
		<row><td>INSTALLDIR</td><td>t2k.dll</td></row>
		<row><td>INSTALLDIR</td><td>tnameserv.exe</td></row>
		<row><td>INSTALLDIR</td><td>unpack.dll</td></row>
		<row><td>INSTALLDIR</td><td>unpack200.exe</td></row>
		<row><td>INSTALLDIR</td><td>verify.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsacad.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsacs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsami.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsarc.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsasf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsbdr.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsbmp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vscdrx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vscgm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdbs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdez.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdif.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdrw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdxla.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsdxlm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsemf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsen4.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsens.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsenw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vseshr.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsexe2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsfax.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsfcd.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsfcs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsfft.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsflw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsfwk.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsgdsf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsgif.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsgzip.dll</td></row>
		<row><td>INSTALLDIR</td><td>vshgs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vshtml.dll</td></row>
		<row><td>INSTALLDIR</td><td>vshwp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vshwp2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsich.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsich6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsid3.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsimg.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsiwok.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsiwok13.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsiwon.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsiwop.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsiwp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsjbg2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsjp2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsjw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsleg.dll</td></row>
		<row><td>INSTALLDIR</td><td>vslwp7.dll</td></row>
		<row><td>INSTALLDIR</td><td>vslzh.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsm11.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmanu.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmbox.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmcw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmdb.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmif.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmime.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmm4.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmmfn.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmpp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmsg.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmsw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmwkd.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmwks.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmwp2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmwpf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsmwrk.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsnsf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsolm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsone.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsow.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspbm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspcl.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspcx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspdf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspdfi.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspdx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspfs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspgl.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspic.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspict.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspng.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspntg.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspp12.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspp2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspp7.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspp97.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsppl.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspsp6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspst.dll</td></row>
		<row><td>INSTALLDIR</td><td>vspstf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsqa.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsqad.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsqp6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsqp9.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsqt.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsrar.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsras.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsrbs.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsrft.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsrfx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsriff.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsrtf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssam.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssc5.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssdw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsshw3.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssmd.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssms.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssmt.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssnap.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsso6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssoc.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssoc6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssoi.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssoi6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vssow.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsspt.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsssml.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsswf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstaz.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstext.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstga.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstif6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vstxt.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsvcrd.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsviso.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsvsdx.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsvw3.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsw12.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsw6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsw97.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswbmp.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswg2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswk4.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswk6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswks.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswm.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswmf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswml.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsword.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswork.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswp5.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswp6.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpf.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpg.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpg2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpl.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpml.dll</td></row>
		<row><td>INSTALLDIR</td><td>vswpw.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsws.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsws2.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxl12.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxl5.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxlsb.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxml.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxps.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsxy.dll</td></row>
		<row><td>INSTALLDIR</td><td>vsyim.dll</td></row>
		<row><td>INSTALLDIR</td><td>vszip.dll</td></row>
		<row><td>INSTALLDIR</td><td>w2k_lsa_auth.dll</td></row>
		<row><td>INSTALLDIR</td><td>wsdetect.dll</td></row>
		<row><td>INSTALLDIR</td><td>wvcore.dll</td></row>
		<row><td>INSTALLDIR</td><td>zip.dll</td></row>
		<row><td>JFR</td><td>ISX_DEFAULTCOMPONENT107</td></row>
		<row><td>JFR</td><td>ISX_DEFAULTCOMPONENT108</td></row>
		<row><td>JFR</td><td>ISX_DEFAULTCOMPONENT109</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT141</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT142</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT143</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT144</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT145</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT146</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>JRE</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>LANGUAGES</td><td>ISX_DEFAULTCOMPONENT147</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT104</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT105</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT106</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT110</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT111</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT112</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT113</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT114</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT120</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT121</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT122</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT123</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT124</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT125</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT126</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT127</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT138</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT139</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT140</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT33</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT35</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT36</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT37</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT44</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT45</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT64</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT79</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT80</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT81</td></row>
		<row><td>LIB</td><td>ISX_DEFAULTCOMPONENT91</td></row>
		<row><td>MANAGEMENT</td><td>ISX_DEFAULTCOMPONENT115</td></row>
		<row><td>MANAGEMENT</td><td>ISX_DEFAULTCOMPONENT116</td></row>
		<row><td>MANAGEMENT</td><td>ISX_DEFAULTCOMPONENT117</td></row>
		<row><td>MANAGEMENT</td><td>ISX_DEFAULTCOMPONENT118</td></row>
		<row><td>MANAGEMENT</td><td>ISX_DEFAULTCOMPONENT119</td></row>
		<row><td>OUTSIDEIN</td><td>ISX_DEFAULTCOMPONENT155</td></row>
		<row><td>OUTSIDEIN</td><td>ISX_DEFAULTCOMPONENT156</td></row>
		<row><td>OUTSIDEIN</td><td>ISX_DEFAULTCOMPONENT157</td></row>
		<row><td>OUTSIDEIN</td><td>ISX_DEFAULTCOMPONENT158</td></row>
		<row><td>OUTSIDEIN</td><td>ISX_DEFAULTCOMPONENT159</td></row>
		<row><td>OUTSIDEIN</td><td>debmp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>dehex.dll</td></row>
		<row><td>OUTSIDEIN</td><td>dess.dll</td></row>
		<row><td>OUTSIDEIN</td><td>detree.dll</td></row>
		<row><td>OUTSIDEIN</td><td>devect.dll</td></row>
		<row><td>OUTSIDEIN</td><td>dewp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibfpx2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibgp42.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibjpg2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibpcd2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibpsd2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibxbm2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibxpm2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ibxwd2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd32.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd42.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd52.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd62.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd72.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcd82.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcdr2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcm52.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcm72.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imcmx2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imdsf2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imfmv2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imgdf2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imgem2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imigs2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>immet2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>impif2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imps_2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>impsi2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>impsz2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>imrnd2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>iphgw2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>isgdi32.dll</td></row>
		<row><td>OUTSIDEIN</td><td>ocemul.dll</td></row>
		<row><td>OUTSIDEIN</td><td>oswin32.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccanno.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccca.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccch.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccda.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccdu.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccfa.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccfi.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccfmt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccfnt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccfut.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccind.dll</td></row>
		<row><td>OUTSIDEIN</td><td>scclo.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccole.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccra.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccsd.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccta.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccut.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccvw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>sccxt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsacad.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsacs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsami.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsarc.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsasf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsbdr.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsbmp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vscdrx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vscgm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdbs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdez.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdif.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdrw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdxla.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsdxlm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsemf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsen4.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsens.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsenw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vseshr.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsexe2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsfax.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsfcd.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsfcs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsfft.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsflw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsfwk.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsgdsf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsgif.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsgzip.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vshgs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vshtml.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vshwp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vshwp2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsich.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsich6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsid3.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsimg.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsiwok.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsiwok13.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsiwon.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsiwop.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsiwp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsjbg2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsjp2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsjw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsleg.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vslwp7.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vslzh.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsm11.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmanu.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmbox.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmcw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmdb.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmif.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmime.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmm4.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmmfn.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmpp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmsg.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmsw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmwkd.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmwks.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmwp2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmwpf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsmwrk.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsnsf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsolm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsone.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsow.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspbm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspcl.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspcx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspdf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspdfi.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspdx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspfs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspgl.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspic.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspict.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspng.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspntg.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspp12.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspp2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspp7.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspp97.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsppl.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspsp6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspst.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vspstf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsqa.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsqad.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsqp6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsqp9.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsqt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsrar.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsras.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsrbs.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsrft.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsrfx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsriff.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsrtf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssam.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssc5.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssdw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsshw3.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssmd.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssms.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssmt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssnap.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsso6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssoc.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssoc6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssoi.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssoi6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vssow.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsspt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsssml.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsswf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstaz.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstext.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstga.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstif6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vstxt.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsvcrd.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsviso.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsvsdx.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsvw3.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsw12.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsw6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsw97.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswbmp.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswg2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswk4.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswk6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswks.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswm.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswmf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswml.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsword.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswork.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswp5.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswp6.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpf.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpg.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpg2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpl.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpml.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vswpw.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsws.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsws2.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxl12.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxl5.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxlsb.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxml.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxps.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsxy.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vsyim.dll</td></row>
		<row><td>OUTSIDEIN</td><td>vszip.dll</td></row>
		<row><td>OUTSIDEIN</td><td>wvcore.dll</td></row>
		<row><td>PLUGIN2</td><td>ISX_DEFAULTCOMPONENT28</td></row>
		<row><td>PLUGIN2</td><td>msvcr100.dll1</td></row>
		<row><td>PLUGIN2</td><td>npjp2.dll</td></row>
		<row><td>ProgramFiles64Folder</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>RESOURCES</td><td>ISX_DEFAULTCOMPONENT163</td></row>
		<row><td>RESOURCES</td><td>MahApps.Metro.dll1</td></row>
		<row><td>RESOURCES</td><td>Singularity.Fonts.dll</td></row>
		<row><td>RESOURCES</td><td>Singularity.UI.Themes.dll</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT128</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT129</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT130</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT131</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT132</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT133</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT134</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT135</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT136</td></row>
		<row><td>SECURITY</td><td>ISX_DEFAULTCOMPONENT137</td></row>
		<row><td>SERVER</td><td>ISX_DEFAULTCOMPONENT30</td></row>
		<row><td>TOOLS</td><td>ISX_DEFAULTCOMPONENT169</td></row>
		<row><td>WINRAR</td><td>ISX_DEFAULTCOMPONENT26</td></row>
		<row><td>X64</td><td>ISX_DEFAULTCOMPONENT183</td></row>
		<row><td>X64</td><td>SQLite.Interop.dll</td></row>
		<row><td>X86</td><td>ISX_DEFAULTCOMPONENT184</td></row>
		<row><td>X86</td><td>SQLite.Interop.dll1</td></row>
	</table>

	<table name="CustomAction">
		<col key="yes" def="s72">Action</col>
		<col def="i2">Type</col>
		<col def="S64">Source</col>
		<col def="S0">Target</col>
		<col def="I4">ExtendedType</col>
		<col def="S255">ISComments</col>
		<row><td>ISPreventDowngrade</td><td>19</td><td/><td>[IS_PREVENT_DOWNGRADE_EXIT]</td><td/><td>Exits install when a newer version of this product is found</td></row>
		<row><td>ISPrint</td><td>1</td><td>SetAllUsers.dll</td><td>PrintScrollableText</td><td/><td>Prints the contents of a ScrollableText control on a dialog.</td></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>1</td><td>ISExpHlp.dll</td><td>RunSetupTypeAddLocalEvent</td><td/><td>Run the AddLocal events associated with the Next button on the Setup Type dialog.</td></row>
		<row><td>ISSelfRegisterCosting</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterCosting</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFiles</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFinalize</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISUnSelfRegisterFiles</td><td/><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td>51</td><td>ARPINSTALLLOCATION</td><td>[INSTALLDIR]</td><td/><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%SystemRoot]\Profiles\All Users</td><td/><td/></row>
		<row><td>ShowMsiLog</td><td>226</td><td>SystemFolder</td><td>[SystemFolder]notepad.exe "[MsiLogFileLocation]"</td><td/><td>Shows Property-driven MSI Log</td></row>
		<row><td>setAllUsersProfile2K</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%ALLUSERSPROFILE]</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>51</td><td>USERPROFILE</td><td>[%USERPROFILE]</td><td/><td/></row>
	</table>

	<table name="Dialog">
		<col key="yes" def="s72">Dialog</col>
		<col def="i2">HCentering</col>
		<col def="i2">VCentering</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L128">Title</col>
		<col def="s50">Control_First</col>
		<col def="S50">Control_Default</col>
		<col def="S50">Control_Cancel</col>
		<col def="S255">ISComments</col>
		<col def="S72">TextStyle_</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISResourceId</col>
		<row><td>AdminChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Install Point Browse</td><td/><td>0</td><td/></row>
		<row><td>AdminNetworkLocation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Network Location</td><td/><td>0</td><td/></row>
		<row><td>AdminWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Administration Welcome</td><td/><td>0</td><td/></row>
		<row><td>CancelSetup</td><td>50</td><td>50</td><td>260</td><td>85</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>No</td><td>No</td><td>No</td><td>Cancel</td><td/><td>0</td><td/></row>
		<row><td>CustomSetup</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tree</td><td>Next</td><td>Cancel</td><td>Custom Selection</td><td/><td>0</td><td/></row>
		<row><td>CustomSetupTips</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Custom Setup Tips</td><td/><td>0</td><td/></row>
		<row><td>CustomerInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>NameEdit</td><td>Next</td><td>Cancel</td><td>Identification</td><td/><td>0</td><td/></row>
		<row><td>DatabaseFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Database Folder</td><td/><td>0</td><td/></row>
		<row><td>DestinationFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Destination Folder</td><td/><td>0</td><td/></row>
		<row><td>DiskSpaceRequirements</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Feature Details</td><td/><td>0</td><td/></row>
		<row><td>FilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Retry</td><td>Retry</td><td>Exit</td><td>Files in Use</td><td/><td>0</td><td/></row>
		<row><td>InstallChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Browse</td><td/><td>0</td><td/></row>
		<row><td>InstallWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Panel</td><td/><td>0</td><td/></row>
		<row><td>LicenseAgreement</td><td>50</td><td>50</td><td>374</td><td>266</td><td>2</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Agree</td><td>Next</td><td>Cancel</td><td>License Agreement</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Change, Reinstall, Remove</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Maintenance Welcome</td><td/><td>0</td><td/></row>
		<row><td>MsiRMFilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>Cancel</td><td>RestartManager Files in Use</td><td/><td>0</td><td/></row>
		<row><td>OutOfSpace</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Resume</td><td>Resume</td><td>Resume</td><td>Out Of Disk Space</td><td/><td>0</td><td/></row>
		<row><td>PatchWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS__IsPatchDlg_PatchWizard##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Patch Panel</td><td/><td>0</td><td/></row>
		<row><td>ReadmeInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>7</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Readme Information</td><td/><td>0</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Ready to Install</td><td/><td>0</td><td/></row>
		<row><td>ReadyToRemove</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RemoveNow</td><td>RemoveNow</td><td>Cancel</td><td>Verify Remove</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteError</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>Fatal Error</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupError</td><td>50</td><td>50</td><td>270</td><td>110</td><td>65543</td><td>##IDS__IsErrorDlg_InstallerInfo##</td><td>ErrorText</td><td>O</td><td>C</td><td>Error</td><td/><td>0</td><td/></row>
		<row><td>SetupInitialization</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Setup Initialization</td><td/><td>0</td><td/></row>
		<row><td>SetupInterrupted</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>User Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupProgress</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Progress</td><td/><td>0</td><td/></row>
		<row><td>SetupResume</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Resume</td><td/><td>0</td><td/></row>
		<row><td>SetupType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Setup Type</td><td/><td>0</td><td/></row>
		<row><td>SplashBitmap</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Bitmap</td><td/><td>0</td><td/></row>
	</table>

	<table name="Directory">
		<col key="yes" def="s72">Directory</col>
		<col def="S72">Directory_Parent</col>
		<col def="l255">DefaultDir</col>
		<col def="S255">ISDescription</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISFolderName</col>
		<row><td>ADB</td><td>TOOLS</td><td>Adb</td><td/><td>0</td><td/></row>
		<row><td>ALLUSERSPROFILE</td><td>TARGETDIR</td><td>.:ALLUSE~1|All Users</td><td/><td>0</td><td/></row>
		<row><td>AMD64</td><td>LIB</td><td>amd64</td><td/><td>0</td><td/></row>
		<row><td>APK</td><td>TOOLS</td><td>Apk</td><td/><td>0</td><td/></row>
		<row><td>ATTACHMENTS</td><td>INSTALLDIR</td><td>ATTACH~1|Attachments</td><td/><td>0</td><td/></row>
		<row><td>AdminToolsFolder</td><td>TARGETDIR</td><td>.:Admint~1|AdminTools</td><td/><td>0</td><td/></row>
		<row><td>AppDataFolder</td><td>TARGETDIR</td><td>.:APPLIC~1|Application Data</td><td/><td>0</td><td/></row>
		<row><td>BIN</td><td>JRE</td><td>bin</td><td/><td>0</td><td/></row>
		<row><td>CDFC</td><td>ProgramFiles64Folder</td><td>CDFC</td><td/><td>0</td><td/></row>
		<row><td>CLIENT</td><td>BIN</td><td>client</td><td/><td>0</td><td/></row>
		<row><td>CMM</td><td>LIB</td><td>cmm</td><td/><td>0</td><td/></row>
		<row><td>COMMON</td><td>INSTALLDIR</td><td>Common</td><td/><td>0</td><td/></row>
		<row><td>CONTROLS</td><td>INSTALLDIR</td><td>Controls</td><td/><td>0</td><td/></row>
		<row><td>CURSORS</td><td>IMAGES</td><td>cursors</td><td/><td>0</td><td/></row>
		<row><td>CommonAppDataFolder</td><td>TARGETDIR</td><td>.:Common~1|CommonAppData</td><td/><td>0</td><td/></row>
		<row><td>CommonFiles64Folder</td><td>TARGETDIR</td><td>.:Common64</td><td/><td>0</td><td/></row>
		<row><td>CommonFilesFolder</td><td>TARGETDIR</td><td>.:Common</td><td/><td>0</td><td/></row>
		<row><td>DATABASEDIR</td><td>ISYourDataBaseDir</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>DEPLOY</td><td>LIB</td><td>deploy</td><td/><td>0</td><td/></row>
		<row><td>DIRECTORY</td><td>CDFC</td><td>流火手机取证~1|流火手机取证分析系统</td><td/><td>0</td><td/></row>
		<row><td>DIRECTORY1</td><td>CDFC</td><td>奇点取证十六~1|奇点取证十六进制版</td><td/><td>0</td><td/></row>
		<row><td>DIRECTORY2</td><td>CDFC</td><td>星云手机取证~1|星云手机取证分析系统</td><td/><td>0</td><td/></row>
		<row><td>DTPLUGIN</td><td>BIN</td><td>dtplugin</td><td/><td>0</td><td/></row>
		<row><td>DesktopFolder</td><td>TARGETDIR</td><td>.:Desktop</td><td/><td>3</td><td/></row>
		<row><td>ENTITIES</td><td>INSTALLDIR</td><td>Entities</td><td/><td>0</td><td/></row>
		<row><td>EN_US</td><td>LANGUAGES</td><td>en_US</td><td/><td>0</td><td/></row>
		<row><td>EXT</td><td>LIB</td><td>ext</td><td/><td>0</td><td/></row>
		<row><td>FILE</td><td>BIN</td><td>file</td><td/><td>0</td><td/></row>
		<row><td>FONTS</td><td>LIB</td><td>fonts</td><td/><td>0</td><td/></row>
		<row><td>FavoritesFolder</td><td>TARGETDIR</td><td>.:FAVORI~1|Favorites</td><td/><td>0</td><td/></row>
		<row><td>FontsFolder</td><td>TARGETDIR</td><td>.:Fonts</td><td/><td>0</td><td/></row>
		<row><td>GlobalAssemblyCache</td><td>TARGETDIR</td><td>.:Global~1|GlobalAssemblyCache</td><td/><td>0</td><td/></row>
		<row><td>I386</td><td>LIB</td><td>i386</td><td/><td>0</td><td/></row>
		<row><td>IMAGES</td><td>LIB</td><td>images</td><td/><td>0</td><td/></row>
		<row><td>INSTALLDIR</td><td>DIRECTORY</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>ISCommonFilesFolder</td><td>CommonFilesFolder</td><td>Instal~1|InstallShield</td><td/><td>0</td><td/></row>
		<row><td>ISMyCompanyDir</td><td>ProgramFilesFolder</td><td>MYCOMP~1|My Company Name</td><td/><td>0</td><td/></row>
		<row><td>ISMyProductDir</td><td>ISMyCompanyDir</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>ISYourDataBaseDir</td><td>INSTALLDIR</td><td>Database</td><td/><td>0</td><td/></row>
		<row><td>JFR</td><td>LIB</td><td>jfr</td><td/><td>0</td><td/></row>
		<row><td>JRE</td><td>INSTALLDIR</td><td>jre</td><td/><td>0</td><td/></row>
		<row><td>LANGUAGES</td><td>INSTALLDIR</td><td>LANGUA~1|Languages</td><td/><td>0</td><td/></row>
		<row><td>LIB</td><td>JRE</td><td>lib</td><td/><td>0</td><td/></row>
		<row><td>LocalAppDataFolder</td><td>TARGETDIR</td><td>.:LocalA~1|LocalAppData</td><td/><td>0</td><td/></row>
		<row><td>MANAGEMENT</td><td>LIB</td><td>MANAGE~1|management</td><td/><td>0</td><td/></row>
		<row><td>MY_PRODUCT_NAME</td><td>CDFC</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>MyPicturesFolder</td><td>TARGETDIR</td><td>.:MyPict~1|MyPictures</td><td/><td>0</td><td/></row>
		<row><td>NetHoodFolder</td><td>TARGETDIR</td><td>.:NetHood</td><td/><td>0</td><td/></row>
		<row><td>OUTSIDEIN</td><td>INSTALLDIR</td><td>OUTSID~1|OutSideIn</td><td/><td>0</td><td/></row>
		<row><td>PLUGIN2</td><td>BIN</td><td>plugin2</td><td/><td>0</td><td/></row>
		<row><td>PersonalFolder</td><td>TARGETDIR</td><td>.:Personal</td><td/><td>0</td><td/></row>
		<row><td>PrimaryVolumePath</td><td>TARGETDIR</td><td>.:Primar~1|PrimaryVolumePath</td><td/><td>0</td><td/></row>
		<row><td>PrintHoodFolder</td><td>TARGETDIR</td><td>.:PRINTH~1|PrintHood</td><td/><td>0</td><td/></row>
		<row><td>ProgramFiles64Folder</td><td>TARGETDIR</td><td>.:Prog64~1|Program Files 64</td><td/><td>0</td><td/></row>
		<row><td>ProgramFilesFolder</td><td>TARGETDIR</td><td>.:PROGRA~1|program files</td><td/><td>0</td><td/></row>
		<row><td>ProgramMenuFolder</td><td>TARGETDIR</td><td>.:Programs</td><td/><td>3</td><td/></row>
		<row><td>RESOURCES</td><td>INSTALLDIR</td><td>RESOUR~1|Resources</td><td/><td>0</td><td/></row>
		<row><td>RecentFolder</td><td>TARGETDIR</td><td>.:Recent</td><td/><td>0</td><td/></row>
		<row><td>SECURITY</td><td>LIB</td><td>security</td><td/><td>0</td><td/></row>
		<row><td>SERVER</td><td>BIN</td><td>server</td><td/><td>0</td><td/></row>
		<row><td>SendToFolder</td><td>TARGETDIR</td><td>.:SendTo</td><td/><td>3</td><td/></row>
		<row><td>StartMenuFolder</td><td>TARGETDIR</td><td>.:STARTM~1|Start Menu</td><td/><td>3</td><td/></row>
		<row><td>StartupFolder</td><td>TARGETDIR</td><td>.:StartUp</td><td/><td>3</td><td/></row>
		<row><td>System16Folder</td><td>TARGETDIR</td><td>.:System</td><td/><td>0</td><td/></row>
		<row><td>System64Folder</td><td>TARGETDIR</td><td>.:System64</td><td/><td>0</td><td/></row>
		<row><td>SystemFolder</td><td>TARGETDIR</td><td>.:System32</td><td/><td>0</td><td/></row>
		<row><td>TARGETDIR</td><td/><td>SourceDir</td><td/><td>0</td><td/></row>
		<row><td>TOOLS</td><td>INSTALLDIR</td><td>Tools</td><td/><td>0</td><td/></row>
		<row><td>TempFolder</td><td>TARGETDIR</td><td>.:Temp</td><td/><td>0</td><td/></row>
		<row><td>TemplateFolder</td><td>TARGETDIR</td><td>.:ShellNew</td><td/><td>0</td><td/></row>
		<row><td>USERPROFILE</td><td>TARGETDIR</td><td>.:USERPR~1|UserProfile</td><td/><td>0</td><td/></row>
		<row><td>WINRAR</td><td>FILE</td><td>WinRAR</td><td/><td>0</td><td/></row>
		<row><td>WindowsFolder</td><td>TARGETDIR</td><td>.:Windows</td><td/><td>0</td><td/></row>
		<row><td>WindowsVolume</td><td>TARGETDIR</td><td>.:WinRoot</td><td/><td>0</td><td/></row>
		<row><td>X64</td><td>INSTALLDIR</td><td>x64</td><td/><td>0</td><td/></row>
		<row><td>X86</td><td>INSTALLDIR</td><td>x86</td><td/><td>0</td><td/></row>
		<row><td>company_name</td><td>ProgramMenuFolder</td><td>天宇宁达</td><td/><td>1</td><td/></row>
		<row><td>company_name1</td><td>ProgramMenuFolder</td><td>公司名称</td><td/><td>1</td><td/></row>
		<row><td>product_name</td><td>company_name</td><td>奇点取~1|奇点取证十六进制版</td><td/><td>1</td><td/></row>
		<row><td>product_name1</td><td>company_name1</td><td>奇点取~1|奇点取证十六进制版</td><td/><td>1</td><td/></row>
		<row><td>product_name2</td><td>company_name</td><td>星云手~1|星云手机取证分析系统</td><td/><td>1</td><td/></row>
		<row><td>product_name3</td><td>company_name</td><td>流火手~1|流火手机取证分析系统</td><td/><td>1</td><td/></row>
	</table>

	<table name="DrLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col key="yes" def="S255">Path</col>
		<col def="I2">Depth</col>
	</table>

	<table name="DuplicateFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="s72">File_</col>
		<col def="L255">DestName</col>
		<col def="S72">DestFolder</col>
	</table>

	<table name="Environment">
		<col key="yes" def="s72">Environment</col>
		<col def="l255">Name</col>
		<col def="L255">Value</col>
		<col def="s72">Component_</col>
	</table>

	<table name="Error">
		<col key="yes" def="i2">Error</col>
		<col def="L255">Message</col>
		<row><td>0</td><td>##IDS_ERROR_0##</td></row>
		<row><td>1</td><td>##IDS_ERROR_1##</td></row>
		<row><td>10</td><td>##IDS_ERROR_8##</td></row>
		<row><td>11</td><td>##IDS_ERROR_9##</td></row>
		<row><td>1101</td><td>##IDS_ERROR_22##</td></row>
		<row><td>12</td><td>##IDS_ERROR_10##</td></row>
		<row><td>13</td><td>##IDS_ERROR_11##</td></row>
		<row><td>1301</td><td>##IDS_ERROR_23##</td></row>
		<row><td>1302</td><td>##IDS_ERROR_24##</td></row>
		<row><td>1303</td><td>##IDS_ERROR_25##</td></row>
		<row><td>1304</td><td>##IDS_ERROR_26##</td></row>
		<row><td>1305</td><td>##IDS_ERROR_27##</td></row>
		<row><td>1306</td><td>##IDS_ERROR_28##</td></row>
		<row><td>1307</td><td>##IDS_ERROR_29##</td></row>
		<row><td>1308</td><td>##IDS_ERROR_30##</td></row>
		<row><td>1309</td><td>##IDS_ERROR_31##</td></row>
		<row><td>1310</td><td>##IDS_ERROR_32##</td></row>
		<row><td>1311</td><td>##IDS_ERROR_33##</td></row>
		<row><td>1312</td><td>##IDS_ERROR_34##</td></row>
		<row><td>1313</td><td>##IDS_ERROR_35##</td></row>
		<row><td>1314</td><td>##IDS_ERROR_36##</td></row>
		<row><td>1315</td><td>##IDS_ERROR_37##</td></row>
		<row><td>1316</td><td>##IDS_ERROR_38##</td></row>
		<row><td>1317</td><td>##IDS_ERROR_39##</td></row>
		<row><td>1318</td><td>##IDS_ERROR_40##</td></row>
		<row><td>1319</td><td>##IDS_ERROR_41##</td></row>
		<row><td>1320</td><td>##IDS_ERROR_42##</td></row>
		<row><td>1321</td><td>##IDS_ERROR_43##</td></row>
		<row><td>1322</td><td>##IDS_ERROR_44##</td></row>
		<row><td>1323</td><td>##IDS_ERROR_45##</td></row>
		<row><td>1324</td><td>##IDS_ERROR_46##</td></row>
		<row><td>1325</td><td>##IDS_ERROR_47##</td></row>
		<row><td>1326</td><td>##IDS_ERROR_48##</td></row>
		<row><td>1327</td><td>##IDS_ERROR_49##</td></row>
		<row><td>1328</td><td>##IDS_ERROR_122##</td></row>
		<row><td>1329</td><td>##IDS_ERROR_1329##</td></row>
		<row><td>1330</td><td>##IDS_ERROR_1330##</td></row>
		<row><td>1331</td><td>##IDS_ERROR_1331##</td></row>
		<row><td>1332</td><td>##IDS_ERROR_1332##</td></row>
		<row><td>1333</td><td>##IDS_ERROR_1333##</td></row>
		<row><td>1334</td><td>##IDS_ERROR_1334##</td></row>
		<row><td>1335</td><td>##IDS_ERROR_1335##</td></row>
		<row><td>1336</td><td>##IDS_ERROR_1336##</td></row>
		<row><td>14</td><td>##IDS_ERROR_12##</td></row>
		<row><td>1401</td><td>##IDS_ERROR_50##</td></row>
		<row><td>1402</td><td>##IDS_ERROR_51##</td></row>
		<row><td>1403</td><td>##IDS_ERROR_52##</td></row>
		<row><td>1404</td><td>##IDS_ERROR_53##</td></row>
		<row><td>1405</td><td>##IDS_ERROR_54##</td></row>
		<row><td>1406</td><td>##IDS_ERROR_55##</td></row>
		<row><td>1407</td><td>##IDS_ERROR_56##</td></row>
		<row><td>1408</td><td>##IDS_ERROR_57##</td></row>
		<row><td>1409</td><td>##IDS_ERROR_58##</td></row>
		<row><td>1410</td><td>##IDS_ERROR_59##</td></row>
		<row><td>15</td><td>##IDS_ERROR_13##</td></row>
		<row><td>1500</td><td>##IDS_ERROR_60##</td></row>
		<row><td>1501</td><td>##IDS_ERROR_61##</td></row>
		<row><td>1502</td><td>##IDS_ERROR_62##</td></row>
		<row><td>1503</td><td>##IDS_ERROR_63##</td></row>
		<row><td>16</td><td>##IDS_ERROR_14##</td></row>
		<row><td>1601</td><td>##IDS_ERROR_64##</td></row>
		<row><td>1602</td><td>##IDS_ERROR_65##</td></row>
		<row><td>1603</td><td>##IDS_ERROR_66##</td></row>
		<row><td>1604</td><td>##IDS_ERROR_67##</td></row>
		<row><td>1605</td><td>##IDS_ERROR_68##</td></row>
		<row><td>1606</td><td>##IDS_ERROR_69##</td></row>
		<row><td>1607</td><td>##IDS_ERROR_70##</td></row>
		<row><td>1608</td><td>##IDS_ERROR_71##</td></row>
		<row><td>1609</td><td>##IDS_ERROR_1609##</td></row>
		<row><td>1651</td><td>##IDS_ERROR_1651##</td></row>
		<row><td>17</td><td>##IDS_ERROR_15##</td></row>
		<row><td>1701</td><td>##IDS_ERROR_72##</td></row>
		<row><td>1702</td><td>##IDS_ERROR_73##</td></row>
		<row><td>1703</td><td>##IDS_ERROR_74##</td></row>
		<row><td>1704</td><td>##IDS_ERROR_75##</td></row>
		<row><td>1705</td><td>##IDS_ERROR_76##</td></row>
		<row><td>1706</td><td>##IDS_ERROR_77##</td></row>
		<row><td>1707</td><td>##IDS_ERROR_78##</td></row>
		<row><td>1708</td><td>##IDS_ERROR_79##</td></row>
		<row><td>1709</td><td>##IDS_ERROR_80##</td></row>
		<row><td>1710</td><td>##IDS_ERROR_81##</td></row>
		<row><td>1711</td><td>##IDS_ERROR_82##</td></row>
		<row><td>1712</td><td>##IDS_ERROR_83##</td></row>
		<row><td>1713</td><td>##IDS_ERROR_123##</td></row>
		<row><td>1714</td><td>##IDS_ERROR_124##</td></row>
		<row><td>1715</td><td>##IDS_ERROR_1715##</td></row>
		<row><td>1716</td><td>##IDS_ERROR_1716##</td></row>
		<row><td>1717</td><td>##IDS_ERROR_1717##</td></row>
		<row><td>1718</td><td>##IDS_ERROR_1718##</td></row>
		<row><td>1719</td><td>##IDS_ERROR_1719##</td></row>
		<row><td>1720</td><td>##IDS_ERROR_1720##</td></row>
		<row><td>1721</td><td>##IDS_ERROR_1721##</td></row>
		<row><td>1722</td><td>##IDS_ERROR_1722##</td></row>
		<row><td>1723</td><td>##IDS_ERROR_1723##</td></row>
		<row><td>1724</td><td>##IDS_ERROR_1724##</td></row>
		<row><td>1725</td><td>##IDS_ERROR_1725##</td></row>
		<row><td>1726</td><td>##IDS_ERROR_1726##</td></row>
		<row><td>1727</td><td>##IDS_ERROR_1727##</td></row>
		<row><td>1728</td><td>##IDS_ERROR_1728##</td></row>
		<row><td>1729</td><td>##IDS_ERROR_1729##</td></row>
		<row><td>1730</td><td>##IDS_ERROR_1730##</td></row>
		<row><td>1731</td><td>##IDS_ERROR_1731##</td></row>
		<row><td>1732</td><td>##IDS_ERROR_1732##</td></row>
		<row><td>18</td><td>##IDS_ERROR_16##</td></row>
		<row><td>1801</td><td>##IDS_ERROR_84##</td></row>
		<row><td>1802</td><td>##IDS_ERROR_85##</td></row>
		<row><td>1803</td><td>##IDS_ERROR_86##</td></row>
		<row><td>1804</td><td>##IDS_ERROR_87##</td></row>
		<row><td>1805</td><td>##IDS_ERROR_88##</td></row>
		<row><td>1806</td><td>##IDS_ERROR_89##</td></row>
		<row><td>1807</td><td>##IDS_ERROR_90##</td></row>
		<row><td>19</td><td>##IDS_ERROR_17##</td></row>
		<row><td>1901</td><td>##IDS_ERROR_91##</td></row>
		<row><td>1902</td><td>##IDS_ERROR_92##</td></row>
		<row><td>1903</td><td>##IDS_ERROR_93##</td></row>
		<row><td>1904</td><td>##IDS_ERROR_94##</td></row>
		<row><td>1905</td><td>##IDS_ERROR_95##</td></row>
		<row><td>1906</td><td>##IDS_ERROR_96##</td></row>
		<row><td>1907</td><td>##IDS_ERROR_97##</td></row>
		<row><td>1908</td><td>##IDS_ERROR_98##</td></row>
		<row><td>1909</td><td>##IDS_ERROR_99##</td></row>
		<row><td>1910</td><td>##IDS_ERROR_100##</td></row>
		<row><td>1911</td><td>##IDS_ERROR_101##</td></row>
		<row><td>1912</td><td>##IDS_ERROR_102##</td></row>
		<row><td>1913</td><td>##IDS_ERROR_103##</td></row>
		<row><td>1914</td><td>##IDS_ERROR_104##</td></row>
		<row><td>1915</td><td>##IDS_ERROR_105##</td></row>
		<row><td>1916</td><td>##IDS_ERROR_106##</td></row>
		<row><td>1917</td><td>##IDS_ERROR_107##</td></row>
		<row><td>1918</td><td>##IDS_ERROR_108##</td></row>
		<row><td>1919</td><td>##IDS_ERROR_109##</td></row>
		<row><td>1920</td><td>##IDS_ERROR_110##</td></row>
		<row><td>1921</td><td>##IDS_ERROR_111##</td></row>
		<row><td>1922</td><td>##IDS_ERROR_112##</td></row>
		<row><td>1923</td><td>##IDS_ERROR_113##</td></row>
		<row><td>1924</td><td>##IDS_ERROR_114##</td></row>
		<row><td>1925</td><td>##IDS_ERROR_115##</td></row>
		<row><td>1926</td><td>##IDS_ERROR_116##</td></row>
		<row><td>1927</td><td>##IDS_ERROR_117##</td></row>
		<row><td>1928</td><td>##IDS_ERROR_118##</td></row>
		<row><td>1929</td><td>##IDS_ERROR_119##</td></row>
		<row><td>1930</td><td>##IDS_ERROR_125##</td></row>
		<row><td>1931</td><td>##IDS_ERROR_126##</td></row>
		<row><td>1932</td><td>##IDS_ERROR_127##</td></row>
		<row><td>1933</td><td>##IDS_ERROR_128##</td></row>
		<row><td>1934</td><td>##IDS_ERROR_129##</td></row>
		<row><td>1935</td><td>##IDS_ERROR_1935##</td></row>
		<row><td>1936</td><td>##IDS_ERROR_1936##</td></row>
		<row><td>1937</td><td>##IDS_ERROR_1937##</td></row>
		<row><td>1938</td><td>##IDS_ERROR_1938##</td></row>
		<row><td>2</td><td>##IDS_ERROR_2##</td></row>
		<row><td>20</td><td>##IDS_ERROR_18##</td></row>
		<row><td>21</td><td>##IDS_ERROR_19##</td></row>
		<row><td>2101</td><td>##IDS_ERROR_2101##</td></row>
		<row><td>2102</td><td>##IDS_ERROR_2102##</td></row>
		<row><td>2103</td><td>##IDS_ERROR_2103##</td></row>
		<row><td>2104</td><td>##IDS_ERROR_2104##</td></row>
		<row><td>2105</td><td>##IDS_ERROR_2105##</td></row>
		<row><td>2106</td><td>##IDS_ERROR_2106##</td></row>
		<row><td>2107</td><td>##IDS_ERROR_2107##</td></row>
		<row><td>2108</td><td>##IDS_ERROR_2108##</td></row>
		<row><td>2109</td><td>##IDS_ERROR_2109##</td></row>
		<row><td>2110</td><td>##IDS_ERROR_2110##</td></row>
		<row><td>2111</td><td>##IDS_ERROR_2111##</td></row>
		<row><td>2112</td><td>##IDS_ERROR_2112##</td></row>
		<row><td>2113</td><td>##IDS_ERROR_2113##</td></row>
		<row><td>22</td><td>##IDS_ERROR_120##</td></row>
		<row><td>2200</td><td>##IDS_ERROR_2200##</td></row>
		<row><td>2201</td><td>##IDS_ERROR_2201##</td></row>
		<row><td>2202</td><td>##IDS_ERROR_2202##</td></row>
		<row><td>2203</td><td>##IDS_ERROR_2203##</td></row>
		<row><td>2204</td><td>##IDS_ERROR_2204##</td></row>
		<row><td>2205</td><td>##IDS_ERROR_2205##</td></row>
		<row><td>2206</td><td>##IDS_ERROR_2206##</td></row>
		<row><td>2207</td><td>##IDS_ERROR_2207##</td></row>
		<row><td>2208</td><td>##IDS_ERROR_2208##</td></row>
		<row><td>2209</td><td>##IDS_ERROR_2209##</td></row>
		<row><td>2210</td><td>##IDS_ERROR_2210##</td></row>
		<row><td>2211</td><td>##IDS_ERROR_2211##</td></row>
		<row><td>2212</td><td>##IDS_ERROR_2212##</td></row>
		<row><td>2213</td><td>##IDS_ERROR_2213##</td></row>
		<row><td>2214</td><td>##IDS_ERROR_2214##</td></row>
		<row><td>2215</td><td>##IDS_ERROR_2215##</td></row>
		<row><td>2216</td><td>##IDS_ERROR_2216##</td></row>
		<row><td>2217</td><td>##IDS_ERROR_2217##</td></row>
		<row><td>2218</td><td>##IDS_ERROR_2218##</td></row>
		<row><td>2219</td><td>##IDS_ERROR_2219##</td></row>
		<row><td>2220</td><td>##IDS_ERROR_2220##</td></row>
		<row><td>2221</td><td>##IDS_ERROR_2221##</td></row>
		<row><td>2222</td><td>##IDS_ERROR_2222##</td></row>
		<row><td>2223</td><td>##IDS_ERROR_2223##</td></row>
		<row><td>2224</td><td>##IDS_ERROR_2224##</td></row>
		<row><td>2225</td><td>##IDS_ERROR_2225##</td></row>
		<row><td>2226</td><td>##IDS_ERROR_2226##</td></row>
		<row><td>2227</td><td>##IDS_ERROR_2227##</td></row>
		<row><td>2228</td><td>##IDS_ERROR_2228##</td></row>
		<row><td>2229</td><td>##IDS_ERROR_2229##</td></row>
		<row><td>2230</td><td>##IDS_ERROR_2230##</td></row>
		<row><td>2231</td><td>##IDS_ERROR_2231##</td></row>
		<row><td>2232</td><td>##IDS_ERROR_2232##</td></row>
		<row><td>2233</td><td>##IDS_ERROR_2233##</td></row>
		<row><td>2234</td><td>##IDS_ERROR_2234##</td></row>
		<row><td>2235</td><td>##IDS_ERROR_2235##</td></row>
		<row><td>2236</td><td>##IDS_ERROR_2236##</td></row>
		<row><td>2237</td><td>##IDS_ERROR_2237##</td></row>
		<row><td>2238</td><td>##IDS_ERROR_2238##</td></row>
		<row><td>2239</td><td>##IDS_ERROR_2239##</td></row>
		<row><td>2240</td><td>##IDS_ERROR_2240##</td></row>
		<row><td>2241</td><td>##IDS_ERROR_2241##</td></row>
		<row><td>2242</td><td>##IDS_ERROR_2242##</td></row>
		<row><td>2243</td><td>##IDS_ERROR_2243##</td></row>
		<row><td>2244</td><td>##IDS_ERROR_2244##</td></row>
		<row><td>2245</td><td>##IDS_ERROR_2245##</td></row>
		<row><td>2246</td><td>##IDS_ERROR_2246##</td></row>
		<row><td>2247</td><td>##IDS_ERROR_2247##</td></row>
		<row><td>2248</td><td>##IDS_ERROR_2248##</td></row>
		<row><td>2249</td><td>##IDS_ERROR_2249##</td></row>
		<row><td>2250</td><td>##IDS_ERROR_2250##</td></row>
		<row><td>2251</td><td>##IDS_ERROR_2251##</td></row>
		<row><td>2252</td><td>##IDS_ERROR_2252##</td></row>
		<row><td>2253</td><td>##IDS_ERROR_2253##</td></row>
		<row><td>2254</td><td>##IDS_ERROR_2254##</td></row>
		<row><td>2255</td><td>##IDS_ERROR_2255##</td></row>
		<row><td>2256</td><td>##IDS_ERROR_2256##</td></row>
		<row><td>2257</td><td>##IDS_ERROR_2257##</td></row>
		<row><td>2258</td><td>##IDS_ERROR_2258##</td></row>
		<row><td>2259</td><td>##IDS_ERROR_2259##</td></row>
		<row><td>2260</td><td>##IDS_ERROR_2260##</td></row>
		<row><td>2261</td><td>##IDS_ERROR_2261##</td></row>
		<row><td>2262</td><td>##IDS_ERROR_2262##</td></row>
		<row><td>2263</td><td>##IDS_ERROR_2263##</td></row>
		<row><td>2264</td><td>##IDS_ERROR_2264##</td></row>
		<row><td>2265</td><td>##IDS_ERROR_2265##</td></row>
		<row><td>2266</td><td>##IDS_ERROR_2266##</td></row>
		<row><td>2267</td><td>##IDS_ERROR_2267##</td></row>
		<row><td>2268</td><td>##IDS_ERROR_2268##</td></row>
		<row><td>2269</td><td>##IDS_ERROR_2269##</td></row>
		<row><td>2270</td><td>##IDS_ERROR_2270##</td></row>
		<row><td>2271</td><td>##IDS_ERROR_2271##</td></row>
		<row><td>2272</td><td>##IDS_ERROR_2272##</td></row>
		<row><td>2273</td><td>##IDS_ERROR_2273##</td></row>
		<row><td>2274</td><td>##IDS_ERROR_2274##</td></row>
		<row><td>2275</td><td>##IDS_ERROR_2275##</td></row>
		<row><td>2276</td><td>##IDS_ERROR_2276##</td></row>
		<row><td>2277</td><td>##IDS_ERROR_2277##</td></row>
		<row><td>2278</td><td>##IDS_ERROR_2278##</td></row>
		<row><td>2279</td><td>##IDS_ERROR_2279##</td></row>
		<row><td>2280</td><td>##IDS_ERROR_2280##</td></row>
		<row><td>2281</td><td>##IDS_ERROR_2281##</td></row>
		<row><td>2282</td><td>##IDS_ERROR_2282##</td></row>
		<row><td>23</td><td>##IDS_ERROR_121##</td></row>
		<row><td>2302</td><td>##IDS_ERROR_2302##</td></row>
		<row><td>2303</td><td>##IDS_ERROR_2303##</td></row>
		<row><td>2304</td><td>##IDS_ERROR_2304##</td></row>
		<row><td>2305</td><td>##IDS_ERROR_2305##</td></row>
		<row><td>2306</td><td>##IDS_ERROR_2306##</td></row>
		<row><td>2307</td><td>##IDS_ERROR_2307##</td></row>
		<row><td>2308</td><td>##IDS_ERROR_2308##</td></row>
		<row><td>2309</td><td>##IDS_ERROR_2309##</td></row>
		<row><td>2310</td><td>##IDS_ERROR_2310##</td></row>
		<row><td>2315</td><td>##IDS_ERROR_2315##</td></row>
		<row><td>2318</td><td>##IDS_ERROR_2318##</td></row>
		<row><td>2319</td><td>##IDS_ERROR_2319##</td></row>
		<row><td>2320</td><td>##IDS_ERROR_2320##</td></row>
		<row><td>2321</td><td>##IDS_ERROR_2321##</td></row>
		<row><td>2322</td><td>##IDS_ERROR_2322##</td></row>
		<row><td>2323</td><td>##IDS_ERROR_2323##</td></row>
		<row><td>2324</td><td>##IDS_ERROR_2324##</td></row>
		<row><td>2325</td><td>##IDS_ERROR_2325##</td></row>
		<row><td>2326</td><td>##IDS_ERROR_2326##</td></row>
		<row><td>2327</td><td>##IDS_ERROR_2327##</td></row>
		<row><td>2328</td><td>##IDS_ERROR_2328##</td></row>
		<row><td>2329</td><td>##IDS_ERROR_2329##</td></row>
		<row><td>2330</td><td>##IDS_ERROR_2330##</td></row>
		<row><td>2331</td><td>##IDS_ERROR_2331##</td></row>
		<row><td>2332</td><td>##IDS_ERROR_2332##</td></row>
		<row><td>2333</td><td>##IDS_ERROR_2333##</td></row>
		<row><td>2334</td><td>##IDS_ERROR_2334##</td></row>
		<row><td>2335</td><td>##IDS_ERROR_2335##</td></row>
		<row><td>2336</td><td>##IDS_ERROR_2336##</td></row>
		<row><td>2337</td><td>##IDS_ERROR_2337##</td></row>
		<row><td>2338</td><td>##IDS_ERROR_2338##</td></row>
		<row><td>2339</td><td>##IDS_ERROR_2339##</td></row>
		<row><td>2340</td><td>##IDS_ERROR_2340##</td></row>
		<row><td>2341</td><td>##IDS_ERROR_2341##</td></row>
		<row><td>2342</td><td>##IDS_ERROR_2342##</td></row>
		<row><td>2343</td><td>##IDS_ERROR_2343##</td></row>
		<row><td>2344</td><td>##IDS_ERROR_2344##</td></row>
		<row><td>2345</td><td>##IDS_ERROR_2345##</td></row>
		<row><td>2347</td><td>##IDS_ERROR_2347##</td></row>
		<row><td>2348</td><td>##IDS_ERROR_2348##</td></row>
		<row><td>2349</td><td>##IDS_ERROR_2349##</td></row>
		<row><td>2350</td><td>##IDS_ERROR_2350##</td></row>
		<row><td>2351</td><td>##IDS_ERROR_2351##</td></row>
		<row><td>2352</td><td>##IDS_ERROR_2352##</td></row>
		<row><td>2353</td><td>##IDS_ERROR_2353##</td></row>
		<row><td>2354</td><td>##IDS_ERROR_2354##</td></row>
		<row><td>2355</td><td>##IDS_ERROR_2355##</td></row>
		<row><td>2356</td><td>##IDS_ERROR_2356##</td></row>
		<row><td>2357</td><td>##IDS_ERROR_2357##</td></row>
		<row><td>2358</td><td>##IDS_ERROR_2358##</td></row>
		<row><td>2359</td><td>##IDS_ERROR_2359##</td></row>
		<row><td>2360</td><td>##IDS_ERROR_2360##</td></row>
		<row><td>2361</td><td>##IDS_ERROR_2361##</td></row>
		<row><td>2362</td><td>##IDS_ERROR_2362##</td></row>
		<row><td>2363</td><td>##IDS_ERROR_2363##</td></row>
		<row><td>2364</td><td>##IDS_ERROR_2364##</td></row>
		<row><td>2365</td><td>##IDS_ERROR_2365##</td></row>
		<row><td>2366</td><td>##IDS_ERROR_2366##</td></row>
		<row><td>2367</td><td>##IDS_ERROR_2367##</td></row>
		<row><td>2368</td><td>##IDS_ERROR_2368##</td></row>
		<row><td>2370</td><td>##IDS_ERROR_2370##</td></row>
		<row><td>2371</td><td>##IDS_ERROR_2371##</td></row>
		<row><td>2372</td><td>##IDS_ERROR_2372##</td></row>
		<row><td>2373</td><td>##IDS_ERROR_2373##</td></row>
		<row><td>2374</td><td>##IDS_ERROR_2374##</td></row>
		<row><td>2375</td><td>##IDS_ERROR_2375##</td></row>
		<row><td>2376</td><td>##IDS_ERROR_2376##</td></row>
		<row><td>2379</td><td>##IDS_ERROR_2379##</td></row>
		<row><td>2380</td><td>##IDS_ERROR_2380##</td></row>
		<row><td>2381</td><td>##IDS_ERROR_2381##</td></row>
		<row><td>2382</td><td>##IDS_ERROR_2382##</td></row>
		<row><td>2401</td><td>##IDS_ERROR_2401##</td></row>
		<row><td>2402</td><td>##IDS_ERROR_2402##</td></row>
		<row><td>2501</td><td>##IDS_ERROR_2501##</td></row>
		<row><td>2502</td><td>##IDS_ERROR_2502##</td></row>
		<row><td>2503</td><td>##IDS_ERROR_2503##</td></row>
		<row><td>2601</td><td>##IDS_ERROR_2601##</td></row>
		<row><td>2602</td><td>##IDS_ERROR_2602##</td></row>
		<row><td>2603</td><td>##IDS_ERROR_2603##</td></row>
		<row><td>2604</td><td>##IDS_ERROR_2604##</td></row>
		<row><td>2605</td><td>##IDS_ERROR_2605##</td></row>
		<row><td>2606</td><td>##IDS_ERROR_2606##</td></row>
		<row><td>2607</td><td>##IDS_ERROR_2607##</td></row>
		<row><td>2608</td><td>##IDS_ERROR_2608##</td></row>
		<row><td>2609</td><td>##IDS_ERROR_2609##</td></row>
		<row><td>2611</td><td>##IDS_ERROR_2611##</td></row>
		<row><td>2612</td><td>##IDS_ERROR_2612##</td></row>
		<row><td>2613</td><td>##IDS_ERROR_2613##</td></row>
		<row><td>2614</td><td>##IDS_ERROR_2614##</td></row>
		<row><td>2615</td><td>##IDS_ERROR_2615##</td></row>
		<row><td>2616</td><td>##IDS_ERROR_2616##</td></row>
		<row><td>2617</td><td>##IDS_ERROR_2617##</td></row>
		<row><td>2618</td><td>##IDS_ERROR_2618##</td></row>
		<row><td>2619</td><td>##IDS_ERROR_2619##</td></row>
		<row><td>2620</td><td>##IDS_ERROR_2620##</td></row>
		<row><td>2621</td><td>##IDS_ERROR_2621##</td></row>
		<row><td>2701</td><td>##IDS_ERROR_2701##</td></row>
		<row><td>2702</td><td>##IDS_ERROR_2702##</td></row>
		<row><td>2703</td><td>##IDS_ERROR_2703##</td></row>
		<row><td>2704</td><td>##IDS_ERROR_2704##</td></row>
		<row><td>2705</td><td>##IDS_ERROR_2705##</td></row>
		<row><td>2706</td><td>##IDS_ERROR_2706##</td></row>
		<row><td>2707</td><td>##IDS_ERROR_2707##</td></row>
		<row><td>2708</td><td>##IDS_ERROR_2708##</td></row>
		<row><td>2709</td><td>##IDS_ERROR_2709##</td></row>
		<row><td>2710</td><td>##IDS_ERROR_2710##</td></row>
		<row><td>2711</td><td>##IDS_ERROR_2711##</td></row>
		<row><td>2712</td><td>##IDS_ERROR_2712##</td></row>
		<row><td>2713</td><td>##IDS_ERROR_2713##</td></row>
		<row><td>2714</td><td>##IDS_ERROR_2714##</td></row>
		<row><td>2715</td><td>##IDS_ERROR_2715##</td></row>
		<row><td>2716</td><td>##IDS_ERROR_2716##</td></row>
		<row><td>2717</td><td>##IDS_ERROR_2717##</td></row>
		<row><td>2718</td><td>##IDS_ERROR_2718##</td></row>
		<row><td>2719</td><td>##IDS_ERROR_2719##</td></row>
		<row><td>2720</td><td>##IDS_ERROR_2720##</td></row>
		<row><td>2721</td><td>##IDS_ERROR_2721##</td></row>
		<row><td>2722</td><td>##IDS_ERROR_2722##</td></row>
		<row><td>2723</td><td>##IDS_ERROR_2723##</td></row>
		<row><td>2724</td><td>##IDS_ERROR_2724##</td></row>
		<row><td>2725</td><td>##IDS_ERROR_2725##</td></row>
		<row><td>2726</td><td>##IDS_ERROR_2726##</td></row>
		<row><td>2727</td><td>##IDS_ERROR_2727##</td></row>
		<row><td>2728</td><td>##IDS_ERROR_2728##</td></row>
		<row><td>2729</td><td>##IDS_ERROR_2729##</td></row>
		<row><td>2730</td><td>##IDS_ERROR_2730##</td></row>
		<row><td>2731</td><td>##IDS_ERROR_2731##</td></row>
		<row><td>2732</td><td>##IDS_ERROR_2732##</td></row>
		<row><td>2733</td><td>##IDS_ERROR_2733##</td></row>
		<row><td>2734</td><td>##IDS_ERROR_2734##</td></row>
		<row><td>2735</td><td>##IDS_ERROR_2735##</td></row>
		<row><td>2736</td><td>##IDS_ERROR_2736##</td></row>
		<row><td>2737</td><td>##IDS_ERROR_2737##</td></row>
		<row><td>2738</td><td>##IDS_ERROR_2738##</td></row>
		<row><td>2739</td><td>##IDS_ERROR_2739##</td></row>
		<row><td>2740</td><td>##IDS_ERROR_2740##</td></row>
		<row><td>2741</td><td>##IDS_ERROR_2741##</td></row>
		<row><td>2742</td><td>##IDS_ERROR_2742##</td></row>
		<row><td>2743</td><td>##IDS_ERROR_2743##</td></row>
		<row><td>2744</td><td>##IDS_ERROR_2744##</td></row>
		<row><td>2745</td><td>##IDS_ERROR_2745##</td></row>
		<row><td>2746</td><td>##IDS_ERROR_2746##</td></row>
		<row><td>2747</td><td>##IDS_ERROR_2747##</td></row>
		<row><td>2748</td><td>##IDS_ERROR_2748##</td></row>
		<row><td>2749</td><td>##IDS_ERROR_2749##</td></row>
		<row><td>2750</td><td>##IDS_ERROR_2750##</td></row>
		<row><td>27500</td><td>##IDS_ERROR_130##</td></row>
		<row><td>27501</td><td>##IDS_ERROR_131##</td></row>
		<row><td>27502</td><td>##IDS_ERROR_27502##</td></row>
		<row><td>27503</td><td>##IDS_ERROR_27503##</td></row>
		<row><td>27504</td><td>##IDS_ERROR_27504##</td></row>
		<row><td>27505</td><td>##IDS_ERROR_27505##</td></row>
		<row><td>27506</td><td>##IDS_ERROR_27506##</td></row>
		<row><td>27507</td><td>##IDS_ERROR_27507##</td></row>
		<row><td>27508</td><td>##IDS_ERROR_27508##</td></row>
		<row><td>27509</td><td>##IDS_ERROR_27509##</td></row>
		<row><td>2751</td><td>##IDS_ERROR_2751##</td></row>
		<row><td>27510</td><td>##IDS_ERROR_27510##</td></row>
		<row><td>27511</td><td>##IDS_ERROR_27511##</td></row>
		<row><td>27512</td><td>##IDS_ERROR_27512##</td></row>
		<row><td>27513</td><td>##IDS_ERROR_27513##</td></row>
		<row><td>27514</td><td>##IDS_ERROR_27514##</td></row>
		<row><td>27515</td><td>##IDS_ERROR_27515##</td></row>
		<row><td>27516</td><td>##IDS_ERROR_27516##</td></row>
		<row><td>27517</td><td>##IDS_ERROR_27517##</td></row>
		<row><td>27518</td><td>##IDS_ERROR_27518##</td></row>
		<row><td>27519</td><td>##IDS_ERROR_27519##</td></row>
		<row><td>2752</td><td>##IDS_ERROR_2752##</td></row>
		<row><td>27520</td><td>##IDS_ERROR_27520##</td></row>
		<row><td>27521</td><td>##IDS_ERROR_27521##</td></row>
		<row><td>27522</td><td>##IDS_ERROR_27522##</td></row>
		<row><td>27523</td><td>##IDS_ERROR_27523##</td></row>
		<row><td>27524</td><td>##IDS_ERROR_27524##</td></row>
		<row><td>27525</td><td>##IDS_ERROR_27525##</td></row>
		<row><td>27526</td><td>##IDS_ERROR_27526##</td></row>
		<row><td>27527</td><td>##IDS_ERROR_27527##</td></row>
		<row><td>27528</td><td>##IDS_ERROR_27528##</td></row>
		<row><td>27529</td><td>##IDS_ERROR_27529##</td></row>
		<row><td>2753</td><td>##IDS_ERROR_2753##</td></row>
		<row><td>27530</td><td>##IDS_ERROR_27530##</td></row>
		<row><td>27531</td><td>##IDS_ERROR_27531##</td></row>
		<row><td>27532</td><td>##IDS_ERROR_27532##</td></row>
		<row><td>27533</td><td>##IDS_ERROR_27533##</td></row>
		<row><td>27534</td><td>##IDS_ERROR_27534##</td></row>
		<row><td>27535</td><td>##IDS_ERROR_27535##</td></row>
		<row><td>27536</td><td>##IDS_ERROR_27536##</td></row>
		<row><td>27537</td><td>##IDS_ERROR_27537##</td></row>
		<row><td>27538</td><td>##IDS_ERROR_27538##</td></row>
		<row><td>27539</td><td>##IDS_ERROR_27539##</td></row>
		<row><td>2754</td><td>##IDS_ERROR_2754##</td></row>
		<row><td>27540</td><td>##IDS_ERROR_27540##</td></row>
		<row><td>27541</td><td>##IDS_ERROR_27541##</td></row>
		<row><td>27542</td><td>##IDS_ERROR_27542##</td></row>
		<row><td>27543</td><td>##IDS_ERROR_27543##</td></row>
		<row><td>27544</td><td>##IDS_ERROR_27544##</td></row>
		<row><td>27545</td><td>##IDS_ERROR_27545##</td></row>
		<row><td>27546</td><td>##IDS_ERROR_27546##</td></row>
		<row><td>27547</td><td>##IDS_ERROR_27547##</td></row>
		<row><td>27548</td><td>##IDS_ERROR_27548##</td></row>
		<row><td>27549</td><td>##IDS_ERROR_27549##</td></row>
		<row><td>2755</td><td>##IDS_ERROR_2755##</td></row>
		<row><td>27550</td><td>##IDS_ERROR_27550##</td></row>
		<row><td>27551</td><td>##IDS_ERROR_27551##</td></row>
		<row><td>27552</td><td>##IDS_ERROR_27552##</td></row>
		<row><td>27553</td><td>##IDS_ERROR_27553##</td></row>
		<row><td>27554</td><td>##IDS_ERROR_27554##</td></row>
		<row><td>27555</td><td>##IDS_ERROR_27555##</td></row>
		<row><td>2756</td><td>##IDS_ERROR_2756##</td></row>
		<row><td>2757</td><td>##IDS_ERROR_2757##</td></row>
		<row><td>2758</td><td>##IDS_ERROR_2758##</td></row>
		<row><td>2759</td><td>##IDS_ERROR_2759##</td></row>
		<row><td>2760</td><td>##IDS_ERROR_2760##</td></row>
		<row><td>2761</td><td>##IDS_ERROR_2761##</td></row>
		<row><td>2762</td><td>##IDS_ERROR_2762##</td></row>
		<row><td>2763</td><td>##IDS_ERROR_2763##</td></row>
		<row><td>2765</td><td>##IDS_ERROR_2765##</td></row>
		<row><td>2766</td><td>##IDS_ERROR_2766##</td></row>
		<row><td>2767</td><td>##IDS_ERROR_2767##</td></row>
		<row><td>2768</td><td>##IDS_ERROR_2768##</td></row>
		<row><td>2769</td><td>##IDS_ERROR_2769##</td></row>
		<row><td>2770</td><td>##IDS_ERROR_2770##</td></row>
		<row><td>2771</td><td>##IDS_ERROR_2771##</td></row>
		<row><td>2772</td><td>##IDS_ERROR_2772##</td></row>
		<row><td>2801</td><td>##IDS_ERROR_2801##</td></row>
		<row><td>2802</td><td>##IDS_ERROR_2802##</td></row>
		<row><td>2803</td><td>##IDS_ERROR_2803##</td></row>
		<row><td>2804</td><td>##IDS_ERROR_2804##</td></row>
		<row><td>2806</td><td>##IDS_ERROR_2806##</td></row>
		<row><td>2807</td><td>##IDS_ERROR_2807##</td></row>
		<row><td>2808</td><td>##IDS_ERROR_2808##</td></row>
		<row><td>2809</td><td>##IDS_ERROR_2809##</td></row>
		<row><td>2810</td><td>##IDS_ERROR_2810##</td></row>
		<row><td>2811</td><td>##IDS_ERROR_2811##</td></row>
		<row><td>2812</td><td>##IDS_ERROR_2812##</td></row>
		<row><td>2813</td><td>##IDS_ERROR_2813##</td></row>
		<row><td>2814</td><td>##IDS_ERROR_2814##</td></row>
		<row><td>2815</td><td>##IDS_ERROR_2815##</td></row>
		<row><td>2816</td><td>##IDS_ERROR_2816##</td></row>
		<row><td>2817</td><td>##IDS_ERROR_2817##</td></row>
		<row><td>2818</td><td>##IDS_ERROR_2818##</td></row>
		<row><td>2819</td><td>##IDS_ERROR_2819##</td></row>
		<row><td>2820</td><td>##IDS_ERROR_2820##</td></row>
		<row><td>2821</td><td>##IDS_ERROR_2821##</td></row>
		<row><td>2822</td><td>##IDS_ERROR_2822##</td></row>
		<row><td>2823</td><td>##IDS_ERROR_2823##</td></row>
		<row><td>2824</td><td>##IDS_ERROR_2824##</td></row>
		<row><td>2825</td><td>##IDS_ERROR_2825##</td></row>
		<row><td>2826</td><td>##IDS_ERROR_2826##</td></row>
		<row><td>2827</td><td>##IDS_ERROR_2827##</td></row>
		<row><td>2828</td><td>##IDS_ERROR_2828##</td></row>
		<row><td>2829</td><td>##IDS_ERROR_2829##</td></row>
		<row><td>2830</td><td>##IDS_ERROR_2830##</td></row>
		<row><td>2831</td><td>##IDS_ERROR_2831##</td></row>
		<row><td>2832</td><td>##IDS_ERROR_2832##</td></row>
		<row><td>2833</td><td>##IDS_ERROR_2833##</td></row>
		<row><td>2834</td><td>##IDS_ERROR_2834##</td></row>
		<row><td>2835</td><td>##IDS_ERROR_2835##</td></row>
		<row><td>2836</td><td>##IDS_ERROR_2836##</td></row>
		<row><td>2837</td><td>##IDS_ERROR_2837##</td></row>
		<row><td>2838</td><td>##IDS_ERROR_2838##</td></row>
		<row><td>2839</td><td>##IDS_ERROR_2839##</td></row>
		<row><td>2840</td><td>##IDS_ERROR_2840##</td></row>
		<row><td>2841</td><td>##IDS_ERROR_2841##</td></row>
		<row><td>2842</td><td>##IDS_ERROR_2842##</td></row>
		<row><td>2843</td><td>##IDS_ERROR_2843##</td></row>
		<row><td>2844</td><td>##IDS_ERROR_2844##</td></row>
		<row><td>2845</td><td>##IDS_ERROR_2845##</td></row>
		<row><td>2846</td><td>##IDS_ERROR_2846##</td></row>
		<row><td>2847</td><td>##IDS_ERROR_2847##</td></row>
		<row><td>2848</td><td>##IDS_ERROR_2848##</td></row>
		<row><td>2849</td><td>##IDS_ERROR_2849##</td></row>
		<row><td>2850</td><td>##IDS_ERROR_2850##</td></row>
		<row><td>2851</td><td>##IDS_ERROR_2851##</td></row>
		<row><td>2852</td><td>##IDS_ERROR_2852##</td></row>
		<row><td>2853</td><td>##IDS_ERROR_2853##</td></row>
		<row><td>2854</td><td>##IDS_ERROR_2854##</td></row>
		<row><td>2855</td><td>##IDS_ERROR_2855##</td></row>
		<row><td>2856</td><td>##IDS_ERROR_2856##</td></row>
		<row><td>2857</td><td>##IDS_ERROR_2857##</td></row>
		<row><td>2858</td><td>##IDS_ERROR_2858##</td></row>
		<row><td>2859</td><td>##IDS_ERROR_2859##</td></row>
		<row><td>2860</td><td>##IDS_ERROR_2860##</td></row>
		<row><td>2861</td><td>##IDS_ERROR_2861##</td></row>
		<row><td>2862</td><td>##IDS_ERROR_2862##</td></row>
		<row><td>2863</td><td>##IDS_ERROR_2863##</td></row>
		<row><td>2864</td><td>##IDS_ERROR_2864##</td></row>
		<row><td>2865</td><td>##IDS_ERROR_2865##</td></row>
		<row><td>2866</td><td>##IDS_ERROR_2866##</td></row>
		<row><td>2867</td><td>##IDS_ERROR_2867##</td></row>
		<row><td>2868</td><td>##IDS_ERROR_2868##</td></row>
		<row><td>2869</td><td>##IDS_ERROR_2869##</td></row>
		<row><td>2870</td><td>##IDS_ERROR_2870##</td></row>
		<row><td>2871</td><td>##IDS_ERROR_2871##</td></row>
		<row><td>2872</td><td>##IDS_ERROR_2872##</td></row>
		<row><td>2873</td><td>##IDS_ERROR_2873##</td></row>
		<row><td>2874</td><td>##IDS_ERROR_2874##</td></row>
		<row><td>2875</td><td>##IDS_ERROR_2875##</td></row>
		<row><td>2876</td><td>##IDS_ERROR_2876##</td></row>
		<row><td>2877</td><td>##IDS_ERROR_2877##</td></row>
		<row><td>2878</td><td>##IDS_ERROR_2878##</td></row>
		<row><td>2879</td><td>##IDS_ERROR_2879##</td></row>
		<row><td>2880</td><td>##IDS_ERROR_2880##</td></row>
		<row><td>2881</td><td>##IDS_ERROR_2881##</td></row>
		<row><td>2882</td><td>##IDS_ERROR_2882##</td></row>
		<row><td>2883</td><td>##IDS_ERROR_2883##</td></row>
		<row><td>2884</td><td>##IDS_ERROR_2884##</td></row>
		<row><td>2885</td><td>##IDS_ERROR_2885##</td></row>
		<row><td>2886</td><td>##IDS_ERROR_2886##</td></row>
		<row><td>2887</td><td>##IDS_ERROR_2887##</td></row>
		<row><td>2888</td><td>##IDS_ERROR_2888##</td></row>
		<row><td>2889</td><td>##IDS_ERROR_2889##</td></row>
		<row><td>2890</td><td>##IDS_ERROR_2890##</td></row>
		<row><td>2891</td><td>##IDS_ERROR_2891##</td></row>
		<row><td>2892</td><td>##IDS_ERROR_2892##</td></row>
		<row><td>2893</td><td>##IDS_ERROR_2893##</td></row>
		<row><td>2894</td><td>##IDS_ERROR_2894##</td></row>
		<row><td>2895</td><td>##IDS_ERROR_2895##</td></row>
		<row><td>2896</td><td>##IDS_ERROR_2896##</td></row>
		<row><td>2897</td><td>##IDS_ERROR_2897##</td></row>
		<row><td>2898</td><td>##IDS_ERROR_2898##</td></row>
		<row><td>2899</td><td>##IDS_ERROR_2899##</td></row>
		<row><td>2901</td><td>##IDS_ERROR_2901##</td></row>
		<row><td>2902</td><td>##IDS_ERROR_2902##</td></row>
		<row><td>2903</td><td>##IDS_ERROR_2903##</td></row>
		<row><td>2904</td><td>##IDS_ERROR_2904##</td></row>
		<row><td>2905</td><td>##IDS_ERROR_2905##</td></row>
		<row><td>2906</td><td>##IDS_ERROR_2906##</td></row>
		<row><td>2907</td><td>##IDS_ERROR_2907##</td></row>
		<row><td>2908</td><td>##IDS_ERROR_2908##</td></row>
		<row><td>2909</td><td>##IDS_ERROR_2909##</td></row>
		<row><td>2910</td><td>##IDS_ERROR_2910##</td></row>
		<row><td>2911</td><td>##IDS_ERROR_2911##</td></row>
		<row><td>2912</td><td>##IDS_ERROR_2912##</td></row>
		<row><td>2919</td><td>##IDS_ERROR_2919##</td></row>
		<row><td>2920</td><td>##IDS_ERROR_2920##</td></row>
		<row><td>2924</td><td>##IDS_ERROR_2924##</td></row>
		<row><td>2927</td><td>##IDS_ERROR_2927##</td></row>
		<row><td>2928</td><td>##IDS_ERROR_2928##</td></row>
		<row><td>2929</td><td>##IDS_ERROR_2929##</td></row>
		<row><td>2932</td><td>##IDS_ERROR_2932##</td></row>
		<row><td>2933</td><td>##IDS_ERROR_2933##</td></row>
		<row><td>2934</td><td>##IDS_ERROR_2934##</td></row>
		<row><td>2935</td><td>##IDS_ERROR_2935##</td></row>
		<row><td>2936</td><td>##IDS_ERROR_2936##</td></row>
		<row><td>2937</td><td>##IDS_ERROR_2937##</td></row>
		<row><td>2938</td><td>##IDS_ERROR_2938##</td></row>
		<row><td>2939</td><td>##IDS_ERROR_2939##</td></row>
		<row><td>2940</td><td>##IDS_ERROR_2940##</td></row>
		<row><td>2941</td><td>##IDS_ERROR_2941##</td></row>
		<row><td>2942</td><td>##IDS_ERROR_2942##</td></row>
		<row><td>2943</td><td>##IDS_ERROR_2943##</td></row>
		<row><td>2944</td><td>##IDS_ERROR_2944##</td></row>
		<row><td>2945</td><td>##IDS_ERROR_2945##</td></row>
		<row><td>3001</td><td>##IDS_ERROR_3001##</td></row>
		<row><td>3002</td><td>##IDS_ERROR_3002##</td></row>
		<row><td>32</td><td>##IDS_ERROR_20##</td></row>
		<row><td>33</td><td>##IDS_ERROR_21##</td></row>
		<row><td>4</td><td>##IDS_ERROR_3##</td></row>
		<row><td>5</td><td>##IDS_ERROR_4##</td></row>
		<row><td>7</td><td>##IDS_ERROR_5##</td></row>
		<row><td>8</td><td>##IDS_ERROR_6##</td></row>
		<row><td>9</td><td>##IDS_ERROR_7##</td></row>
	</table>

	<table name="EventMapping">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col def="s50">Attribute</col>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>SelectionDescription</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Location</td><td>SelectionPath</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Size</td><td>SelectionSize</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>ActionData</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>AdminInstallFinalize</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>InstallFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>MoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>SetProgress</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>UnmoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteIniValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
	</table>

	<table name="Extension">
		<col key="yes" def="s255">Extension</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_</col>
		<col def="S64">MIME_</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="Feature">
		<col key="yes" def="s38">Feature</col>
		<col def="S38">Feature_Parent</col>
		<col def="L64">Title</col>
		<col def="L255">Description</col>
		<col def="I2">Display</col>
		<col def="i2">Level</col>
		<col def="S72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISFeatureCabName</col>
		<col def="S255">ISProFeatureName</col>
		<row><td>AlwaysInstall</td><td/><td>##DN_AlwaysInstall##</td><td>Enter the description for this feature here.</td><td>0</td><td>1</td><td>INSTALLDIR</td><td>16</td><td/><td>Enter comments regarding this feature here.</td><td/><td/></row>
	</table>

	<table name="FeatureComponents">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>AlwaysInstall</td><td>AdbWinApi.dll</td></row>
		<row><td>AlwaysInstall</td><td>AdbWinUsbApi.dll</td></row>
		<row><td>AlwaysInstall</td><td>BouncyCastle.Crypto.dll2</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.PInvoke.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Parse.Android.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Parse.Local.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Parse.Signature.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Parse.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Previewers.Contracts.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFC.Singularity.Forensics.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFCControls.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFCCultures.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFCHexaEditor.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFCMessageBoxes.dll</td></row>
		<row><td>AlwaysInstall</td><td>CDFCUIContracts.dll1</td></row>
		<row><td>AlwaysInstall</td><td>CDFCUIContracts.dll3</td></row>
		<row><td>AlwaysInstall</td><td>Cflab.DataTransport.dll2</td></row>
		<row><td>AlwaysInstall</td><td>DirectOutIn.dll2</td></row>
		<row><td>AlwaysInstall</td><td>EventLogger.dll</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT100</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT101</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT102</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT103</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT104</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT105</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT106</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT107</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT108</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT109</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT110</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT111</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT112</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT113</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT114</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT115</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT116</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT117</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT118</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT119</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT12</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT120</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT121</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT122</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT123</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT124</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT125</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT126</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT127</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT128</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT129</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT130</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT131</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT132</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT133</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT134</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT135</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT136</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT137</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT138</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT139</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT140</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT141</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT142</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT143</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT144</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT145</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT146</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT147</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT148</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT149</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT150</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT155</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT156</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT157</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT158</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT159</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT16</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT163</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT169</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT17</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT170</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT171</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT172</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT173</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT183</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT184</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT185</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT186</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT187</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT188</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT189</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT19</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT190</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT191</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT192</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT193</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT194</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT195</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT196</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT197</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT198</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT199</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT200</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT201</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT202</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT203</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT204</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT205</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT206</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT207</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT21</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT22</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT23</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT24</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT25</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT26</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT27</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT28</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT29</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT30</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT33</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT34</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT35</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT36</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT37</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT38</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT39</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT4</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT40</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT41</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT42</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT43</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT44</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT45</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT46</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT47</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT48</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT49</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT50</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT51</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT52</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT53</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT54</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT55</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT56</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT57</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT58</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT59</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT60</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT61</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT62</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT63</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT64</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT65</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT66</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT67</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT68</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT69</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT70</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT71</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT72</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT73</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT74</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT75</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT76</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT77</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT78</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT79</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT80</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT81</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT82</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT83</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT84</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT85</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT86</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT87</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT88</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT89</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT90</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT91</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT92</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT93</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT94</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT95</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT96</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT97</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT98</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT99</td></row>
		<row><td>AlwaysInstall</td><td>IS_ININSTALL_SHORTCUT</td></row>
		<row><td>AlwaysInstall</td><td>JAWTAccessBridge_32.dll</td></row>
		<row><td>AlwaysInstall</td><td>JavaAccessBridge_32.dll</td></row>
		<row><td>AlwaysInstall</td><td>MahApps.Metro.dll1</td></row>
		<row><td>AlwaysInstall</td><td>MahApps.Metro.dll3</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Practices.ServiceLocation.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Newtonsoft.Json.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Ookii.Dialogs.Wpf.dll</td></row>
		<row><td>AlwaysInstall</td><td>Prism.Mef.Wpf.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Prism.Wpf.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Prism.dll2</td></row>
		<row><td>AlwaysInstall</td><td>SQLite.Interop.dll</td></row>
		<row><td>AlwaysInstall</td><td>SQLite.Interop.dll1</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.Fonts.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.Previewers.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.AdbViewer.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.Controls.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.Converters.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.Info.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.Infrastructure.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.MessageBoxes.dll</td></row>
		<row><td>AlwaysInstall</td><td>Singularity.UI.Themes.dll</td></row>
		<row><td>AlwaysInstall</td><td>SingularityForensic.dll</td></row>
		<row><td>AlwaysInstall</td><td>SingularityShell.exe2</td></row>
		<row><td>AlwaysInstall</td><td>SingularityShell.vshost.exe2</td></row>
		<row><td>AlwaysInstall</td><td>System.Data.Sqlite.dll2</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.dll2</td></row>
		<row><td>AlwaysInstall</td><td>UserMsgUs.dll</td></row>
		<row><td>AlwaysInstall</td><td>WindowsAccessBridge_32.dll</td></row>
		<row><td>AlwaysInstall</td><td>adb.exe</td></row>
		<row><td>AlwaysInstall</td><td>awt.dll</td></row>
		<row><td>AlwaysInstall</td><td>bci.dll</td></row>
		<row><td>AlwaysInstall</td><td>cdfcphoto.dll</td></row>
		<row><td>AlwaysInstall</td><td>cdfcqd.dll1</td></row>
		<row><td>AlwaysInstall</td><td>cdfcqd.dll3</td></row>
		<row><td>AlwaysInstall</td><td>dcpr.dll</td></row>
		<row><td>AlwaysInstall</td><td>debmp.dll</td></row>
		<row><td>AlwaysInstall</td><td>decora_sse.dll</td></row>
		<row><td>AlwaysInstall</td><td>dehex.dll</td></row>
		<row><td>AlwaysInstall</td><td>deploy.dll</td></row>
		<row><td>AlwaysInstall</td><td>deployJava1.dll</td></row>
		<row><td>AlwaysInstall</td><td>dess.dll</td></row>
		<row><td>AlwaysInstall</td><td>detree.dll</td></row>
		<row><td>AlwaysInstall</td><td>devect.dll</td></row>
		<row><td>AlwaysInstall</td><td>dewp.dll</td></row>
		<row><td>AlwaysInstall</td><td>dt_shmem.dll</td></row>
		<row><td>AlwaysInstall</td><td>dt_socket.dll</td></row>
		<row><td>AlwaysInstall</td><td>eula.dll</td></row>
		<row><td>AlwaysInstall</td><td>fontmanager.dll</td></row>
		<row><td>AlwaysInstall</td><td>fxplugins.dll</td></row>
		<row><td>AlwaysInstall</td><td>glass.dll</td></row>
		<row><td>AlwaysInstall</td><td>glib_lite.dll</td></row>
		<row><td>AlwaysInstall</td><td>gstreamer_lite.dll</td></row>
		<row><td>AlwaysInstall</td><td>hprof.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibfpx2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibgp42.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibjpg2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibpcd2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibpsd2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibxbm2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibxpm2.dll</td></row>
		<row><td>AlwaysInstall</td><td>ibxwd2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd32.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd42.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd52.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd62.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd72.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcd82.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcdr2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcm52.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcm72.dll</td></row>
		<row><td>AlwaysInstall</td><td>imcmx2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imdsf2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imfmv2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imgdf2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imgem2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imigs2.dll</td></row>
		<row><td>AlwaysInstall</td><td>immet2.dll</td></row>
		<row><td>AlwaysInstall</td><td>impif2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imps_2.dll</td></row>
		<row><td>AlwaysInstall</td><td>impsi2.dll</td></row>
		<row><td>AlwaysInstall</td><td>impsz2.dll</td></row>
		<row><td>AlwaysInstall</td><td>imrnd2.dll</td></row>
		<row><td>AlwaysInstall</td><td>instrument.dll</td></row>
		<row><td>AlwaysInstall</td><td>iphgw2.dll</td></row>
		<row><td>AlwaysInstall</td><td>isgdi32.dll</td></row>
		<row><td>AlwaysInstall</td><td>j2pcsc.dll</td></row>
		<row><td>AlwaysInstall</td><td>j2pkcs11.dll</td></row>
		<row><td>AlwaysInstall</td><td>jaas_nt.dll</td></row>
		<row><td>AlwaysInstall</td><td>jabswitch.exe</td></row>
		<row><td>AlwaysInstall</td><td>java.dll</td></row>
		<row><td>AlwaysInstall</td><td>java.exe</td></row>
		<row><td>AlwaysInstall</td><td>java_crw_demo.dll</td></row>
		<row><td>AlwaysInstall</td><td>java_rmi.exe</td></row>
		<row><td>AlwaysInstall</td><td>javacpl.exe</td></row>
		<row><td>AlwaysInstall</td><td>javafx_font.dll</td></row>
		<row><td>AlwaysInstall</td><td>javafx_font_t2k.dll</td></row>
		<row><td>AlwaysInstall</td><td>javafx_iio.dll</td></row>
		<row><td>AlwaysInstall</td><td>javaw.exe</td></row>
		<row><td>AlwaysInstall</td><td>javaws.exe</td></row>
		<row><td>AlwaysInstall</td><td>jawt.dll</td></row>
		<row><td>AlwaysInstall</td><td>jdwp.dll</td></row>
		<row><td>AlwaysInstall</td><td>jfr.dll</td></row>
		<row><td>AlwaysInstall</td><td>jfxmedia.dll</td></row>
		<row><td>AlwaysInstall</td><td>jfxwebkit.dll</td></row>
		<row><td>AlwaysInstall</td><td>jjs.exe</td></row>
		<row><td>AlwaysInstall</td><td>jli.dll</td></row>
		<row><td>AlwaysInstall</td><td>jp2iexp.dll</td></row>
		<row><td>AlwaysInstall</td><td>jp2launcher.exe</td></row>
		<row><td>AlwaysInstall</td><td>jp2native.dll</td></row>
		<row><td>AlwaysInstall</td><td>jp2ssv.dll</td></row>
		<row><td>AlwaysInstall</td><td>jpeg.dll</td></row>
		<row><td>AlwaysInstall</td><td>jsdt.dll</td></row>
		<row><td>AlwaysInstall</td><td>jsound.dll</td></row>
		<row><td>AlwaysInstall</td><td>jsoundds.dll</td></row>
		<row><td>AlwaysInstall</td><td>jvm.dll</td></row>
		<row><td>AlwaysInstall</td><td>kcms.dll</td></row>
		<row><td>AlwaysInstall</td><td>keytool.exe</td></row>
		<row><td>AlwaysInstall</td><td>kinit.exe</td></row>
		<row><td>AlwaysInstall</td><td>klist.exe</td></row>
		<row><td>AlwaysInstall</td><td>ktab.exe</td></row>
		<row><td>AlwaysInstall</td><td>lcms.dll</td></row>
		<row><td>AlwaysInstall</td><td>management.dll</td></row>
		<row><td>AlwaysInstall</td><td>mlib_image.dll</td></row>
		<row><td>AlwaysInstall</td><td>msvcp120.dll</td></row>
		<row><td>AlwaysInstall</td><td>msvcr100.dll</td></row>
		<row><td>AlwaysInstall</td><td>msvcr100.dll1</td></row>
		<row><td>AlwaysInstall</td><td>msvcr120.dll</td></row>
		<row><td>AlwaysInstall</td><td>net.dll</td></row>
		<row><td>AlwaysInstall</td><td>nio.dll</td></row>
		<row><td>AlwaysInstall</td><td>npdeployJava1.dll</td></row>
		<row><td>AlwaysInstall</td><td>npjp2.dll</td></row>
		<row><td>AlwaysInstall</td><td>npt.dll</td></row>
		<row><td>AlwaysInstall</td><td>ocemul.dll</td></row>
		<row><td>AlwaysInstall</td><td>orbd.exe</td></row>
		<row><td>AlwaysInstall</td><td>oswin32.dll</td></row>
		<row><td>AlwaysInstall</td><td>pack200.exe</td></row>
		<row><td>AlwaysInstall</td><td>policytool.exe</td></row>
		<row><td>AlwaysInstall</td><td>prism_common.dll</td></row>
		<row><td>AlwaysInstall</td><td>prism_d3d.dll</td></row>
		<row><td>AlwaysInstall</td><td>prism_sw.dll</td></row>
		<row><td>AlwaysInstall</td><td>resource.dll</td></row>
		<row><td>AlwaysInstall</td><td>rmid.exe</td></row>
		<row><td>AlwaysInstall</td><td>rmiregistry.exe</td></row>
		<row><td>AlwaysInstall</td><td>sccanno.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccca.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccch.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccda.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccdu.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccfa.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccfi.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccfmt.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccfnt.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccfut.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccind.dll</td></row>
		<row><td>AlwaysInstall</td><td>scclo.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccole.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccra.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccsd.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccta.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccut.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccvw.dll</td></row>
		<row><td>AlwaysInstall</td><td>sccxt.dll</td></row>
		<row><td>AlwaysInstall</td><td>servertool.exe</td></row>
		<row><td>AlwaysInstall</td><td>splashscreen.dll</td></row>
		<row><td>AlwaysInstall</td><td>ssv.dll</td></row>
		<row><td>AlwaysInstall</td><td>ssvagent.exe</td></row>
		<row><td>AlwaysInstall</td><td>sunec.dll</td></row>
		<row><td>AlwaysInstall</td><td>sunmscapi.dll</td></row>
		<row><td>AlwaysInstall</td><td>t2k.dll</td></row>
		<row><td>AlwaysInstall</td><td>tnameserv.exe</td></row>
		<row><td>AlwaysInstall</td><td>unpack.dll</td></row>
		<row><td>AlwaysInstall</td><td>unpack200.exe</td></row>
		<row><td>AlwaysInstall</td><td>verify.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsacad.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsacs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsami.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsarc.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsasf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsbdr.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsbmp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vscdrx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vscgm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdbs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdez.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdif.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdrw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdxla.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsdxlm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsemf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsen4.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsens.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsenw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vseshr.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsexe2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsfax.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsfcd.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsfcs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsfft.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsflw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsfwk.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsgdsf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsgif.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsgzip.dll</td></row>
		<row><td>AlwaysInstall</td><td>vshgs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vshtml.dll</td></row>
		<row><td>AlwaysInstall</td><td>vshwp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vshwp2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsich.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsich6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsid3.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsimg.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsiwok.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsiwok13.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsiwon.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsiwop.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsiwp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsjbg2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsjp2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsjw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsleg.dll</td></row>
		<row><td>AlwaysInstall</td><td>vslwp7.dll</td></row>
		<row><td>AlwaysInstall</td><td>vslzh.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsm11.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmanu.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmbox.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmcw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmdb.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmif.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmime.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmm4.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmmfn.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmpp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmsg.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmsw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmwkd.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmwks.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmwp2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmwpf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsmwrk.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsnsf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsolm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsone.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsow.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspbm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspcl.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspcx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspdf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspdfi.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspdx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspfs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspgl.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspic.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspict.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspng.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspntg.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspp12.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspp2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspp7.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspp97.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsppl.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspsp6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspst.dll</td></row>
		<row><td>AlwaysInstall</td><td>vspstf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsqa.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsqad.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsqp6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsqp9.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsqt.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsrar.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsras.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsrbs.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsrft.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsrfx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsriff.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsrtf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssam.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssc5.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssdw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsshw3.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssmd.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssms.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssmt.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssnap.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsso6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssoc.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssoc6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssoi.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssoi6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vssow.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsspt.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsssml.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsswf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstaz.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstext.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstga.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstif6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vstxt.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsvcrd.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsviso.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsvsdx.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsvw3.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsw12.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsw6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsw97.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswbmp.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswg2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswk4.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswk6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswks.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswm.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswmf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswml.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsword.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswork.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswp5.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswp6.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpf.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpg.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpg2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpl.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpml.dll</td></row>
		<row><td>AlwaysInstall</td><td>vswpw.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsws.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsws2.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxl12.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxl5.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxlsb.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxml.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxps.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsxy.dll</td></row>
		<row><td>AlwaysInstall</td><td>vsyim.dll</td></row>
		<row><td>AlwaysInstall</td><td>vszip.dll</td></row>
		<row><td>AlwaysInstall</td><td>w2k_lsa_auth.dll</td></row>
		<row><td>AlwaysInstall</td><td>wsdetect.dll</td></row>
		<row><td>AlwaysInstall</td><td>wvcore.dll</td></row>
		<row><td>AlwaysInstall</td><td>zip.dll</td></row>
	</table>

	<table name="File">
		<col key="yes" def="s72">File</col>
		<col def="s72">Component_</col>
		<col def="s255">FileName</col>
		<col def="i4">FileSize</col>
		<col def="S72">Version</col>
		<col def="S20">Language</col>
		<col def="I2">Attributes</col>
		<col def="i2">Sequence</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I4">ISAttributes</col>
		<col def="S72">ISComponentSubFolder_</col>
		<row><td>access_bridge_32.jar</td><td>ISX_DEFAULTCOMPONENT66</td><td>ACCESS~1.JAR|access-bridge-32.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\access-bridge-32.jar</td><td>1</td><td/></row>
		<row><td>accessibility.properties</td><td>ISX_DEFAULTCOMPONENT33</td><td>ACCESS~1.PRO|accessibility.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\accessibility.properties</td><td>1</td><td/></row>
		<row><td>adb.exe</td><td>adb.exe</td><td>adb.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Tools\Adb\adb.exe</td><td>1</td><td/></row>
		<row><td>adbwinapi.dll</td><td>AdbWinApi.dll</td><td>ADBWIN~1.DLL|AdbWinApi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Tools\Adb\AdbWinApi.dll</td><td>1</td><td/></row>
		<row><td>adbwinusbapi.dll</td><td>AdbWinUsbApi.dll</td><td>ADBWIN~1.DLL|AdbWinUsbApi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Tools\Adb\AdbWinUsbApi.dll</td><td>1</td><td/></row>
		<row><td>adinit.dat</td><td>ISX_DEFAULTCOMPONENT156</td><td>adinit.dat</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\adinit.dat</td><td>1</td><td/></row>
		<row><td>app_release.apk</td><td>ISX_DEFAULTCOMPONENT173</td><td>APP-RE~1.APK|app-release.apk</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Tools\Apk\app-release.apk</td><td>1</td><td/></row>
		<row><td>app_release_1.1.0.apk</td><td>ISX_DEFAULTCOMPONENT172</td><td>APP-RE~1.APK|app-release-1.1.0.apk</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Tools\Apk\app-release-1.1.0.apk</td><td>1</td><td/></row>
		<row><td>awt.dll</td><td>awt.dll</td><td>awt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\awt.dll</td><td>1</td><td/></row>
		<row><td>bci.dll</td><td>bci.dll</td><td>bci.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\bci.dll</td><td>1</td><td/></row>
		<row><td>blacklist</td><td>ISX_DEFAULTCOMPONENT129</td><td>BLACKL~1|blacklist</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\blacklist</td><td>1</td><td/></row>
		<row><td>blacklisted.certs</td><td>ISX_DEFAULTCOMPONENT130</td><td>BLACKL~1.CER|blacklisted.certs</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\blacklisted.certs</td><td>1</td><td/></row>
		<row><td>bouncycastle.crypto.dll2</td><td>BouncyCastle.Crypto.dll2</td><td>BOUNCY~1.DLL|BouncyCastle.Crypto.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\BouncyCastle.Crypto.dll</td><td>1</td><td/></row>
		<row><td>cacerts</td><td>ISX_DEFAULTCOMPONENT131</td><td>cacerts</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\cacerts</td><td>1</td><td/></row>
		<row><td>calendars.properties</td><td>ISX_DEFAULTCOMPONENT35</td><td>CALEND~1.PRO|calendars.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\calendars.properties</td><td>1</td><td/></row>
		<row><td>cdfc.parse.android.dll</td><td>CDFC.Parse.Android.dll</td><td>CDFCPA~1.DLL|CDFC.Parse.Android.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\CDFC.Parse.Android.dll</td><td>1</td><td/></row>
		<row><td>cdfc.parse.dll</td><td>CDFC.Parse.dll</td><td>CDFCPA~1.DLL|CDFC.Parse.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\CDFC.Parse.dll</td><td>1</td><td/></row>
		<row><td>cdfc.parse.local.dll</td><td>CDFC.Parse.Local.dll</td><td>CDFCPA~1.DLL|CDFC.Parse.Local.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\CDFC.Parse.Local.dll</td><td>1</td><td/></row>
		<row><td>cdfc.parse.signature.dll</td><td>CDFC.Parse.Signature.dll</td><td>CDFCPA~1.DLL|CDFC.Parse.Signature.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\CDFC.Parse.Signature.dll</td><td>1</td><td/></row>
		<row><td>cdfc.pinvoke.dll</td><td>CDFC.PInvoke.dll</td><td>CDFCPI~1.DLL|CDFC.PInvoke.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\CDFC.PInvoke.dll</td><td>1</td><td/></row>
		<row><td>cdfc.previewers.contracts.dl</td><td>CDFC.Previewers.Contracts.dll</td><td>CDFCPR~1.DLL|CDFC.Previewers.Contracts.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\CDFC.Previewers.Contracts.dll</td><td>1</td><td/></row>
		<row><td>cdfc.singularity.forensics.d</td><td>CDFC.Singularity.Forensics.dll</td><td>CDFCSI~1.DLL|CDFC.Singularity.Forensics.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\CDFC.Singularity.Forensics.dll</td><td>1</td><td/></row>
		<row><td>cdfccontrols.dll</td><td>CDFCControls.dll</td><td>CDFCCO~1.DLL|CDFCControls.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\CDFCControls.dll</td><td>1</td><td/></row>
		<row><td>cdfccultures.dll</td><td>CDFCCultures.dll</td><td>CDFCCU~1.DLL|CDFCCultures.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\CDFCCultures.dll</td><td>1</td><td/></row>
		<row><td>cdfchexaeditor.dll</td><td>CDFCHexaEditor.dll</td><td>CDFCHE~1.DLL|CDFCHexaEditor.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\CDFCHexaEditor.dll</td><td>1</td><td/></row>
		<row><td>cdfcmessageboxes.dll</td><td>CDFCMessageBoxes.dll</td><td>CDFCME~1.DLL|CDFCMessageBoxes.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\CDFCMessageBoxes.dll</td><td>1</td><td/></row>
		<row><td>cdfcmessageboxes.xaml</td><td>ISX_DEFAULTCOMPONENT149</td><td>CDFCME~1.XAM|CDFCMessageBoxes.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Languages\en_US\CDFCMessageBoxes.xaml</td><td>1</td><td/></row>
		<row><td>cdfcphoto.dll</td><td>cdfcphoto.dll</td><td>CDFCPH~1.DLL|cdfcphoto.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\cdfcphoto.dll</td><td>1</td><td/></row>
		<row><td>cdfcqd.dll1</td><td>cdfcqd.dll1</td><td>cdfcqd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\cdfcqd.dll</td><td>1</td><td/></row>
		<row><td>cdfcqd.dll3</td><td>cdfcqd.dll3</td><td>cdfcqd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\cdfcqd.dll</td><td>1</td><td/></row>
		<row><td>cdfcuicontracts.dll1</td><td>CDFCUIContracts.dll1</td><td>CDFCUI~1.DLL|CDFCUIContracts.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\CDFCUIContracts.dll</td><td>1</td><td/></row>
		<row><td>cdfcuicontracts.dll3</td><td>CDFCUIContracts.dll3</td><td>CDFCUI~1.DLL|CDFCUIContracts.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\CDFCUIContracts.dll</td><td>1</td><td/></row>
		<row><td>cflab.datatransport.dll2</td><td>Cflab.DataTransport.dll2</td><td>CFLABD~1.DLL|Cflab.DataTransport.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Cflab.DataTransport.dll</td><td>1</td><td/></row>
		<row><td>cflab.datatransport.pdb2</td><td>ISX_DEFAULTCOMPONENT185</td><td>CFLABD~1.PDB|Cflab.DataTransport.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Cflab.DataTransport.pdb</td><td>1</td><td/></row>
		<row><td>cflab.datatransport.xml2</td><td>ISX_DEFAULTCOMPONENT186</td><td>CFLABD~1.XML|Cflab.DataTransport.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Cflab.DataTransport.xml</td><td>1</td><td/></row>
		<row><td>charsets.jar</td><td>ISX_DEFAULTCOMPONENT36</td><td>charsets.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\charsets.jar</td><td>1</td><td/></row>
		<row><td>ciexyz.pf</td><td>ISX_DEFAULTCOMPONENT39</td><td>CIEXYZ.pf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\cmm\CIEXYZ.pf</td><td>1</td><td/></row>
		<row><td>classes.jsa</td><td>ISX_DEFAULTCOMPONENT21</td><td>classes.jsa</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\client\classes.jsa</td><td>1</td><td/></row>
		<row><td>classlist</td><td>ISX_DEFAULTCOMPONENT37</td><td>CLASSL~1|classlist</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\classlist</td><td>1</td><td/></row>
		<row><td>cldrdata.jar</td><td>ISX_DEFAULTCOMPONENT67</td><td>cldrdata.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\cldrdata.jar</td><td>1</td><td/></row>
		<row><td>cmmap000.bin</td><td>ISX_DEFAULTCOMPONENT157</td><td>cmmap000.bin</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\cmmap000.bin</td><td>1</td><td/></row>
		<row><td>content_types.properties</td><td>ISX_DEFAULTCOMPONENT44</td><td>CONTEN~1.PRO|content-types.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\content-types.properties</td><td>1</td><td/></row>
		<row><td>copyright</td><td>ISX_DEFAULTCOMPONENT31</td><td>COPYRI~1|COPYRIGHT</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\COPYRIGHT</td><td>1</td><td/></row>
		<row><td>create.jar</td><td>ISX_DEFAULTCOMPONENT23</td><td>Create.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\Create.jar</td><td>1</td><td/></row>
		<row><td>currency.data</td><td>ISX_DEFAULTCOMPONENT45</td><td>CURREN~1.DAT|currency.data</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\currency.data</td><td>1</td><td/></row>
		<row><td>cursors.properties</td><td>ISX_DEFAULTCOMPONENT96</td><td>CURSOR~1.PRO|cursors.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\cursors.properties</td><td>1</td><td/></row>
		<row><td>dcpr.dll</td><td>dcpr.dll</td><td>dcpr.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\dcpr.dll</td><td>1</td><td/></row>
		<row><td>debmp.dll</td><td>debmp.dll</td><td>debmp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\debmp.dll</td><td>1</td><td/></row>
		<row><td>decora_sse.dll</td><td>decora_sse.dll</td><td>DECORA~1.DLL|decora_sse.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\decora_sse.dll</td><td>1</td><td/></row>
		<row><td>default.jfc</td><td>ISX_DEFAULTCOMPONENT108</td><td>default.jfc</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jfr\default.jfc</td><td>1</td><td/></row>
		<row><td>dehex.dll</td><td>dehex.dll</td><td>dehex.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\dehex.dll</td><td>1</td><td/></row>
		<row><td>deploy.dll</td><td>deploy.dll</td><td>deploy.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\deploy.dll</td><td>1</td><td/></row>
		<row><td>deploy.jar</td><td>ISX_DEFAULTCOMPONENT64</td><td>deploy.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy.jar</td><td>1</td><td/></row>
		<row><td>deployjava1.dll</td><td>deployJava1.dll</td><td>DEPLOY~1.DLL|deployJava1.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\dtplugin\deployJava1.dll</td><td>1</td><td/></row>
		<row><td>dess.dll</td><td>dess.dll</td><td>dess.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\dess.dll</td><td>1</td><td/></row>
		<row><td>detree.dll</td><td>detree.dll</td><td>detree.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\detree.dll</td><td>1</td><td/></row>
		<row><td>devect.dll</td><td>devect.dll</td><td>devect.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\devect.dll</td><td>1</td><td/></row>
		<row><td>dewp.dll</td><td>dewp.dll</td><td>dewp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\dewp.dll</td><td>1</td><td/></row>
		<row><td>directoutin.dll2</td><td>DirectOutIn.dll2</td><td>DIRECT~1.DLL|DirectOutIn.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\DirectOutIn.dll</td><td>1</td><td/></row>
		<row><td>dnsns.jar</td><td>ISX_DEFAULTCOMPONENT68</td><td>dnsns.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\dnsns.jar</td><td>1</td><td/></row>
		<row><td>dt_shmem.dll</td><td>dt_shmem.dll</td><td>dt_shmem.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\dt_shmem.dll</td><td>1</td><td/></row>
		<row><td>dt_socket.dll</td><td>dt_socket.dll</td><td>DT_SOC~1.DLL|dt_socket.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\dt_socket.dll</td><td>1</td><td/></row>
		<row><td>eula.dll</td><td>eula.dll</td><td>eula.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\eula.dll</td><td>1</td><td/></row>
		<row><td>eventlogger.dll</td><td>EventLogger.dll</td><td>EVENTL~1.DLL|EventLogger.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\EventLogger.dll</td><td>1</td><td/></row>
		<row><td>ffjcext.zip</td><td>ISX_DEFAULTCOMPONENT47</td><td>ffjcext.zip</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\ffjcext.zip</td><td>1</td><td/></row>
		<row><td>flavormap.properties</td><td>ISX_DEFAULTCOMPONENT79</td><td>FLAVOR~1.PRO|flavormap.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\flavormap.properties</td><td>1</td><td/></row>
		<row><td>fontconfig.bfc</td><td>ISX_DEFAULTCOMPONENT80</td><td>FONTCO~1.BFC|fontconfig.bfc</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fontconfig.bfc</td><td>1</td><td/></row>
		<row><td>fontconfig.properties.src</td><td>ISX_DEFAULTCOMPONENT81</td><td>FONTCO~1.SRC|fontconfig.properties.src</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fontconfig.properties.src</td><td>1</td><td/></row>
		<row><td>fontmanager.dll</td><td>fontmanager.dll</td><td>FONTMA~1.DLL|fontmanager.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\fontmanager.dll</td><td>1</td><td/></row>
		<row><td>fxplugins.dll</td><td>fxplugins.dll</td><td>FXPLUG~1.DLL|fxplugins.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\fxplugins.dll</td><td>1</td><td/></row>
		<row><td>glass.dll</td><td>glass.dll</td><td>glass.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\glass.dll</td><td>1</td><td/></row>
		<row><td>glib_lite.dll</td><td>glib_lite.dll</td><td>GLIB-L~1.DLL|glib-lite.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\glib-lite.dll</td><td>1</td><td/></row>
		<row><td>gray.pf</td><td>ISX_DEFAULTCOMPONENT40</td><td>GRAY.pf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\cmm\GRAY.pf</td><td>1</td><td/></row>
		<row><td>gstreamer_lite.dll</td><td>gstreamer_lite.dll</td><td>GSTREA~1.DLL|gstreamer-lite.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\gstreamer-lite.dll</td><td>1</td><td/></row>
		<row><td>hijrah_config_umalqura.prope</td><td>ISX_DEFAULTCOMPONENT91</td><td>HIJRAH~1.PRO|hijrah-config-umalqura.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\hijrah-config-umalqura.properties</td><td>1</td><td/></row>
		<row><td>hprof.dll</td><td>hprof.dll</td><td>hprof.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\hprof.dll</td><td>1</td><td/></row>
		<row><td>ibfpx2.dll</td><td>ibfpx2.dll</td><td>ibfpx2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibfpx2.dll</td><td>1</td><td/></row>
		<row><td>ibgp42.dll</td><td>ibgp42.dll</td><td>ibgp42.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibgp42.dll</td><td>1</td><td/></row>
		<row><td>ibjpg2.dll</td><td>ibjpg2.dll</td><td>ibjpg2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibjpg2.dll</td><td>1</td><td/></row>
		<row><td>ibpcd2.dll</td><td>ibpcd2.dll</td><td>ibpcd2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibpcd2.dll</td><td>1</td><td/></row>
		<row><td>ibpsd2.dll</td><td>ibpsd2.dll</td><td>ibpsd2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibpsd2.dll</td><td>1</td><td/></row>
		<row><td>ibxbm2.dll</td><td>ibxbm2.dll</td><td>ibxbm2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibxbm2.dll</td><td>1</td><td/></row>
		<row><td>ibxpm2.dll</td><td>ibxpm2.dll</td><td>ibxpm2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibxpm2.dll</td><td>1</td><td/></row>
		<row><td>ibxwd2.dll</td><td>ibxwd2.dll</td><td>ibxwd2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ibxwd2.dll</td><td>1</td><td/></row>
		<row><td>imcd32.dll</td><td>imcd32.dll</td><td>imcd32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd32.dll</td><td>1</td><td/></row>
		<row><td>imcd42.dll</td><td>imcd42.dll</td><td>imcd42.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd42.dll</td><td>1</td><td/></row>
		<row><td>imcd52.dll</td><td>imcd52.dll</td><td>imcd52.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd52.dll</td><td>1</td><td/></row>
		<row><td>imcd62.dll</td><td>imcd62.dll</td><td>imcd62.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd62.dll</td><td>1</td><td/></row>
		<row><td>imcd72.dll</td><td>imcd72.dll</td><td>imcd72.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd72.dll</td><td>1</td><td/></row>
		<row><td>imcd82.dll</td><td>imcd82.dll</td><td>imcd82.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcd82.dll</td><td>1</td><td/></row>
		<row><td>imcdr2.dll</td><td>imcdr2.dll</td><td>imcdr2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcdr2.dll</td><td>1</td><td/></row>
		<row><td>imcm52.dll</td><td>imcm52.dll</td><td>imcm52.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcm52.dll</td><td>1</td><td/></row>
		<row><td>imcm72.dll</td><td>imcm72.dll</td><td>imcm72.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcm72.dll</td><td>1</td><td/></row>
		<row><td>imcmx2.dll</td><td>imcmx2.dll</td><td>imcmx2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imcmx2.dll</td><td>1</td><td/></row>
		<row><td>imdsf2.dll</td><td>imdsf2.dll</td><td>imdsf2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imdsf2.dll</td><td>1</td><td/></row>
		<row><td>imfmv2.dll</td><td>imfmv2.dll</td><td>imfmv2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imfmv2.dll</td><td>1</td><td/></row>
		<row><td>imgdf2.dll</td><td>imgdf2.dll</td><td>imgdf2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imgdf2.dll</td><td>1</td><td/></row>
		<row><td>imgem2.dll</td><td>imgem2.dll</td><td>imgem2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imgem2.dll</td><td>1</td><td/></row>
		<row><td>imigs2.dll</td><td>imigs2.dll</td><td>imigs2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imigs2.dll</td><td>1</td><td/></row>
		<row><td>immet2.dll</td><td>immet2.dll</td><td>immet2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\immet2.dll</td><td>1</td><td/></row>
		<row><td>impif2.dll</td><td>impif2.dll</td><td>impif2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\impif2.dll</td><td>1</td><td/></row>
		<row><td>imps_2.dll</td><td>imps_2.dll</td><td>imps_2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imps_2.dll</td><td>1</td><td/></row>
		<row><td>impsi2.dll</td><td>impsi2.dll</td><td>impsi2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\impsi2.dll</td><td>1</td><td/></row>
		<row><td>impsz2.dll</td><td>impsz2.dll</td><td>impsz2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\impsz2.dll</td><td>1</td><td/></row>
		<row><td>imrnd2.dll</td><td>imrnd2.dll</td><td>imrnd2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\imrnd2.dll</td><td>1</td><td/></row>
		<row><td>instrument.dll</td><td>instrument.dll</td><td>INSTRU~1.DLL|instrument.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\instrument.dll</td><td>1</td><td/></row>
		<row><td>invalid32x32.gif</td><td>ISX_DEFAULTCOMPONENT97</td><td>INVALI~1.GIF|invalid32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\invalid32x32.gif</td><td>1</td><td/></row>
		<row><td>iphgw2.dll</td><td>iphgw2.dll</td><td>iphgw2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\iphgw2.dll</td><td>1</td><td/></row>
		<row><td>isgdi32.dll</td><td>isgdi32.dll</td><td>isgdi32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\isgdi32.dll</td><td>1</td><td/></row>
		<row><td>j2pcsc.dll</td><td>j2pcsc.dll</td><td>j2pcsc.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\j2pcsc.dll</td><td>1</td><td/></row>
		<row><td>j2pkcs11.dll</td><td>j2pkcs11.dll</td><td>j2pkcs11.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\j2pkcs11.dll</td><td>1</td><td/></row>
		<row><td>jaas_nt.dll</td><td>jaas_nt.dll</td><td>jaas_nt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jaas_nt.dll</td><td>1</td><td/></row>
		<row><td>jabswitch.exe</td><td>jabswitch.exe</td><td>JABSWI~1.EXE|jabswitch.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jabswitch.exe</td><td>1</td><td/></row>
		<row><td>jaccess.jar</td><td>ISX_DEFAULTCOMPONENT69</td><td>jaccess.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\jaccess.jar</td><td>1</td><td/></row>
		<row><td>java.dll</td><td>java.dll</td><td>java.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\java.dll</td><td>1</td><td/></row>
		<row><td>java.exe</td><td>java.exe</td><td>java.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\java.exe</td><td>1</td><td/></row>
		<row><td>java.policy</td><td>ISX_DEFAULTCOMPONENT132</td><td>JAVA~1.POL|java.policy</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\java.policy</td><td>1</td><td/></row>
		<row><td>java.security</td><td>ISX_DEFAULTCOMPONENT133</td><td>JAVA~1.SEC|java.security</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\java.security</td><td>1</td><td/></row>
		<row><td>java_crw_demo.dll</td><td>java_crw_demo.dll</td><td>JAVA_C~1.DLL|java_crw_demo.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\java_crw_demo.dll</td><td>1</td><td/></row>
		<row><td>java_rmi.exe</td><td>java_rmi.exe</td><td>java-rmi.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\java-rmi.exe</td><td>1</td><td/></row>
		<row><td>javaaccessbridge_32.dll</td><td>JavaAccessBridge_32.dll</td><td>JAVAAC~1.DLL|JavaAccessBridge-32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\JavaAccessBridge-32.dll</td><td>1</td><td/></row>
		<row><td>javacpl.cpl</td><td>ISX_DEFAULTCOMPONENT27</td><td>javacpl.cpl</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javacpl.cpl</td><td>1</td><td/></row>
		<row><td>javacpl.exe</td><td>javacpl.exe</td><td>javacpl.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javacpl.exe</td><td>1</td><td/></row>
		<row><td>javafx.properties</td><td>ISX_DEFAULTCOMPONENT104</td><td>JAVAFX~1.PRO|javafx.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\javafx.properties</td><td>1</td><td/></row>
		<row><td>javafx_font.dll</td><td>javafx_font.dll</td><td>JAVAFX~1.DLL|javafx_font.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javafx_font.dll</td><td>1</td><td/></row>
		<row><td>javafx_font_t2k.dll</td><td>javafx_font_t2k.dll</td><td>JAVAFX~1.DLL|javafx_font_t2k.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javafx_font_t2k.dll</td><td>1</td><td/></row>
		<row><td>javafx_iio.dll</td><td>javafx_iio.dll</td><td>JAVAFX~1.DLL|javafx_iio.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javafx_iio.dll</td><td>1</td><td/></row>
		<row><td>javaw.exe</td><td>javaw.exe</td><td>javaw.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javaw.exe</td><td>1</td><td/></row>
		<row><td>javaws.exe</td><td>javaws.exe</td><td>javaws.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\javaws.exe</td><td>1</td><td/></row>
		<row><td>javaws.jar</td><td>ISX_DEFAULTCOMPONENT105</td><td>javaws.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\javaws.jar</td><td>1</td><td/></row>
		<row><td>javaws.policy</td><td>ISX_DEFAULTCOMPONENT134</td><td>JAVAWS~1.POL|javaws.policy</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\javaws.policy</td><td>1</td><td/></row>
		<row><td>jawt.dll</td><td>jawt.dll</td><td>jawt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jawt.dll</td><td>1</td><td/></row>
		<row><td>jawtaccessbridge_32.dll</td><td>JAWTAccessBridge_32.dll</td><td>JAWTAC~1.DLL|JAWTAccessBridge-32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\JAWTAccessBridge-32.dll</td><td>1</td><td/></row>
		<row><td>jce.jar</td><td>ISX_DEFAULTCOMPONENT106</td><td>jce.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jce.jar</td><td>1</td><td/></row>
		<row><td>jdwp.dll</td><td>jdwp.dll</td><td>jdwp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jdwp.dll</td><td>1</td><td/></row>
		<row><td>jfr.dll</td><td>jfr.dll</td><td>jfr.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jfr.dll</td><td>1</td><td/></row>
		<row><td>jfr.jar</td><td>ISX_DEFAULTCOMPONENT110</td><td>jfr.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jfr.jar</td><td>1</td><td/></row>
		<row><td>jfxmedia.dll</td><td>jfxmedia.dll</td><td>jfxmedia.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jfxmedia.dll</td><td>1</td><td/></row>
		<row><td>jfxrt.jar</td><td>ISX_DEFAULTCOMPONENT70</td><td>jfxrt.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\jfxrt.jar</td><td>1</td><td/></row>
		<row><td>jfxswt.jar</td><td>ISX_DEFAULTCOMPONENT111</td><td>jfxswt.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jfxswt.jar</td><td>1</td><td/></row>
		<row><td>jfxwebkit.dll</td><td>jfxwebkit.dll</td><td>JFXWEB~1.DLL|jfxwebkit.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jfxwebkit.dll</td><td>1</td><td/></row>
		<row><td>jjs.exe</td><td>jjs.exe</td><td>jjs.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jjs.exe</td><td>1</td><td/></row>
		<row><td>jli.dll</td><td>jli.dll</td><td>jli.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jli.dll</td><td>1</td><td/></row>
		<row><td>jmxremote.access</td><td>ISX_DEFAULTCOMPONENT116</td><td>JMXREM~1.ACC|jmxremote.access</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\management\jmxremote.access</td><td>1</td><td/></row>
		<row><td>jmxremote.password.template</td><td>ISX_DEFAULTCOMPONENT117</td><td>JMXREM~1.TEM|jmxremote.password.template</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\management\jmxremote.password.template</td><td>1</td><td/></row>
		<row><td>jp2iexp.dll</td><td>jp2iexp.dll</td><td>jp2iexp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jp2iexp.dll</td><td>1</td><td/></row>
		<row><td>jp2launcher.exe</td><td>jp2launcher.exe</td><td>JP2LAU~1.EXE|jp2launcher.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jp2launcher.exe</td><td>1</td><td/></row>
		<row><td>jp2native.dll</td><td>jp2native.dll</td><td>JP2NAT~1.DLL|jp2native.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jp2native.dll</td><td>1</td><td/></row>
		<row><td>jp2ssv.dll</td><td>jp2ssv.dll</td><td>jp2ssv.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jp2ssv.dll</td><td>1</td><td/></row>
		<row><td>jpeg.dll</td><td>jpeg.dll</td><td>jpeg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jpeg.dll</td><td>1</td><td/></row>
		<row><td>jsdt.dll</td><td>jsdt.dll</td><td>jsdt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jsdt.dll</td><td>1</td><td/></row>
		<row><td>jsound.dll</td><td>jsound.dll</td><td>jsound.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jsound.dll</td><td>1</td><td/></row>
		<row><td>jsoundds.dll</td><td>jsoundds.dll</td><td>jsoundds.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\jsoundds.dll</td><td>1</td><td/></row>
		<row><td>jsse.jar</td><td>ISX_DEFAULTCOMPONENT112</td><td>jsse.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jsse.jar</td><td>1</td><td/></row>
		<row><td>jvm.cfg</td><td>ISX_DEFAULTCOMPONENT93</td><td>jvm.cfg</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\i386\jvm.cfg</td><td>1</td><td/></row>
		<row><td>jvm.dll</td><td>jvm.dll</td><td>jvm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\client\jvm.dll</td><td>1</td><td/></row>
		<row><td>jvm.hprof.txt</td><td>ISX_DEFAULTCOMPONENT113</td><td>JVMHPR~1.TXT|jvm.hprof.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jvm.hprof.txt</td><td>1</td><td/></row>
		<row><td>kcms.dll</td><td>kcms.dll</td><td>kcms.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\kcms.dll</td><td>1</td><td/></row>
		<row><td>keytool.exe</td><td>keytool.exe</td><td>keytool.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\keytool.exe</td><td>1</td><td/></row>
		<row><td>kinit.exe</td><td>kinit.exe</td><td>kinit.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\kinit.exe</td><td>1</td><td/></row>
		<row><td>klist.exe</td><td>klist.exe</td><td>klist.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\klist.exe</td><td>1</td><td/></row>
		<row><td>ktab.exe</td><td>ktab.exe</td><td>ktab.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\ktab.exe</td><td>1</td><td/></row>
		<row><td>lcms.dll</td><td>lcms.dll</td><td>lcms.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\lcms.dll</td><td>1</td><td/></row>
		<row><td>license</td><td>ISX_DEFAULTCOMPONENT141</td><td>LICENSE</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\LICENSE</td><td>1</td><td/></row>
		<row><td>linear_rgb.pf</td><td>ISX_DEFAULTCOMPONENT41</td><td>LINEAR~1.PF|LINEAR_RGB.pf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\cmm\LINEAR_RGB.pf</td><td>1</td><td/></row>
		<row><td>local_policy.jar</td><td>ISX_DEFAULTCOMPONENT135</td><td>LOCAL_~1.JAR|local_policy.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\local_policy.jar</td><td>1</td><td/></row>
		<row><td>localedata.jar</td><td>ISX_DEFAULTCOMPONENT71</td><td>LOCALE~1.JAR|localedata.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\localedata.jar</td><td>1</td><td/></row>
		<row><td>logging.properties</td><td>ISX_DEFAULTCOMPONENT114</td><td>LOGGIN~1.PRO|logging.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\logging.properties</td><td>1</td><td/></row>
		<row><td>lucidabrightdemibold.ttf</td><td>ISX_DEFAULTCOMPONENT83</td><td>LUCIDA~1.TTF|LucidaBrightDemiBold.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaBrightDemiBold.ttf</td><td>1</td><td/></row>
		<row><td>lucidabrightdemiitalic.ttf</td><td>ISX_DEFAULTCOMPONENT84</td><td>LUCIDA~1.TTF|LucidaBrightDemiItalic.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaBrightDemiItalic.ttf</td><td>1</td><td/></row>
		<row><td>lucidabrightitalic.ttf</td><td>ISX_DEFAULTCOMPONENT85</td><td>LUCIDA~1.TTF|LucidaBrightItalic.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaBrightItalic.ttf</td><td>1</td><td/></row>
		<row><td>lucidabrightregular.ttf</td><td>ISX_DEFAULTCOMPONENT86</td><td>LUCIDA~1.TTF|LucidaBrightRegular.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaBrightRegular.ttf</td><td>1</td><td/></row>
		<row><td>lucidasansdemibold.ttf</td><td>ISX_DEFAULTCOMPONENT87</td><td>LUCIDA~1.TTF|LucidaSansDemiBold.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaSansDemiBold.ttf</td><td>1</td><td/></row>
		<row><td>lucidasansregular.ttf</td><td>ISX_DEFAULTCOMPONENT88</td><td>LUCIDA~1.TTF|LucidaSansRegular.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaSansRegular.ttf</td><td>1</td><td/></row>
		<row><td>lucidatypewriterbold.ttf</td><td>ISX_DEFAULTCOMPONENT89</td><td>LUCIDA~1.TTF|LucidaTypewriterBold.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaTypewriterBold.ttf</td><td>1</td><td/></row>
		<row><td>lucidatypewriterregular.ttf</td><td>ISX_DEFAULTCOMPONENT90</td><td>LUCIDA~1.TTF|LucidaTypewriterRegular.ttf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\fonts\LucidaTypewriterRegular.ttf</td><td>1</td><td/></row>
		<row><td>mahapps.metro.dll1</td><td>MahApps.Metro.dll1</td><td>MAHAPP~1.DLL|MahApps.Metro.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Resources\MahApps.Metro.dll</td><td>1</td><td/></row>
		<row><td>mahapps.metro.dll3</td><td>MahApps.Metro.dll3</td><td>MAHAPP~1.DLL|MahApps.Metro.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\MahApps.Metro.dll</td><td>1</td><td/></row>
		<row><td>mahapps.metro.pdb2</td><td>ISX_DEFAULTCOMPONENT187</td><td>MAHAPP~1.PDB|MahApps.Metro.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\MahApps.Metro.pdb</td><td>1</td><td/></row>
		<row><td>mahapps.metro.xml2</td><td>ISX_DEFAULTCOMPONENT188</td><td>MAHAPP~1.XML|MahApps.Metro.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\MahApps.Metro.xml</td><td>1</td><td/></row>
		<row><td>management.dll</td><td>management.dll</td><td>MANAGE~1.DLL|management.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\management.dll</td><td>1</td><td/></row>
		<row><td>management.properties</td><td>ISX_DEFAULTCOMPONENT118</td><td>MANAGE~1.PRO|management.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\management\management.properties</td><td>1</td><td/></row>
		<row><td>management_agent.jar</td><td>ISX_DEFAULTCOMPONENT120</td><td>MANAGE~1.JAR|management-agent.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\management-agent.jar</td><td>1</td><td/></row>
		<row><td>messages.properties</td><td>ISX_DEFAULTCOMPONENT48</td><td>MESSAG~1.PRO|messages.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages.properties</td><td>1</td><td/></row>
		<row><td>messages_de.properties</td><td>ISX_DEFAULTCOMPONENT49</td><td>MESSAG~1.PRO|messages_de.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_de.properties</td><td>1</td><td/></row>
		<row><td>messages_es.properties</td><td>ISX_DEFAULTCOMPONENT50</td><td>MESSAG~1.PRO|messages_es.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_es.properties</td><td>1</td><td/></row>
		<row><td>messages_fr.properties</td><td>ISX_DEFAULTCOMPONENT51</td><td>MESSAG~1.PRO|messages_fr.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_fr.properties</td><td>1</td><td/></row>
		<row><td>messages_it.properties</td><td>ISX_DEFAULTCOMPONENT52</td><td>MESSAG~1.PRO|messages_it.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_it.properties</td><td>1</td><td/></row>
		<row><td>messages_ja.properties</td><td>ISX_DEFAULTCOMPONENT53</td><td>MESSAG~1.PRO|messages_ja.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_ja.properties</td><td>1</td><td/></row>
		<row><td>messages_ko.properties</td><td>ISX_DEFAULTCOMPONENT54</td><td>MESSAG~1.PRO|messages_ko.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_ko.properties</td><td>1</td><td/></row>
		<row><td>messages_pt_br.properties</td><td>ISX_DEFAULTCOMPONENT55</td><td>MESSAG~1.PRO|messages_pt_BR.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_pt_BR.properties</td><td>1</td><td/></row>
		<row><td>messages_sv.properties</td><td>ISX_DEFAULTCOMPONENT56</td><td>MESSAG~1.PRO|messages_sv.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_sv.properties</td><td>1</td><td/></row>
		<row><td>messages_zh_cn.properties</td><td>ISX_DEFAULTCOMPONENT57</td><td>MESSAG~1.PRO|messages_zh_CN.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_zh_CN.properties</td><td>1</td><td/></row>
		<row><td>messages_zh_hk.properties</td><td>ISX_DEFAULTCOMPONENT58</td><td>MESSAG~1.PRO|messages_zh_HK.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_zh_HK.properties</td><td>1</td><td/></row>
		<row><td>messages_zh_tw.properties</td><td>ISX_DEFAULTCOMPONENT59</td><td>MESSAG~1.PRO|messages_zh_TW.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\messages_zh_TW.properties</td><td>1</td><td/></row>
		<row><td>meta_index</td><td>ISX_DEFAULTCOMPONENT72</td><td>META-I~1|meta-index</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\meta-index</td><td>1</td><td/></row>
		<row><td>meta_index1</td><td>ISX_DEFAULTCOMPONENT121</td><td>META-I~1|meta-index</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\meta-index</td><td>1</td><td/></row>
		<row><td>microsoft.practices.servicel4</td><td>Microsoft.Practices.ServiceLocation.dll2</td><td>MICROS~1.DLL|Microsoft.Practices.ServiceLocation.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Microsoft.Practices.ServiceLocation.dll</td><td>1</td><td/></row>
		<row><td>microsoft.practices.servicel5</td><td>ISX_DEFAULTCOMPONENT189</td><td>MICROS~1.PDB|Microsoft.Practices.ServiceLocation.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Microsoft.Practices.ServiceLocation.pdb</td><td>1</td><td/></row>
		<row><td>microsoft.practices.servicel6</td><td>ISX_DEFAULTCOMPONENT190</td><td>MICROS~1.XML|Microsoft.Practices.ServiceLocation.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Microsoft.Practices.ServiceLocation.xml</td><td>1</td><td/></row>
		<row><td>mlib_image.dll</td><td>mlib_image.dll</td><td>MLIB_I~1.DLL|mlib_image.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\mlib_image.dll</td><td>1</td><td/></row>
		<row><td>msvcp120.dll</td><td>msvcp120.dll</td><td>msvcp120.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\msvcp120.dll</td><td>1</td><td/></row>
		<row><td>msvcr100.dll</td><td>msvcr100.dll</td><td>msvcr100.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\msvcr100.dll</td><td>1</td><td/></row>
		<row><td>msvcr100.dll1</td><td>msvcr100.dll1</td><td>msvcr100.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\plugin2\msvcr100.dll</td><td>1</td><td/></row>
		<row><td>msvcr120.dll</td><td>msvcr120.dll</td><td>msvcr120.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\msvcr120.dll</td><td>1</td><td/></row>
		<row><td>nashorn.jar</td><td>ISX_DEFAULTCOMPONENT73</td><td>nashorn.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\nashorn.jar</td><td>1</td><td/></row>
		<row><td>net.dll</td><td>net.dll</td><td>net.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\net.dll</td><td>1</td><td/></row>
		<row><td>net.properties</td><td>ISX_DEFAULTCOMPONENT122</td><td>NET~1.PRO|net.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\net.properties</td><td>1</td><td/></row>
		<row><td>newtonsoft.json.dll2</td><td>Newtonsoft.Json.dll2</td><td>NEWTON~1.DLL|Newtonsoft.Json.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Newtonsoft.Json.dll</td><td>1</td><td/></row>
		<row><td>nio.dll</td><td>nio.dll</td><td>nio.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\nio.dll</td><td>1</td><td/></row>
		<row><td>npdeployjava1.dll</td><td>npdeployJava1.dll</td><td>NPDEPL~1.DLL|npdeployJava1.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\dtplugin\npdeployJava1.dll</td><td>1</td><td/></row>
		<row><td>npjp2.dll</td><td>npjp2.dll</td><td>npjp2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\plugin2\npjp2.dll</td><td>1</td><td/></row>
		<row><td>npt.dll</td><td>npt.dll</td><td>npt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\npt.dll</td><td>1</td><td/></row>
		<row><td>ocemul.dll</td><td>ocemul.dll</td><td>ocemul.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\ocemul.dll</td><td>1</td><td/></row>
		<row><td>oit_font_metrics.db</td><td>ISX_DEFAULTCOMPONENT159</td><td>OIT_FO~1.DB|oit_font_metrics.db</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\oit_font_metrics.db</td><td>1</td><td/></row>
		<row><td>oitnsf.id</td><td>ISX_DEFAULTCOMPONENT158</td><td>oitnsf.id</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\oitnsf.id</td><td>1</td><td/></row>
		<row><td>ookii.dialogs.wpf.dll</td><td>Ookii.Dialogs.Wpf.dll</td><td>OOKIID~1.DLL|Ookii.Dialogs.Wpf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Ookii.Dialogs.Wpf.dll</td><td>1</td><td/></row>
		<row><td>orbd.exe</td><td>orbd.exe</td><td>orbd.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\orbd.exe</td><td>1</td><td/></row>
		<row><td>oswin32.dll</td><td>oswin32.dll</td><td>oswin32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\oswin32.dll</td><td>1</td><td/></row>
		<row><td>pack200.exe</td><td>pack200.exe</td><td>pack200.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\pack200.exe</td><td>1</td><td/></row>
		<row><td>plugin.jar</td><td>ISX_DEFAULTCOMPONENT123</td><td>plugin.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\plugin.jar</td><td>1</td><td/></row>
		<row><td>policytool.exe</td><td>policytool.exe</td><td>POLICY~1.EXE|policytool.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\policytool.exe</td><td>1</td><td/></row>
		<row><td>prism.dll2</td><td>Prism.dll2</td><td>Prism.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.dll</td><td>1</td><td/></row>
		<row><td>prism.mef.wpf.dll2</td><td>Prism.Mef.Wpf.dll2</td><td>PRISMM~1.DLL|Prism.Mef.Wpf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.Mef.Wpf.dll</td><td>1</td><td/></row>
		<row><td>prism.mef.wpf.xml2</td><td>ISX_DEFAULTCOMPONENT191</td><td>PRISMM~1.XML|Prism.Mef.Wpf.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.Mef.Wpf.xml</td><td>1</td><td/></row>
		<row><td>prism.wpf.dll2</td><td>Prism.Wpf.dll2</td><td>PRISMW~1.DLL|Prism.Wpf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.Wpf.dll</td><td>1</td><td/></row>
		<row><td>prism.wpf.xml2</td><td>ISX_DEFAULTCOMPONENT192</td><td>PRISMW~1.XML|Prism.Wpf.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.Wpf.xml</td><td>1</td><td/></row>
		<row><td>prism.xml2</td><td>ISX_DEFAULTCOMPONENT193</td><td>Prism.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Prism.xml</td><td>1</td><td/></row>
		<row><td>prism_common.dll</td><td>prism_common.dll</td><td>PRISM_~1.DLL|prism_common.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\prism_common.dll</td><td>1</td><td/></row>
		<row><td>prism_d3d.dll</td><td>prism_d3d.dll</td><td>PRISM_~1.DLL|prism_d3d.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\prism_d3d.dll</td><td>1</td><td/></row>
		<row><td>prism_sw.dll</td><td>prism_sw.dll</td><td>prism_sw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\prism_sw.dll</td><td>1</td><td/></row>
		<row><td>profile.jfc</td><td>ISX_DEFAULTCOMPONENT109</td><td>profile.jfc</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\jfr\profile.jfc</td><td>1</td><td/></row>
		<row><td>psfont.properties.ja</td><td>ISX_DEFAULTCOMPONENT124</td><td>PSFONT~1.JA|psfont.properties.ja</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\psfont.properties.ja</td><td>1</td><td/></row>
		<row><td>psfontj2d.properties</td><td>ISX_DEFAULTCOMPONENT125</td><td>PSFONT~1.PRO|psfontj2d.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\psfontj2d.properties</td><td>1</td><td/></row>
		<row><td>pycc.pf</td><td>ISX_DEFAULTCOMPONENT42</td><td>PYCC.pf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\cmm\PYCC.pf</td><td>1</td><td/></row>
		<row><td>readme.txt</td><td>ISX_DEFAULTCOMPONENT142</td><td>README.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\README.txt</td><td>1</td><td/></row>
		<row><td>release</td><td>ISX_DEFAULTCOMPONENT143</td><td>release</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\release</td><td>1</td><td/></row>
		<row><td>resource.dll</td><td>resource.dll</td><td>resource.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\resource.dll</td><td>1</td><td/></row>
		<row><td>resources.jar</td><td>ISX_DEFAULTCOMPONENT126</td><td>RESOUR~1.JAR|resources.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\resources.jar</td><td>1</td><td/></row>
		<row><td>rmid.exe</td><td>rmid.exe</td><td>rmid.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\rmid.exe</td><td>1</td><td/></row>
		<row><td>rmiregistry.exe</td><td>rmiregistry.exe</td><td>RMIREG~1.EXE|rmiregistry.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\rmiregistry.exe</td><td>1</td><td/></row>
		<row><td>rt.jar</td><td>ISX_DEFAULTCOMPONENT127</td><td>rt.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\rt.jar</td><td>1</td><td/></row>
		<row><td>sccanno.dll</td><td>sccanno.dll</td><td>sccanno.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccanno.dll</td><td>1</td><td/></row>
		<row><td>sccca.dll</td><td>sccca.dll</td><td>sccca.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccca.dll</td><td>1</td><td/></row>
		<row><td>sccch.dll</td><td>sccch.dll</td><td>sccch.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccch.dll</td><td>1</td><td/></row>
		<row><td>sccda.dll</td><td>sccda.dll</td><td>sccda.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccda.dll</td><td>1</td><td/></row>
		<row><td>sccdu.dll</td><td>sccdu.dll</td><td>sccdu.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccdu.dll</td><td>1</td><td/></row>
		<row><td>sccfa.dll</td><td>sccfa.dll</td><td>sccfa.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccfa.dll</td><td>1</td><td/></row>
		<row><td>sccfi.dll</td><td>sccfi.dll</td><td>sccfi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccfi.dll</td><td>1</td><td/></row>
		<row><td>sccfmt.dll</td><td>sccfmt.dll</td><td>sccfmt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccfmt.dll</td><td>1</td><td/></row>
		<row><td>sccfnt.dll</td><td>sccfnt.dll</td><td>sccfnt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccfnt.dll</td><td>1</td><td/></row>
		<row><td>sccfut.dll</td><td>sccfut.dll</td><td>sccfut.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccfut.dll</td><td>1</td><td/></row>
		<row><td>sccind.dll</td><td>sccind.dll</td><td>sccind.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccind.dll</td><td>1</td><td/></row>
		<row><td>scclo.dll</td><td>scclo.dll</td><td>scclo.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\scclo.dll</td><td>1</td><td/></row>
		<row><td>sccole.dll</td><td>sccole.dll</td><td>sccole.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccole.dll</td><td>1</td><td/></row>
		<row><td>sccra.dll</td><td>sccra.dll</td><td>sccra.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccra.dll</td><td>1</td><td/></row>
		<row><td>sccsd.dll</td><td>sccsd.dll</td><td>sccsd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccsd.dll</td><td>1</td><td/></row>
		<row><td>sccta.dll</td><td>sccta.dll</td><td>sccta.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccta.dll</td><td>1</td><td/></row>
		<row><td>sccut.dll</td><td>sccut.dll</td><td>sccut.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccut.dll</td><td>1</td><td/></row>
		<row><td>sccvw.dll</td><td>sccvw.dll</td><td>sccvw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccvw.dll</td><td>1</td><td/></row>
		<row><td>sccxt.dll</td><td>sccxt.dll</td><td>sccxt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\sccxt.dll</td><td>1</td><td/></row>
		<row><td>searcher.jar</td><td>ISX_DEFAULTCOMPONENT29</td><td>Searcher.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\Searcher.jar</td><td>1</td><td/></row>
		<row><td>servertool.exe</td><td>servertool.exe</td><td>SERVER~1.EXE|servertool.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\servertool.exe</td><td>1</td><td/></row>
		<row><td>sign.txt</td><td>ISX_DEFAULTCOMPONENT4</td><td>sign.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Attachments\sign.txt</td><td>1</td><td/></row>
		<row><td>singularity.fonts.dll</td><td>Singularity.Fonts.dll</td><td>SINGUL~1.DLL|Singularity.Fonts.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Resources\Singularity.Fonts.dll</td><td>1</td><td/></row>
		<row><td>singularity.previewers.dll</td><td>Singularity.Previewers.dll</td><td>SINGUL~1.DLL|Singularity.Previewers.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Singularity.Previewers.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.adbviewer.dll</td><td>Singularity.UI.AdbViewer.dll</td><td>SINGUL~1.DLL|Singularity.UI.AdbViewer.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Singularity.UI.AdbViewer.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.controls.dll</td><td>Singularity.UI.Controls.dll</td><td>SINGUL~1.DLL|Singularity.UI.Controls.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Singularity.UI.Controls.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.converters.dl</td><td>Singularity.UI.Converters.dll</td><td>SINGUL~1.DLL|Singularity.UI.Converters.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Singularity.UI.Converters.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.info.dll2</td><td>Singularity.UI.Info.dll2</td><td>SINGUL~1.DLL|Singularity.UI.Info.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Singularity.UI.Info.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.info.pdb2</td><td>ISX_DEFAULTCOMPONENT194</td><td>SINGUL~1.PDB|Singularity.UI.Info.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Singularity.UI.Info.pdb</td><td>1</td><td/></row>
		<row><td>singularity.ui.infrastructur</td><td>Singularity.UI.Infrastructure.dll</td><td>SINGUL~1.DLL|Singularity.UI.Infrastructure.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Common\Singularity.UI.Infrastructure.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.messageboxes.</td><td>Singularity.UI.MessageBoxes.dll</td><td>SINGUL~1.DLL|Singularity.UI.MessageBoxes.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Controls\Singularity.UI.MessageBoxes.dll</td><td>1</td><td/></row>
		<row><td>singularity.ui.themes.dll</td><td>Singularity.UI.Themes.dll</td><td>SINGUL~1.DLL|Singularity.UI.Themes.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Resources\Singularity.UI.Themes.dll</td><td>1</td><td/></row>
		<row><td>singularityforensic.dll</td><td>SingularityForensic.dll</td><td>SINGUL~1.DLL|SingularityForensic.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\SingularityForensic.dll</td><td>1</td><td/></row>
		<row><td>singularityshell.exe.config2</td><td>ISX_DEFAULTCOMPONENT195</td><td>SINGUL~1.CON|SingularityShell.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.exe.config</td><td>1</td><td/></row>
		<row><td>singularityshell.exe2</td><td>SingularityShell.exe2</td><td>SINGUL~1.EXE|SingularityShell.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.exe</td><td>1</td><td/></row>
		<row><td>singularityshell.pdb2</td><td>ISX_DEFAULTCOMPONENT196</td><td>SINGUL~1.PDB|SingularityShell.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.pdb</td><td>1</td><td/></row>
		<row><td>singularityshell.vshost.exe.3</td><td>ISX_DEFAULTCOMPONENT197</td><td>SINGUL~1.CON|SingularityShell.vshost.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.vshost.exe.config</td><td>1</td><td/></row>
		<row><td>singularityshell.vshost.exe.4</td><td>ISX_DEFAULTCOMPONENT198</td><td>SINGUL~1.MAN|SingularityShell.vshost.exe.manifest</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.vshost.exe.manifest</td><td>1</td><td/></row>
		<row><td>singularityshell.vshost.exe2</td><td>SingularityShell.vshost.exe2</td><td>SINGUL~1.EXE|SingularityShell.vshost.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.vshost.exe</td><td>1</td><td/></row>
		<row><td>singularityupdater.xaml</td><td>ISX_DEFAULTCOMPONENT150</td><td>SINGUL~1.XAM|SingularityUpdater.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Languages\en_US\SingularityUpdater.xaml</td><td>1</td><td/></row>
		<row><td>snmp.acl.template</td><td>ISX_DEFAULTCOMPONENT119</td><td>SNMPAC~1.TEM|snmp.acl.template</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\management\snmp.acl.template</td><td>1</td><td/></row>
		<row><td>sound.properties</td><td>ISX_DEFAULTCOMPONENT138</td><td>SOUND~1.PRO|sound.properties</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\sound.properties</td><td>1</td><td/></row>
		<row><td>splash.gif</td><td>ISX_DEFAULTCOMPONENT60</td><td>splash.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\splash.gif</td><td>1</td><td/></row>
		<row><td>splash_11_2x_lic.gif</td><td>ISX_DEFAULTCOMPONENT63</td><td>SPLASH~1.GIF|splash_11@2x-lic.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\splash_11@2x-lic.gif</td><td>1</td><td/></row>
		<row><td>splash_11_lic.gif</td><td>ISX_DEFAULTCOMPONENT62</td><td>SPLASH~1.GIF|splash_11-lic.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\splash_11-lic.gif</td><td>1</td><td/></row>
		<row><td>splash_2x.gif</td><td>ISX_DEFAULTCOMPONENT61</td><td>SPLASH~1.GIF|splash@2x.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\deploy\splash@2x.gif</td><td>1</td><td/></row>
		<row><td>splashscreen.dll</td><td>splashscreen.dll</td><td>SPLASH~1.DLL|splashscreen.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\splashscreen.dll</td><td>1</td><td/></row>
		<row><td>sqlite.interop.dll</td><td>SQLite.Interop.dll</td><td>SQD02B~1.DLL|SQLite.Interop.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\x64\SQLite.Interop.dll</td><td>1</td><td/></row>
		<row><td>sqlite.interop.dll1</td><td>SQLite.Interop.dll1</td><td>SQD02B~1.DLL|SQLite.Interop.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\x86\SQLite.Interop.dll</td><td>1</td><td/></row>
		<row><td>srgb.pf</td><td>ISX_DEFAULTCOMPONENT43</td><td>sRGB.pf</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\cmm\sRGB.pf</td><td>1</td><td/></row>
		<row><td>ssv.dll</td><td>ssv.dll</td><td>ssv.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\ssv.dll</td><td>1</td><td/></row>
		<row><td>ssvagent.exe</td><td>ssvagent.exe</td><td>ssvagent.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\ssvagent.exe</td><td>1</td><td/></row>
		<row><td>sunec.dll</td><td>sunec.dll</td><td>sunec.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\sunec.dll</td><td>1</td><td/></row>
		<row><td>sunec.jar</td><td>ISX_DEFAULTCOMPONENT74</td><td>sunec.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\sunec.jar</td><td>1</td><td/></row>
		<row><td>sunjce_provider.jar</td><td>ISX_DEFAULTCOMPONENT75</td><td>SUNJCE~1.JAR|sunjce_provider.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\sunjce_provider.jar</td><td>1</td><td/></row>
		<row><td>sunmscapi.dll</td><td>sunmscapi.dll</td><td>SUNMSC~1.DLL|sunmscapi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\sunmscapi.dll</td><td>1</td><td/></row>
		<row><td>sunmscapi.jar</td><td>ISX_DEFAULTCOMPONENT76</td><td>SUNMSC~1.JAR|sunmscapi.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\sunmscapi.jar</td><td>1</td><td/></row>
		<row><td>sunpkcs11.jar</td><td>ISX_DEFAULTCOMPONENT77</td><td>SUNPKC~1.JAR|sunpkcs11.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\sunpkcs11.jar</td><td>1</td><td/></row>
		<row><td>system.data.sqlite.dll2</td><td>System.Data.Sqlite.dll2</td><td>SYSTEM~1.DLL|System.Data.Sqlite.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\System.Data.Sqlite.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity2</td><td>System.Windows.Interactivity.dll2</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\System.Windows.Interactivity.dll</td><td>1</td><td/></row>
		<row><td>t2k.dll</td><td>t2k.dll</td><td>t2k.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\t2k.dll</td><td>1</td><td/></row>
		<row><td>thirdpartylicensereadme.txt</td><td>ISX_DEFAULTCOMPONENT145</td><td>THIRDP~1.TXT|THIRDPARTYLICENSEREADME.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\THIRDPARTYLICENSEREADME.txt</td><td>1</td><td/></row>
		<row><td>thirdpartylicensereadme_java</td><td>ISX_DEFAULTCOMPONENT144</td><td>THIRDP~1.TXT|THIRDPARTYLICENSEREADME-JAVAFX.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\THIRDPARTYLICENSEREADME-JAVAFX.txt</td><td>1</td><td/></row>
		<row><td>tnameserv.exe</td><td>tnameserv.exe</td><td>TNAMES~1.EXE|tnameserv.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\tnameserv.exe</td><td>1</td><td/></row>
		<row><td>trusted.libraries</td><td>ISX_DEFAULTCOMPONENT136</td><td>TRUSTE~1.LIB|trusted.libraries</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\trusted.libraries</td><td>1</td><td/></row>
		<row><td>tzdb.dat</td><td>ISX_DEFAULTCOMPONENT139</td><td>tzdb.dat</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\tzdb.dat</td><td>1</td><td/></row>
		<row><td>tzmappings</td><td>ISX_DEFAULTCOMPONENT140</td><td>TZMAPP~1|tzmappings</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\tzmappings</td><td>1</td><td/></row>
		<row><td>unpack.dll</td><td>unpack.dll</td><td>unpack.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\unpack.dll</td><td>1</td><td/></row>
		<row><td>unpack200.exe</td><td>unpack200.exe</td><td>UNPACK~1.EXE|unpack200.exe</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\unpack200.exe</td><td>1</td><td/></row>
		<row><td>us_export_policy.jar</td><td>ISX_DEFAULTCOMPONENT137</td><td>US_EXP~1.JAR|US_export_policy.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\security\US_export_policy.jar</td><td>1</td><td/></row>
		<row><td>usermessage.ini2</td><td>ISX_DEFAULTCOMPONENT199</td><td>USERME~1.INI|UserMessage.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessage.ini</td><td>1</td><td/></row>
		<row><td>usermessagede.ini2</td><td>ISX_DEFAULTCOMPONENT200</td><td>USERME~1.INI|UserMessageDe.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageDe.ini</td><td>1</td><td/></row>
		<row><td>usermessagees.ini2</td><td>ISX_DEFAULTCOMPONENT201</td><td>USERME~1.INI|UserMessageEs.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageEs.ini</td><td>1</td><td/></row>
		<row><td>usermessagefr.ini2</td><td>ISX_DEFAULTCOMPONENT202</td><td>USERME~1.INI|UserMessageFr.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageFr.ini</td><td>1</td><td/></row>
		<row><td>usermessageit.ini2</td><td>ISX_DEFAULTCOMPONENT203</td><td>USERME~1.INI|UserMessageIt.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageIt.ini</td><td>1</td><td/></row>
		<row><td>usermessageja.ini2</td><td>ISX_DEFAULTCOMPONENT204</td><td>USERME~1.INI|UserMessageJa.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageJa.ini</td><td>1</td><td/></row>
		<row><td>usermessageru.ini2</td><td>ISX_DEFAULTCOMPONENT205</td><td>USERME~1.INI|UserMessageRu.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageRu.ini</td><td>1</td><td/></row>
		<row><td>usermessagezh.ini2</td><td>ISX_DEFAULTCOMPONENT206</td><td>USERME~1.INI|UserMessageZh.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMessageZh.ini</td><td>1</td><td/></row>
		<row><td>usermsg.bmp</td><td>ISX_DEFAULTCOMPONENT11</td><td>UserMsg.bmp</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsg.bmp</td><td>1</td><td/></row>
		<row><td>usermsg.bmp3</td><td>ISX_DEFAULTCOMPONENT207</td><td>UserMsg.bmp</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\UserMsg.bmp</td><td>1</td><td/></row>
		<row><td>usermsg.ini</td><td>ISX_DEFAULTCOMPONENT12</td><td>UserMsg.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsg.ini</td><td>1</td><td/></row>
		<row><td>usermsgde.ini</td><td>ISX_DEFAULTCOMPONENT13</td><td>USERMS~1.INI|UserMsgDe.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgDe.ini</td><td>1</td><td/></row>
		<row><td>usermsges.ini</td><td>ISX_DEFAULTCOMPONENT14</td><td>USERMS~1.INI|UserMsgEs.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgEs.ini</td><td>1</td><td/></row>
		<row><td>usermsgja.ini</td><td>ISX_DEFAULTCOMPONENT15</td><td>USERMS~1.INI|UserMsgJa.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgJa.ini</td><td>1</td><td/></row>
		<row><td>usermsgru.ini</td><td>ISX_DEFAULTCOMPONENT16</td><td>USERMS~1.INI|UserMsgRu.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgRu.ini</td><td>1</td><td/></row>
		<row><td>usermsgus.dll</td><td>UserMsgUs.dll</td><td>USERMS~1.DLL|UserMsgUs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgUs.dll</td><td>1</td><td/></row>
		<row><td>usermsgzh.ini</td><td>ISX_DEFAULTCOMPONENT17</td><td>USERMS~1.INI|UserMsgZh.ini</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Entities\UserMsgZh.ini</td><td>1</td><td/></row>
		<row><td>verify.dll</td><td>verify.dll</td><td>verify.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\verify.dll</td><td>1</td><td/></row>
		<row><td>viewerprograms.xml</td><td>ISX_DEFAULTCOMPONENT5</td><td>VIEWER~1.XML|ViewerPrograms.xml</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\Attachments\ViewerPrograms.xml</td><td>1</td><td/></row>
		<row><td>vsacad.dll</td><td>vsacad.dll</td><td>vsacad.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsacad.dll</td><td>1</td><td/></row>
		<row><td>vsacs.dll</td><td>vsacs.dll</td><td>vsacs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsacs.dll</td><td>1</td><td/></row>
		<row><td>vsami.dll</td><td>vsami.dll</td><td>vsami.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsami.dll</td><td>1</td><td/></row>
		<row><td>vsarc.dll</td><td>vsarc.dll</td><td>vsarc.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsarc.dll</td><td>1</td><td/></row>
		<row><td>vsasf.dll</td><td>vsasf.dll</td><td>vsasf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsasf.dll</td><td>1</td><td/></row>
		<row><td>vsbdr.dll</td><td>vsbdr.dll</td><td>vsbdr.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsbdr.dll</td><td>1</td><td/></row>
		<row><td>vsbmp.dll</td><td>vsbmp.dll</td><td>vsbmp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsbmp.dll</td><td>1</td><td/></row>
		<row><td>vscdrx.dll</td><td>vscdrx.dll</td><td>vscdrx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vscdrx.dll</td><td>1</td><td/></row>
		<row><td>vscgm.dll</td><td>vscgm.dll</td><td>vscgm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vscgm.dll</td><td>1</td><td/></row>
		<row><td>vsdbs.dll</td><td>vsdbs.dll</td><td>vsdbs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdbs.dll</td><td>1</td><td/></row>
		<row><td>vsdez.dll</td><td>vsdez.dll</td><td>vsdez.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdez.dll</td><td>1</td><td/></row>
		<row><td>vsdif.dll</td><td>vsdif.dll</td><td>vsdif.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdif.dll</td><td>1</td><td/></row>
		<row><td>vsdrw.dll</td><td>vsdrw.dll</td><td>vsdrw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdrw.dll</td><td>1</td><td/></row>
		<row><td>vsdx.dll</td><td>vsdx.dll</td><td>vsdx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdx.dll</td><td>1</td><td/></row>
		<row><td>vsdxla.dll</td><td>vsdxla.dll</td><td>vsdxla.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdxla.dll</td><td>1</td><td/></row>
		<row><td>vsdxlm.dll</td><td>vsdxlm.dll</td><td>vsdxlm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsdxlm.dll</td><td>1</td><td/></row>
		<row><td>vsemf.dll</td><td>vsemf.dll</td><td>vsemf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsemf.dll</td><td>1</td><td/></row>
		<row><td>vsen4.dll</td><td>vsen4.dll</td><td>vsen4.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsen4.dll</td><td>1</td><td/></row>
		<row><td>vsens.dll</td><td>vsens.dll</td><td>vsens.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsens.dll</td><td>1</td><td/></row>
		<row><td>vsenw.dll</td><td>vsenw.dll</td><td>vsenw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsenw.dll</td><td>1</td><td/></row>
		<row><td>vseshr.dll</td><td>vseshr.dll</td><td>vseshr.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vseshr.dll</td><td>1</td><td/></row>
		<row><td>vsexe2.dll</td><td>vsexe2.dll</td><td>vsexe2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsexe2.dll</td><td>1</td><td/></row>
		<row><td>vsfax.dll</td><td>vsfax.dll</td><td>vsfax.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsfax.dll</td><td>1</td><td/></row>
		<row><td>vsfcd.dll</td><td>vsfcd.dll</td><td>vsfcd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsfcd.dll</td><td>1</td><td/></row>
		<row><td>vsfcs.dll</td><td>vsfcs.dll</td><td>vsfcs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsfcs.dll</td><td>1</td><td/></row>
		<row><td>vsfft.dll</td><td>vsfft.dll</td><td>vsfft.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsfft.dll</td><td>1</td><td/></row>
		<row><td>vsflw.dll</td><td>vsflw.dll</td><td>vsflw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsflw.dll</td><td>1</td><td/></row>
		<row><td>vsfwk.dll</td><td>vsfwk.dll</td><td>vsfwk.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsfwk.dll</td><td>1</td><td/></row>
		<row><td>vsgdsf.dll</td><td>vsgdsf.dll</td><td>vsgdsf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsgdsf.dll</td><td>1</td><td/></row>
		<row><td>vsgif.dll</td><td>vsgif.dll</td><td>vsgif.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsgif.dll</td><td>1</td><td/></row>
		<row><td>vsgzip.dll</td><td>vsgzip.dll</td><td>vsgzip.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsgzip.dll</td><td>1</td><td/></row>
		<row><td>vshgs.dll</td><td>vshgs.dll</td><td>vshgs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vshgs.dll</td><td>1</td><td/></row>
		<row><td>vshtml.dll</td><td>vshtml.dll</td><td>vshtml.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vshtml.dll</td><td>1</td><td/></row>
		<row><td>vshwp.dll</td><td>vshwp.dll</td><td>vshwp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vshwp.dll</td><td>1</td><td/></row>
		<row><td>vshwp2.dll</td><td>vshwp2.dll</td><td>vshwp2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vshwp2.dll</td><td>1</td><td/></row>
		<row><td>vsich.dll</td><td>vsich.dll</td><td>vsich.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsich.dll</td><td>1</td><td/></row>
		<row><td>vsich6.dll</td><td>vsich6.dll</td><td>vsich6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsich6.dll</td><td>1</td><td/></row>
		<row><td>vsid3.dll</td><td>vsid3.dll</td><td>vsid3.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsid3.dll</td><td>1</td><td/></row>
		<row><td>vsimg.dll</td><td>vsimg.dll</td><td>vsimg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsimg.dll</td><td>1</td><td/></row>
		<row><td>vsiwok.dll</td><td>vsiwok.dll</td><td>vsiwok.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsiwok.dll</td><td>1</td><td/></row>
		<row><td>vsiwok13.dll</td><td>vsiwok13.dll</td><td>vsiwok13.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsiwok13.dll</td><td>1</td><td/></row>
		<row><td>vsiwon.dll</td><td>vsiwon.dll</td><td>vsiwon.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsiwon.dll</td><td>1</td><td/></row>
		<row><td>vsiwop.dll</td><td>vsiwop.dll</td><td>vsiwop.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsiwop.dll</td><td>1</td><td/></row>
		<row><td>vsiwp.dll</td><td>vsiwp.dll</td><td>vsiwp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsiwp.dll</td><td>1</td><td/></row>
		<row><td>vsjbg2.dll</td><td>vsjbg2.dll</td><td>vsjbg2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsjbg2.dll</td><td>1</td><td/></row>
		<row><td>vsjp2.dll</td><td>vsjp2.dll</td><td>vsjp2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsjp2.dll</td><td>1</td><td/></row>
		<row><td>vsjw.dll</td><td>vsjw.dll</td><td>vsjw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsjw.dll</td><td>1</td><td/></row>
		<row><td>vsleg.dll</td><td>vsleg.dll</td><td>vsleg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsleg.dll</td><td>1</td><td/></row>
		<row><td>vslwp7.dll</td><td>vslwp7.dll</td><td>vslwp7.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vslwp7.dll</td><td>1</td><td/></row>
		<row><td>vslzh.dll</td><td>vslzh.dll</td><td>vslzh.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vslzh.dll</td><td>1</td><td/></row>
		<row><td>vsm11.dll</td><td>vsm11.dll</td><td>vsm11.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsm11.dll</td><td>1</td><td/></row>
		<row><td>vsmanu.dll</td><td>vsmanu.dll</td><td>vsmanu.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmanu.dll</td><td>1</td><td/></row>
		<row><td>vsmbox.dll</td><td>vsmbox.dll</td><td>vsmbox.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmbox.dll</td><td>1</td><td/></row>
		<row><td>vsmcw.dll</td><td>vsmcw.dll</td><td>vsmcw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmcw.dll</td><td>1</td><td/></row>
		<row><td>vsmdb.dll</td><td>vsmdb.dll</td><td>vsmdb.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmdb.dll</td><td>1</td><td/></row>
		<row><td>vsmif.dll</td><td>vsmif.dll</td><td>vsmif.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmif.dll</td><td>1</td><td/></row>
		<row><td>vsmime.dll</td><td>vsmime.dll</td><td>vsmime.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmime.dll</td><td>1</td><td/></row>
		<row><td>vsmm.dll</td><td>vsmm.dll</td><td>vsmm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmm.dll</td><td>1</td><td/></row>
		<row><td>vsmm4.dll</td><td>vsmm4.dll</td><td>vsmm4.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmm4.dll</td><td>1</td><td/></row>
		<row><td>vsmmfn.dll</td><td>vsmmfn.dll</td><td>vsmmfn.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmmfn.dll</td><td>1</td><td/></row>
		<row><td>vsmp.dll</td><td>vsmp.dll</td><td>vsmp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmp.dll</td><td>1</td><td/></row>
		<row><td>vsmpp.dll</td><td>vsmpp.dll</td><td>vsmpp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmpp.dll</td><td>1</td><td/></row>
		<row><td>vsmsg.dll</td><td>vsmsg.dll</td><td>vsmsg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmsg.dll</td><td>1</td><td/></row>
		<row><td>vsmsw.dll</td><td>vsmsw.dll</td><td>vsmsw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmsw.dll</td><td>1</td><td/></row>
		<row><td>vsmwkd.dll</td><td>vsmwkd.dll</td><td>vsmwkd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmwkd.dll</td><td>1</td><td/></row>
		<row><td>vsmwks.dll</td><td>vsmwks.dll</td><td>vsmwks.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmwks.dll</td><td>1</td><td/></row>
		<row><td>vsmwp2.dll</td><td>vsmwp2.dll</td><td>vsmwp2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmwp2.dll</td><td>1</td><td/></row>
		<row><td>vsmwpf.dll</td><td>vsmwpf.dll</td><td>vsmwpf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmwpf.dll</td><td>1</td><td/></row>
		<row><td>vsmwrk.dll</td><td>vsmwrk.dll</td><td>vsmwrk.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsmwrk.dll</td><td>1</td><td/></row>
		<row><td>vsnsf.dll</td><td>vsnsf.dll</td><td>vsnsf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsnsf.dll</td><td>1</td><td/></row>
		<row><td>vsolm.dll</td><td>vsolm.dll</td><td>vsolm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsolm.dll</td><td>1</td><td/></row>
		<row><td>vsone.dll</td><td>vsone.dll</td><td>vsone.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsone.dll</td><td>1</td><td/></row>
		<row><td>vsow.dll</td><td>vsow.dll</td><td>vsow.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsow.dll</td><td>1</td><td/></row>
		<row><td>vspbm.dll</td><td>vspbm.dll</td><td>vspbm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspbm.dll</td><td>1</td><td/></row>
		<row><td>vspcl.dll</td><td>vspcl.dll</td><td>vspcl.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspcl.dll</td><td>1</td><td/></row>
		<row><td>vspcx.dll</td><td>vspcx.dll</td><td>vspcx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspcx.dll</td><td>1</td><td/></row>
		<row><td>vspdf.dll</td><td>vspdf.dll</td><td>vspdf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspdf.dll</td><td>1</td><td/></row>
		<row><td>vspdfi.dll</td><td>vspdfi.dll</td><td>vspdfi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspdfi.dll</td><td>1</td><td/></row>
		<row><td>vspdx.dll</td><td>vspdx.dll</td><td>vspdx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspdx.dll</td><td>1</td><td/></row>
		<row><td>vspfs.dll</td><td>vspfs.dll</td><td>vspfs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspfs.dll</td><td>1</td><td/></row>
		<row><td>vspgl.dll</td><td>vspgl.dll</td><td>vspgl.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspgl.dll</td><td>1</td><td/></row>
		<row><td>vspic.dll</td><td>vspic.dll</td><td>vspic.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspic.dll</td><td>1</td><td/></row>
		<row><td>vspict.dll</td><td>vspict.dll</td><td>vspict.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspict.dll</td><td>1</td><td/></row>
		<row><td>vspng.dll</td><td>vspng.dll</td><td>vspng.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspng.dll</td><td>1</td><td/></row>
		<row><td>vspntg.dll</td><td>vspntg.dll</td><td>vspntg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspntg.dll</td><td>1</td><td/></row>
		<row><td>vspp12.dll</td><td>vspp12.dll</td><td>vspp12.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspp12.dll</td><td>1</td><td/></row>
		<row><td>vspp2.dll</td><td>vspp2.dll</td><td>vspp2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspp2.dll</td><td>1</td><td/></row>
		<row><td>vspp7.dll</td><td>vspp7.dll</td><td>vspp7.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspp7.dll</td><td>1</td><td/></row>
		<row><td>vspp97.dll</td><td>vspp97.dll</td><td>vspp97.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspp97.dll</td><td>1</td><td/></row>
		<row><td>vsppl.dll</td><td>vsppl.dll</td><td>vsppl.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsppl.dll</td><td>1</td><td/></row>
		<row><td>vspsp6.dll</td><td>vspsp6.dll</td><td>vspsp6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspsp6.dll</td><td>1</td><td/></row>
		<row><td>vspst.dll</td><td>vspst.dll</td><td>vspst.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspst.dll</td><td>1</td><td/></row>
		<row><td>vspstf.dll</td><td>vspstf.dll</td><td>vspstf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vspstf.dll</td><td>1</td><td/></row>
		<row><td>vsqa.dll</td><td>vsqa.dll</td><td>vsqa.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsqa.dll</td><td>1</td><td/></row>
		<row><td>vsqad.dll</td><td>vsqad.dll</td><td>vsqad.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsqad.dll</td><td>1</td><td/></row>
		<row><td>vsqp6.dll</td><td>vsqp6.dll</td><td>vsqp6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsqp6.dll</td><td>1</td><td/></row>
		<row><td>vsqp9.dll</td><td>vsqp9.dll</td><td>vsqp9.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsqp9.dll</td><td>1</td><td/></row>
		<row><td>vsqt.dll</td><td>vsqt.dll</td><td>vsqt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsqt.dll</td><td>1</td><td/></row>
		<row><td>vsrar.dll</td><td>vsrar.dll</td><td>vsrar.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsrar.dll</td><td>1</td><td/></row>
		<row><td>vsras.dll</td><td>vsras.dll</td><td>vsras.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsras.dll</td><td>1</td><td/></row>
		<row><td>vsrbs.dll</td><td>vsrbs.dll</td><td>vsrbs.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsrbs.dll</td><td>1</td><td/></row>
		<row><td>vsrft.dll</td><td>vsrft.dll</td><td>vsrft.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsrft.dll</td><td>1</td><td/></row>
		<row><td>vsrfx.dll</td><td>vsrfx.dll</td><td>vsrfx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsrfx.dll</td><td>1</td><td/></row>
		<row><td>vsriff.dll</td><td>vsriff.dll</td><td>vsriff.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsriff.dll</td><td>1</td><td/></row>
		<row><td>vsrtf.dll</td><td>vsrtf.dll</td><td>vsrtf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsrtf.dll</td><td>1</td><td/></row>
		<row><td>vssam.dll</td><td>vssam.dll</td><td>vssam.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssam.dll</td><td>1</td><td/></row>
		<row><td>vssc5.dll</td><td>vssc5.dll</td><td>vssc5.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssc5.dll</td><td>1</td><td/></row>
		<row><td>vssdw.dll</td><td>vssdw.dll</td><td>vssdw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssdw.dll</td><td>1</td><td/></row>
		<row><td>vsshw3.dll</td><td>vsshw3.dll</td><td>vsshw3.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsshw3.dll</td><td>1</td><td/></row>
		<row><td>vssmd.dll</td><td>vssmd.dll</td><td>vssmd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssmd.dll</td><td>1</td><td/></row>
		<row><td>vssms.dll</td><td>vssms.dll</td><td>vssms.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssms.dll</td><td>1</td><td/></row>
		<row><td>vssmt.dll</td><td>vssmt.dll</td><td>vssmt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssmt.dll</td><td>1</td><td/></row>
		<row><td>vssnap.dll</td><td>vssnap.dll</td><td>vssnap.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssnap.dll</td><td>1</td><td/></row>
		<row><td>vsso6.dll</td><td>vsso6.dll</td><td>vsso6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsso6.dll</td><td>1</td><td/></row>
		<row><td>vssoc.dll</td><td>vssoc.dll</td><td>vssoc.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssoc.dll</td><td>1</td><td/></row>
		<row><td>vssoc6.dll</td><td>vssoc6.dll</td><td>vssoc6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssoc6.dll</td><td>1</td><td/></row>
		<row><td>vssoi.dll</td><td>vssoi.dll</td><td>vssoi.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssoi.dll</td><td>1</td><td/></row>
		<row><td>vssoi6.dll</td><td>vssoi6.dll</td><td>vssoi6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssoi6.dll</td><td>1</td><td/></row>
		<row><td>vssow.dll</td><td>vssow.dll</td><td>vssow.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vssow.dll</td><td>1</td><td/></row>
		<row><td>vsspt.dll</td><td>vsspt.dll</td><td>vsspt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsspt.dll</td><td>1</td><td/></row>
		<row><td>vsssml.dll</td><td>vsssml.dll</td><td>vsssml.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsssml.dll</td><td>1</td><td/></row>
		<row><td>vsswf.dll</td><td>vsswf.dll</td><td>vsswf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsswf.dll</td><td>1</td><td/></row>
		<row><td>vstaz.dll</td><td>vstaz.dll</td><td>vstaz.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstaz.dll</td><td>1</td><td/></row>
		<row><td>vstext.dll</td><td>vstext.dll</td><td>vstext.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstext.dll</td><td>1</td><td/></row>
		<row><td>vstga.dll</td><td>vstga.dll</td><td>vstga.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstga.dll</td><td>1</td><td/></row>
		<row><td>vstif6.dll</td><td>vstif6.dll</td><td>vstif6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstif6.dll</td><td>1</td><td/></row>
		<row><td>vstw.dll</td><td>vstw.dll</td><td>vstw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstw.dll</td><td>1</td><td/></row>
		<row><td>vstxt.dll</td><td>vstxt.dll</td><td>vstxt.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vstxt.dll</td><td>1</td><td/></row>
		<row><td>vsvcrd.dll</td><td>vsvcrd.dll</td><td>vsvcrd.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsvcrd.dll</td><td>1</td><td/></row>
		<row><td>vsviso.dll</td><td>vsviso.dll</td><td>vsviso.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsviso.dll</td><td>1</td><td/></row>
		<row><td>vsvsdx.dll</td><td>vsvsdx.dll</td><td>vsvsdx.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsvsdx.dll</td><td>1</td><td/></row>
		<row><td>vsvw3.dll</td><td>vsvw3.dll</td><td>vsvw3.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsvw3.dll</td><td>1</td><td/></row>
		<row><td>vsw12.dll</td><td>vsw12.dll</td><td>vsw12.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsw12.dll</td><td>1</td><td/></row>
		<row><td>vsw6.dll</td><td>vsw6.dll</td><td>vsw6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsw6.dll</td><td>1</td><td/></row>
		<row><td>vsw97.dll</td><td>vsw97.dll</td><td>vsw97.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsw97.dll</td><td>1</td><td/></row>
		<row><td>vswbmp.dll</td><td>vswbmp.dll</td><td>vswbmp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswbmp.dll</td><td>1</td><td/></row>
		<row><td>vswg2.dll</td><td>vswg2.dll</td><td>vswg2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswg2.dll</td><td>1</td><td/></row>
		<row><td>vswk4.dll</td><td>vswk4.dll</td><td>vswk4.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswk4.dll</td><td>1</td><td/></row>
		<row><td>vswk6.dll</td><td>vswk6.dll</td><td>vswk6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswk6.dll</td><td>1</td><td/></row>
		<row><td>vswks.dll</td><td>vswks.dll</td><td>vswks.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswks.dll</td><td>1</td><td/></row>
		<row><td>vswm.dll</td><td>vswm.dll</td><td>vswm.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswm.dll</td><td>1</td><td/></row>
		<row><td>vswmf.dll</td><td>vswmf.dll</td><td>vswmf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswmf.dll</td><td>1</td><td/></row>
		<row><td>vswml.dll</td><td>vswml.dll</td><td>vswml.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswml.dll</td><td>1</td><td/></row>
		<row><td>vsword.dll</td><td>vsword.dll</td><td>vsword.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsword.dll</td><td>1</td><td/></row>
		<row><td>vswork.dll</td><td>vswork.dll</td><td>vswork.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswork.dll</td><td>1</td><td/></row>
		<row><td>vswp5.dll</td><td>vswp5.dll</td><td>vswp5.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswp5.dll</td><td>1</td><td/></row>
		<row><td>vswp6.dll</td><td>vswp6.dll</td><td>vswp6.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswp6.dll</td><td>1</td><td/></row>
		<row><td>vswpf.dll</td><td>vswpf.dll</td><td>vswpf.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpf.dll</td><td>1</td><td/></row>
		<row><td>vswpg.dll</td><td>vswpg.dll</td><td>vswpg.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpg.dll</td><td>1</td><td/></row>
		<row><td>vswpg2.dll</td><td>vswpg2.dll</td><td>vswpg2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpg2.dll</td><td>1</td><td/></row>
		<row><td>vswpl.dll</td><td>vswpl.dll</td><td>vswpl.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpl.dll</td><td>1</td><td/></row>
		<row><td>vswpml.dll</td><td>vswpml.dll</td><td>vswpml.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpml.dll</td><td>1</td><td/></row>
		<row><td>vswpw.dll</td><td>vswpw.dll</td><td>vswpw.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vswpw.dll</td><td>1</td><td/></row>
		<row><td>vsws.dll</td><td>vsws.dll</td><td>vsws.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsws.dll</td><td>1</td><td/></row>
		<row><td>vsws2.dll</td><td>vsws2.dll</td><td>vsws2.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsws2.dll</td><td>1</td><td/></row>
		<row><td>vsxl12.dll</td><td>vsxl12.dll</td><td>vsxl12.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxl12.dll</td><td>1</td><td/></row>
		<row><td>vsxl5.dll</td><td>vsxl5.dll</td><td>vsxl5.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxl5.dll</td><td>1</td><td/></row>
		<row><td>vsxlsb.dll</td><td>vsxlsb.dll</td><td>vsxlsb.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxlsb.dll</td><td>1</td><td/></row>
		<row><td>vsxml.dll</td><td>vsxml.dll</td><td>vsxml.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxml.dll</td><td>1</td><td/></row>
		<row><td>vsxps.dll</td><td>vsxps.dll</td><td>vsxps.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxps.dll</td><td>1</td><td/></row>
		<row><td>vsxy.dll</td><td>vsxy.dll</td><td>vsxy.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsxy.dll</td><td>1</td><td/></row>
		<row><td>vsyim.dll</td><td>vsyim.dll</td><td>vsyim.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vsyim.dll</td><td>1</td><td/></row>
		<row><td>vszip.dll</td><td>vszip.dll</td><td>vszip.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\vszip.dll</td><td>1</td><td/></row>
		<row><td>w2k_lsa_auth.dll</td><td>w2k_lsa_auth.dll</td><td>W2K_LS~1.DLL|w2k_lsa_auth.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\w2k_lsa_auth.dll</td><td>1</td><td/></row>
		<row><td>welcome.html</td><td>ISX_DEFAULTCOMPONENT146</td><td>WELCOM~1.HTM|Welcome.html</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\Welcome.html</td><td>1</td><td/></row>
		<row><td>win32_copydrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT98</td><td>WIN32_~1.GIF|win32_CopyDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_CopyDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>win32_copynodrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT99</td><td>WIN32_~1.GIF|win32_CopyNoDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_CopyNoDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>win32_linkdrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT100</td><td>WIN32_~1.GIF|win32_LinkDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_LinkDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>win32_linknodrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT101</td><td>WIN32_~1.GIF|win32_LinkNoDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_LinkNoDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>win32_movedrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT102</td><td>WIN32_~1.GIF|win32_MoveDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_MoveDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>win32_movenodrop32x32.gif</td><td>ISX_DEFAULTCOMPONENT103</td><td>WIN32_~1.GIF|win32_MoveNoDrop32x32.gif</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\images\cursors\win32_MoveNoDrop32x32.gif</td><td>1</td><td/></row>
		<row><td>windowsaccessbridge_32.dll</td><td>WindowsAccessBridge_32.dll</td><td>WINDOW~1.DLL|WindowsAccessBridge-32.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\WindowsAccessBridge-32.dll</td><td>1</td><td/></row>
		<row><td>wsdetect.dll</td><td>wsdetect.dll</td><td>wsdetect.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\wsdetect.dll</td><td>1</td><td/></row>
		<row><td>wvcore.dll</td><td>wvcore.dll</td><td>wvcore.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\OutSideIn\wvcore.dll</td><td>1</td><td/></row>
		<row><td>xusage.txt</td><td>ISX_DEFAULTCOMPONENT22</td><td>Xusage.txt</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\client\Xusage.txt</td><td>1</td><td/></row>
		<row><td>zip.dll</td><td>zip.dll</td><td>zip.dll</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\bin\zip.dll</td><td>1</td><td/></row>
		<row><td>zipfs.jar</td><td>ISX_DEFAULTCOMPONENT78</td><td>zipfs.jar</td><td>0</td><td/><td/><td/><td>1</td><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\jre\lib\ext\zipfs.jar</td><td>1</td><td/></row>
	</table>

	<table name="FileSFPCatalog">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s255">SFPCatalog_</col>
	</table>

	<table name="Font">
		<col key="yes" def="s72">File_</col>
		<col def="S128">FontTitle</col>
		<row><td>lucidabrightdemibold.ttf</td><td/></row>
		<row><td>lucidabrightdemiitalic.ttf</td><td/></row>
		<row><td>lucidabrightitalic.ttf</td><td/></row>
		<row><td>lucidabrightregular.ttf</td><td/></row>
		<row><td>lucidasansdemibold.ttf</td><td/></row>
		<row><td>lucidasansregular.ttf</td><td/></row>
		<row><td>lucidatypewriterbold.ttf</td><td/></row>
		<row><td>lucidatypewriterregular.ttf</td><td/></row>
	</table>

	<table name="ISAssistantTag">
		<col key="yes" def="s72">Tag</col>
		<col def="S255">Data</col>
		<row><td>PROJECT_ASSISTANT_DEFAULT_FEATURE</td><td>AlwaysInstall</td></row>
	</table>

	<table name="ISBillBoard">
		<col key="yes" def="s72">ISBillboard</col>
		<col def="i2">Duration</col>
		<col def="i2">Origin</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Effect</col>
		<col def="i2">Sequence</col>
		<col def="i2">Target</col>
		<col def="S72">Color</col>
		<col def="S72">Style</col>
		<col def="S72">Font</col>
		<col def="L72">Title</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISChainPackage">
		<col key="yes" def="s72">Package</col>
		<col def="S255">SourcePath</col>
		<col def="S72">ProductCode</col>
		<col def="i2">Order</col>
		<col def="i4">Options</col>
		<col def="S255">InstallCondition</col>
		<col def="S255">RemoveCondition</col>
		<col def="S0">InstallProperties</col>
		<col def="S0">RemoveProperties</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISChainPackageData">
		<col key="yes" def="s72">Package_</col>
		<col key="yes" def="s72">File</col>
		<col def="s50">FilePath</col>
		<col def="I4">Options</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISClrWrap">
		<col key="yes" def="s72">Action_</col>
		<col key="yes" def="s72">Name</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISComCatalogAttribute">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col key="yes" def="s255">ItemName</col>
		<col def="S0">ItemValue</col>
	</table>

	<table name="ISComCatalogCollection">
		<col key="yes" def="s72">ISComCatalogCollection</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s255">CollectionName</col>
	</table>

	<table name="ISComCatalogCollectionObjects">
		<col key="yes" def="s72">ISComCatalogCollection_</col>
		<col key="yes" def="s72">ISComCatalogObject_</col>
	</table>

	<table name="ISComCatalogObject">
		<col key="yes" def="s72">ISComCatalogObject</col>
		<col def="s255">DisplayName</col>
	</table>

	<table name="ISComPlusApplication">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col def="S255">ComputerName</col>
		<col def="s72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusApplicationDLL">
		<col key="yes" def="s72">ISComPlusApplicationDLL</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s0">CLSID</col>
		<col def="S0">ProgId</col>
		<col def="S0">DLL</col>
		<col def="S0">AlterDLL</col>
	</table>

	<table name="ISComPlusProxy">
		<col key="yes" def="s72">ISComPlusProxy</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="S72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusProxyDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusProxyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComPlusServerDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusServerFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComponentExtended">
		<col key="yes" def="s72">Component_</col>
		<col def="I4">OS</col>
		<col def="S0">Language</col>
		<col def="s72">FilterProperty</col>
		<col def="I4">Platforms</col>
		<col def="S0">FTPLocation</col>
		<col def="S0">HTTPLocation</col>
		<col def="S0">Miscellaneous</col>
		<row><td>AdbWinApi.dll</td><td/><td/><td>_FA34CB27_A834_44FE_AFED_D22A6A932E75_FILTER</td><td/><td/><td/><td/></row>
		<row><td>AdbWinUsbApi.dll</td><td/><td/><td>_7ABA9E17_462B_482F_BF69_FAEA9D5E25E8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>BouncyCastle.Crypto.dll2</td><td/><td/><td>_28C6AFA6_5749_495E_AF3F_3DBB4002D38E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.PInvoke.dll</td><td/><td/><td>_79EDD4D6_239B_40AC_8997_F0726058B9A7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Parse.Android.dll</td><td/><td/><td>_02844604_AC98_4931_A13E_BFBECC548586_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Parse.Local.dll</td><td/><td/><td>_7C072B02_40D0_4F93_AF53_4BEE73FE4370_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Parse.Signature.dll</td><td/><td/><td>_C1460AA6_5C0F_4E26_8CE2_E07F24640E1A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Parse.dll</td><td/><td/><td>_2B770FC1_B8CD_43A2_BF12_F4D028F214D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Previewers.Contracts.dll</td><td/><td/><td>_D256DBD3_FFDE_49E7_90F0_50E9284518B0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFC.Singularity.Forensics.dll</td><td/><td/><td>_02B23C42_EDFE_4501_B6FB_171F5EB9C2DC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCControls.dll</td><td/><td/><td>_82925152_0960_483E_A068_D92569100567_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCCultures.dll</td><td/><td/><td>_41655C4F_0904_4D93_BEC0_7CD42A3C5760_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCHexaEditor.dll</td><td/><td/><td>_519FF174_8855_41A7_886D_7B76D3F83907_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCMessageBoxes.dll</td><td/><td/><td>_3F56E3F6_FF19_4ECE_89EB_D088B3275DB3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCUIContracts.dll1</td><td/><td/><td>_9E56B8CF_9336_42B2_97A7_5CFF727C971B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>CDFCUIContracts.dll3</td><td/><td/><td>_C73AA4EE_C6CA_4D04_B3D1_C5CBB60384B9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Cflab.DataTransport.dll2</td><td/><td/><td>_91EB0825_5792_41BE_A592_178DD603CEC5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>DirectOutIn.dll2</td><td/><td/><td>_04522819_E1E5_434D_92E9_DADC6CB57ACC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>EventLogger.dll</td><td/><td/><td>_24C1F9B6_0327_4BE2_A3E2_75F739392CFD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td/><td/><td>_54749AAB_7BCA_4105_8F77_DF85EE02C369_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td/><td/><td>_914C2CEE_2F7A_44B1_B8CE_0C3B6484F216_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT100</td><td/><td/><td>_F3DBBCBD_F048_4C25_A213_BCBB986714A7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT101</td><td/><td/><td>_FDE8DF18_889A_4655_AF18_BC18729826B9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT102</td><td/><td/><td>_C85CAF55_72E4_4AD9_9CF5_D6FCAAA0AF62_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT103</td><td/><td/><td>_6A8AFDBF_C68E_44D2_85B7_1784D35F449C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT104</td><td/><td/><td>_7DE5EC27_F52B_45C8_9F3D_0867D572AD30_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT105</td><td/><td/><td>_385846D3_EB8B_4B12_A126_9849123CE26D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT106</td><td/><td/><td>_840B260E_81C8_4D64_A8BB_80D35AE1322B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT107</td><td/><td/><td>_6544FC49_8292_4C4F_9D3A_09D2DA013897_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT108</td><td/><td/><td>_7D61AF5E_6731_4D67_8DAA_1B66A6E885CF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT109</td><td/><td/><td>_6FD32B36_790D_473D_8567_5F6FEE2B3AFC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td/><td/><td>_66EB6099_DB87_4F67_9DBD_E447A93B41CF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT110</td><td/><td/><td>_A9852A15_1167_4C66_B6FC_2BAA82673822_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT111</td><td/><td/><td>_9B5AF2AD_637C_4881_8FE2_E4E2B52898B5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT112</td><td/><td/><td>_37B63E0E_11F5_4457_B148_78D616560DD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT113</td><td/><td/><td>_A81242F3_FFF7_4B86_B30A_71EC0E385728_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT114</td><td/><td/><td>_5A2BD0D3_4D17_4FA0_B12F_091173C21AF0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT115</td><td/><td/><td>_938D655F_EDD4_431D_A0B0_C9FE4589E167_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT116</td><td/><td/><td>_2B3E72CE_F544_4B03_AC43_8F42F22FD9E2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT117</td><td/><td/><td>_3844CB70_DDEE_4EEB_A620_06745A95F5A7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT118</td><td/><td/><td>_E2744383_68D4_4743_B459_6FAD25B532E9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT119</td><td/><td/><td>_D114E5C1_F14B_4348_BF4B_940F32DA8197_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT12</td><td/><td/><td>_D764F8E9_DF70_453F_95C7_3178485D234F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT120</td><td/><td/><td>_56F7A3F8_DBDA_4BC0_8B2B_B3ADE0C1F0A8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT121</td><td/><td/><td>_3C0D9F49_76C0_4FDA_A13B_F56D442C5F64_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT122</td><td/><td/><td>_9F027B43_3366_4D32_B996_CFFD87CD9B03_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT123</td><td/><td/><td>_E82A2AD4_9A6B_4B7C_9B1A_03BB19C7271A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT124</td><td/><td/><td>_3C1C271F_B8DB_47E0_8609_2B9F09656146_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT125</td><td/><td/><td>_EA7B0A99_32E9_42BC_B7ED_C9684EBDFBC2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT126</td><td/><td/><td>_5E42D6A3_EFCB_49F4_9EF1_7891A56AE03B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT127</td><td/><td/><td>_F3CF5EB0_0C4E_489D_9A18_9D5FE9043D3E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT128</td><td/><td/><td>_F66FFEB0_BCA5_43FC_A25F_BFD66DC9F72A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT129</td><td/><td/><td>_46196C58_C01F_46A5_A3C1_F43FCA4CE633_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td/><td/><td>_E1C5A527_76B3_4D18_98B0_25D446DC99FF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT130</td><td/><td/><td>_9E9953F8_B84A_44D8_83BD_8A8A82186A41_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT131</td><td/><td/><td>_239C104D_73B7_46D0_8950_4C4F4A03780E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT132</td><td/><td/><td>_4B09952A_885E_4180_B6A1_5049CA9B3B10_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT133</td><td/><td/><td>_5BE11737_7DE6_40D8_BA83_5613AE240083_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT134</td><td/><td/><td>_EEB52460_549A_4170_9EF4_4199C56D2E37_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT135</td><td/><td/><td>_ED3E39BA_8E96_477C_80D0_58B6FEA74B82_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT136</td><td/><td/><td>_C60C434C_3705_42B4_B142_1BD62142DF02_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT137</td><td/><td/><td>_D4FA2DA9_D37F_43A7_BB15_B2EB4297723B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT138</td><td/><td/><td>_0DAE8F33_21D3_4962_8EEE_BA1F7AA7F5AD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT139</td><td/><td/><td>_368F6AC7_E1C8_4640_9B6B_38330E3C2E25_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td/><td/><td>_71461525_DCDC_4E3D_BED3_CDE3B8A0E943_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT140</td><td/><td/><td>_5689AFD1_B197_44BD_AC81_B3401660D0CC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT141</td><td/><td/><td>_8890C145_FE7A_4211_B58C_E5F3578A69AE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT142</td><td/><td/><td>_8111AFC4_332E_4A20_B6E3_34AC7E82BB0B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT143</td><td/><td/><td>_F288364F_D297_4E71_973D_BA77928BE224_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT144</td><td/><td/><td>_5312085A_EAF2_44E7_95F4_CE6623E1E146_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT145</td><td/><td/><td>_15E05D9E_1EF4_4B13_BF96_2354595D340B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT146</td><td/><td/><td>_8BC1797E_A9FD_44A8_AA4E_521CFC266DBD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT147</td><td/><td/><td>_ACD898A9_4FC5_44B4_BD14_83B6041E5114_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT148</td><td/><td/><td>_0D5BBA85_489F_4834_B8D3_9402B779D2C8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT149</td><td/><td/><td>_10451938_2CE0_41B5_B3AA_9F475B89A320_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td/><td/><td>_15569623_7489_4F9E_8661_4CEEC3C71DEE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT150</td><td/><td/><td>_838CF9C0_9B9D_481E_8070_08AC36AF6D88_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT155</td><td/><td/><td>_6A41E597_E693_4A54_A27C_52BE9F50C3BC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT156</td><td/><td/><td>_0A98BC28_4F61_4148_83B5_8B496C87C2EB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT157</td><td/><td/><td>_68CF92AF_98F9_40A6_9E0E_531C6CD494EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT158</td><td/><td/><td>_B78D8778_DA78_4CE8_9642_1A95ACF4E1B0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT159</td><td/><td/><td>_C4204C77_02E1_4C75_B768_C5ADC6EB3BAE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td/><td/><td>_A75417CB_3263_413A_83EA_93B722F259D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT163</td><td/><td/><td>_AF79540A_2BF2_4472_97EB_38B503FB8B44_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT169</td><td/><td/><td>_7F4297C9_35EC_4EFB_AF75_FABD2C0FC3A5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td/><td/><td>_C0C2130A_193C_46F2_B974_30B4C35547CB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT170</td><td/><td/><td>_2335F2E4_E91D_4106_8B4B_6A5E9FE73424_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT171</td><td/><td/><td>_CDA361E3_00E0_4C00_90D4_DF12FFC344C3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT172</td><td/><td/><td>_585CCCAA_DC98_4058_9683_8692CE22F082_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT173</td><td/><td/><td>_89FA3A14_C2EF_4DE9_9DF5_6E975B61F50C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td/><td/><td>_DFB33A34_305D_4B7F_A97D_3A3B18BF8599_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT183</td><td/><td/><td>_8EBDB09C_FF62_4DD8_A900_2218E9A04699_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT184</td><td/><td/><td>_80DB70E8_66CC_46E3_8069_E3ABC8B2CEFE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT185</td><td/><td/><td>_52C9D457_FD69_426A_B38B_920DB30C3EE6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT186</td><td/><td/><td>_8573A67C_EC01_4F95_ACD4_3D683B031FD0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT187</td><td/><td/><td>_C53CD663_A109_42A4_98FA_F14E28674144_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT188</td><td/><td/><td>_E85E2A77_503C_48FB_8241_2B968AA64381_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT189</td><td/><td/><td>_3A3B186F_F272_42D1_90B5_F337CCD11360_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td/><td/><td>_06912E6A_B6C5_441F_9C36_5831AD6271FF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT190</td><td/><td/><td>_D5A3D870_7BE2_4C60_A0D2_850E6EF8FF1E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT191</td><td/><td/><td>_17F7CA19_C20B_4CB2_B1A2_7642853B6E68_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT192</td><td/><td/><td>_CDB71054_D71F_472D_BE63_B5427F2C3099_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT193</td><td/><td/><td>_0FE923E0_2D62_46BE_B2DB_C6E47C9F328F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT194</td><td/><td/><td>_38B95C9A_B9CB_40E0_B579_FE2834D1BF83_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT195</td><td/><td/><td>_4FFF6D86_BC88_4D9C_B6CD_A6ABE6F09353_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT196</td><td/><td/><td>_183E6908_D7CE_47B4_986B_9A8993821028_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT197</td><td/><td/><td>_B13E761F_9106_4916_9FD1_63E69B39518F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT198</td><td/><td/><td>_2103A33E_5CFD_4C00_8DC8_BA9F35B8F5E5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT199</td><td/><td/><td>_4103ACC6_B389_4512_9B30_6E8ACFEA7D00_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td/><td/><td>_C53FE15D_250C_4F1E_949A_865AA742A785_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT200</td><td/><td/><td>_B9E7B850_4182_4B06_A8EB_D92178ACD65E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT201</td><td/><td/><td>_37ECC064_3F65_4F5B_B894_40FBB3D6C29A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT202</td><td/><td/><td>_19D2D261_BD16_4641_B601_7A401C697234_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT203</td><td/><td/><td>_DA6131CD_AECA_42BD_8105_A6EE0079A126_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT204</td><td/><td/><td>_406CF6E9_FEBE_4342_9338_A799BE29D5D5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT205</td><td/><td/><td>_DF81DD2B_4078_4DF4_9FB8_E96424BA4387_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT206</td><td/><td/><td>_6C3DB224_AFFA_4C27_8E64_5DFED98B043C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT207</td><td/><td/><td>_51CDB1E5_3216_4C9C_A74E_7BB4875D119F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td/><td/><td>_75A1B585_2E94_412B_A74A_11673BEB8BC7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td/><td/><td>_6338A56B_9CCB_4DC4_8632_80A9F5BDDC7E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td/><td/><td>_AEEA5A39_7D42_48E6_BC83_CD756046DC5D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td/><td/><td>_0518C40F_428B_4F31_9E94_2B5EE67FA4C6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td/><td/><td>_DA4580EB_04C2_460E_B5BE_25C6203F7050_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td/><td/><td>_1054FB26_D9CA_4823_9E46_51210E82C6F1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td/><td/><td>_51B3182C_9CA7_4806_B032_ACA1844DF7E3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td/><td/><td>_40D6ABE0_C8E0_4FF7_96CF_1C0EBA47E126_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td/><td/><td>_5CF79490_5901_4B6C_A643_5E661F9D5902_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td/><td/><td>_B9A93B29_74BD_4A08_BE92_2113DB41BB06_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td/><td/><td>_AD13DD2E_4603_4FD8_8B6E_85BF71B6723A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td/><td/><td>_3D7D5A7F_EB4D_45C9_A474_2E89DF31FBC7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td/><td/><td>_A3B7AA1C_7ADE_4AAB_AF5E_A0A1B4E44E50_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td/><td/><td>_BD4CDCCE_AAFC_48F3_ABD3_72E540574937_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT34</td><td/><td/><td>_5A6E1412_60B6_4CFD_823F_4989AE9E28CB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT35</td><td/><td/><td>_9FC7E1FB_B012_49AD_B4C3_FAAADC77B072_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT36</td><td/><td/><td>_9BEC16FF_5934_4BB2_99FA_4DD567D1D95E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT37</td><td/><td/><td>_0D740269_BBFB_47C9_A8AB_87FE9E78C014_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT38</td><td/><td/><td>_64BD03B1_27F0_49A6_972A_B9D634E6BB24_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT39</td><td/><td/><td>_E0DEE4F2_8C3A_4D71_9982_B327CC480563_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT4</td><td/><td/><td>_BBBBD9C3_D378_40A1_A890_F4D71F391F66_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT40</td><td/><td/><td>_B45414C4_A2AD_4AAE_8BA2_09312D133509_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT41</td><td/><td/><td>_F4AAA611_1746_4DDE_AEB5_F63B043513C5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT42</td><td/><td/><td>_4A883643_A8EC_481F_9497_FDF273B8BE45_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT43</td><td/><td/><td>_9AFEC5F5_9FD8_415D_8748_14729D29F497_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT44</td><td/><td/><td>_F2159D3A_782F_4E67_AD2D_B0D6B66C88E5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT45</td><td/><td/><td>_9471D973_C3BF_44CD_8835_22BA4E3FE39C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT46</td><td/><td/><td>_7CBAA731_7682_423B_874F_0DE691AAD28E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT47</td><td/><td/><td>_218C29C1_AB5A_4BCD_B2AD_CC3B71567A6B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT48</td><td/><td/><td>_AB4C4D80_830F_4885_9FF1_74A4096CC7AD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT49</td><td/><td/><td>_D6453082_6B09_4427_8945_BF58E8DE6E5D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td/><td/><td>_889CBC78_E6E0_4ACE_8B73_7A79E0856D1B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT50</td><td/><td/><td>_B75D4B2C_9255_4ECB_A758_9B7044BFFD4C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT51</td><td/><td/><td>_747FE20A_2FA3_4071_933D_BD89D8BCD9F4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT52</td><td/><td/><td>_C0002EF1_471A_49FE_980B_B89E7BB31C0B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT53</td><td/><td/><td>_76079793_FD19_4F2D_8AA1_E616740269D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT54</td><td/><td/><td>_3FD9AC38_3206_4551_AAB4_78201D93C165_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT55</td><td/><td/><td>_D4AB82BF_0978_495D_9C6A_32A48C4EB2ED_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT56</td><td/><td/><td>_12956BA5_DDB2_4ABF_8E86_0BB92F31C67B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT57</td><td/><td/><td>_65707505_B35F_44FB_BA45_68DE57C476EC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT58</td><td/><td/><td>_3BD85004_F3EB_417A_9C45_366F4F62DB21_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT59</td><td/><td/><td>_20EF7BC1_2FA8_4701_B771_0ACA74B9BAA5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT60</td><td/><td/><td>_75C5EF69_3009_40D3_AD31_46187546DAC8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT61</td><td/><td/><td>_59782824_79A2_4DDB_AAE4_BDB33E92F79B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT62</td><td/><td/><td>_9AFDF636_4778_4475_8CE2_3E1FACEDFE11_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT63</td><td/><td/><td>_64EE33CF_8CD1_4D10_9634_441D5298AE46_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT64</td><td/><td/><td>_03009949_1D23_42DE_9253_D8583CB2EFC0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT65</td><td/><td/><td>_7CA6162B_42CF_486E_8FF4_2EDA8BD835A5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT66</td><td/><td/><td>_08923C5F_C29A_4BC1_86DF_83C0360D80BB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT67</td><td/><td/><td>_DB99A7A9_BF50_4D54_B59E_0D4A73834F41_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT68</td><td/><td/><td>_FFE888FE_9A16_4180_AC40_7DFC54B1B6E0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT69</td><td/><td/><td>_76070CDB_5E8F_4436_8269_A540D9350049_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT70</td><td/><td/><td>_6B88EED5_2A56_4EE2_BA2B_B4B277367B4C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT71</td><td/><td/><td>_E0333579_4F20_4524_A20D_56C11AC5C2F5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT72</td><td/><td/><td>_7ECA17BA_0736_4DA3_BC2D_E40EB961B2EB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT73</td><td/><td/><td>_12CC8ABF_343E_4D37_81BE_743F68183C50_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT74</td><td/><td/><td>_5CC70BAB_05E6_457C_B1E3_6189260DB49F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT75</td><td/><td/><td>_DFBD191F_0E1C_4A5F_8B9E_3EFD217E701C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT76</td><td/><td/><td>_F09AA656_4C60_471F_A9F5_B362FF279E5A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT77</td><td/><td/><td>_C83612E3_A208_4E7B_B117_A4C2BC3D4F8D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT78</td><td/><td/><td>_38FE6957_B733_47DA_9E6E_E780CEE6E938_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT79</td><td/><td/><td>_A31322F8_6442_4E16_838F_F117E262C15F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td/><td/><td>_FBB058E1_ECD9_484D_AC6F_198C036504BD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT80</td><td/><td/><td>_FF878B24_FCAB_49EC_A449_7AF25C7241EC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT81</td><td/><td/><td>_498865E6_4215_43B0_AEE7_7D1D76370317_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT82</td><td/><td/><td>_CB3101BE_60E9_49CB_9BD7_E60D525CC66A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT83</td><td/><td/><td>_F88ECA8B_CEBB_464A_8E08_C7D25008A925_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT84</td><td/><td/><td>_A10E7C87_D7F9_4A6E_A670_44DC609AA9C2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT85</td><td/><td/><td>_04D9CE96_EF3F_4CB4_A8A0_85BE8611EA28_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT86</td><td/><td/><td>_488EBB43_2E03_4989_89EC_8A457FBE26DF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT87</td><td/><td/><td>_A20BF19A_8FC2_4E40_AE71_EC4E954036F0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT88</td><td/><td/><td>_AA72556B_BB13_4D88_BEA3_5F415DF6D0D6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT89</td><td/><td/><td>_21D25653_D1DA_47F9_AD86_460AB7C619F5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td/><td/><td>_FC07B2B6_34B8_47A7_9A0D_E977B5045C0D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT90</td><td/><td/><td>_7570DC03_580C_4F2B_BEDA_19F8CAB7D4B2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT91</td><td/><td/><td>_FB5E7617_195B_4312_A875_DB1091865450_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT92</td><td/><td/><td>_2B05DB29_2382_40BE_928A_A053B7F226EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT93</td><td/><td/><td>_F2BDB09D_6D57_4AD7_BEB4_5A178B15BCFA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT94</td><td/><td/><td>_D74BBFF6_C72F_4EFA_9D96_151A5468B4D3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT95</td><td/><td/><td>_409A0A00_2CE0_45CF_B968_F36C9739C2A8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT96</td><td/><td/><td>_03BE0D1E_4BE1_4630_B511_52F61308C722_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT97</td><td/><td/><td>_07AC374C_839F_4012_A5F9_39C506E98C4E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT98</td><td/><td/><td>_53969D38_DEE8_4D1C_AD37_3619CDE62C04_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT99</td><td/><td/><td>_E23BC48A_321F_44C4_BBF7_BE48538467E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>IS_ININSTALL_SHORTCUT</td><td/><td/><td>_8CD309D0_0803_406A_AB34_01DA6A25932D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>JAWTAccessBridge_32.dll</td><td/><td/><td>_3A4DFC45_2926_4AFC_81B2_51764F55D5C4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>JavaAccessBridge_32.dll</td><td/><td/><td>_50DBA0DD_017C_4B75_8BFE_03D3472962F5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>MahApps.Metro.dll1</td><td/><td/><td>_91BB07B2_63DE_4FB3_B81B_C712A1CF2459_FILTER</td><td/><td/><td/><td/></row>
		<row><td>MahApps.Metro.dll3</td><td/><td/><td>_4671F62B_CD59_4070_8660_EFAB530AD611_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Practices.ServiceLocation.dll2</td><td/><td/><td>_2D4310E0_58DE_40CE_8F48_61A4C99AA748_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Newtonsoft.Json.dll2</td><td/><td/><td>_91D697E6_4FCF_4FB9_93DE_00D080484897_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Ookii.Dialogs.Wpf.dll</td><td/><td/><td>_3BBCBBD6_6034_460A_B1A0_34087B12B728_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Prism.Mef.Wpf.dll2</td><td/><td/><td>_CD2B4A00_AC0E_4E20_87FA_2F275A72AE24_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Prism.Wpf.dll2</td><td/><td/><td>_DBDC23F5_88AF_4607_B60A_99AA7855D8FA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Prism.dll2</td><td/><td/><td>_7F444AC2_3E58_4217_8798_97A8E0FB2C99_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SQLite.Interop.dll</td><td/><td/><td>_3E94CA77_E33C_45D5_A4FE_E129087ED37A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SQLite.Interop.dll1</td><td/><td/><td>_EAB925D7_68BD_4124_80C6_12F7B857B739_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.Fonts.dll</td><td/><td/><td>_7BDE21D8_6ACB_43DB_985F_BF6A17B60812_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.Previewers.dll</td><td/><td/><td>_75856389_E4D5_48F7_A13F_B2F84E837D9C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.AdbViewer.dll</td><td/><td/><td>_59CD3CF6_DC96_44DB_90DD_9D2297CE2242_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.Controls.dll</td><td/><td/><td>_D966CD3C_9C37_4807_9650_9F6D4480AF27_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.Converters.dll</td><td/><td/><td>_03D463A1_42E5_4D86_A74B_BC062A2ACD76_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.Info.dll2</td><td/><td/><td>_3C646B21_FF7E_4A10_A2C6_ED742CDB5AC8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.Infrastructure.dll</td><td/><td/><td>_5729FA0D_7A35_4419_8CE8_BF3A32D2C48E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.MessageBoxes.dll</td><td/><td/><td>_8866158E_AFCA_43D7_877E_24F0C0B5A404_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Singularity.UI.Themes.dll</td><td/><td/><td>_E45A7EE7_9DC6_4374_AAFB_6FC5E52ECA7A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SingularityForensic.dll</td><td/><td/><td>_95F20FC7_653D_4A22_A118_E0387BB2BC58_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SingularityShell.exe2</td><td/><td/><td>_AFAA6CBD_B18B_43D8_BA1A_A743CCA0BE0A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SingularityShell.vshost.exe2</td><td/><td/><td>_758359E8_8777_40A8_8C0C_DE700F3407EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Data.Sqlite.dll2</td><td/><td/><td>_C9E61A8F_84C7_4484_BEF4_525013F31AE0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.dll2</td><td/><td/><td>_723B597E_05D4_4976_9010_00E5AB02880E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>UserMsgUs.dll</td><td/><td/><td>_D015E131_AE3A_4F4A_90DB_9E0BAC6BC040_FILTER</td><td/><td/><td/><td/></row>
		<row><td>WindowsAccessBridge_32.dll</td><td/><td/><td>_635799F9_C86F_4DBB_9F88_C8C9C9CC5238_FILTER</td><td/><td/><td/><td/></row>
		<row><td>adb.exe</td><td/><td/><td>_F233B438_AFBF_41ED_B1F8_B9626A87E731_FILTER</td><td/><td/><td/><td/></row>
		<row><td>awt.dll</td><td/><td/><td>_D7531CD5_EB33_434A_9454_E145E18E9382_FILTER</td><td/><td/><td/><td/></row>
		<row><td>bci.dll</td><td/><td/><td>_4B937F15_A525_46B1_942E_678C01995518_FILTER</td><td/><td/><td/><td/></row>
		<row><td>cdfcphoto.dll</td><td/><td/><td>_2710CB92_D908_4DFE_BDE2_AAF03EC9EBE7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>cdfcqd.dll1</td><td/><td/><td>_7E885843_3167_4C63_BF07_43D189EA33E2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>cdfcqd.dll3</td><td/><td/><td>_28E6F6AA_F7FB_4041_B475_C91B52F1BCDA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dcpr.dll</td><td/><td/><td>_E3BF52D4_3A6D_467F_A452_573B13CEBA35_FILTER</td><td/><td/><td/><td/></row>
		<row><td>debmp.dll</td><td/><td/><td>_52820449_5A55_4864_B7C3_FC6104BB63A6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>decora_sse.dll</td><td/><td/><td>_13C20CAB_B793_4DBF_B5AC_31DFBA028B9B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dehex.dll</td><td/><td/><td>_86C08691_7C4B_4CD1_A4A3_A1E26DD8DAEF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>deploy.dll</td><td/><td/><td>_1B3893ED_FA56_4642_8949_A072C9B07C72_FILTER</td><td/><td/><td/><td/></row>
		<row><td>deployJava1.dll</td><td/><td/><td>_E1805329_21E7_455C_AD57_2C75C9F62077_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dess.dll</td><td/><td/><td>_25C051D0_8BD9_495F_8E1D_3EDF45B1B04F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>detree.dll</td><td/><td/><td>_6C7CC0C7_B852_4080_B60D_799CF6922206_FILTER</td><td/><td/><td/><td/></row>
		<row><td>devect.dll</td><td/><td/><td>_54423915_06D7_47F8_81A9_66E7C28DB45D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dewp.dll</td><td/><td/><td>_B512B585_4394_41B6_97E0_17720C633D7C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dt_shmem.dll</td><td/><td/><td>_A16BD527_655A_4AE1_AAFB_532D8957FF4E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>dt_socket.dll</td><td/><td/><td>_F8AC24FE_E115_492F_AA76_CB4D13CC8C2B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>eula.dll</td><td/><td/><td>_4E57B5C5_1A7C_4BEE_9B36_32433CC781DB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>fontmanager.dll</td><td/><td/><td>_DD2BC61F_AB64_4C66_87D3_0FF78E87979D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>fxplugins.dll</td><td/><td/><td>_BD8562D1_E8B3_4A4F_B9DC_D37385BF9627_FILTER</td><td/><td/><td/><td/></row>
		<row><td>glass.dll</td><td/><td/><td>_D8C5D876_3A1B_4FC3_822D_BF8AB9438E20_FILTER</td><td/><td/><td/><td/></row>
		<row><td>glib_lite.dll</td><td/><td/><td>_D1CC9233_7E23_4A56_B49A_AB7D2CAAF4D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>gstreamer_lite.dll</td><td/><td/><td>_87002831_03D5_4A41_934E_9D5AFA2D7F20_FILTER</td><td/><td/><td/><td/></row>
		<row><td>hprof.dll</td><td/><td/><td>_E59191C8_3F4F_4574_ABBA_A33A5FE33EF4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibfpx2.dll</td><td/><td/><td>_68E888BB_9DF7_43F3_9980_93331415B8B3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibgp42.dll</td><td/><td/><td>_DBAC9B69_5996_4488_AEAB_DD44F67CF497_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibjpg2.dll</td><td/><td/><td>_BB3F2F43_9940_46DE_BA2A_94BC09A933A7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibpcd2.dll</td><td/><td/><td>_77974814_CBD4_42AB_8EF0_46A890536EB5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibpsd2.dll</td><td/><td/><td>_E2F9C34F_3934_4DBF_8196_66C47C962ACC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibxbm2.dll</td><td/><td/><td>_1654051C_2693_4C6D_8E5E_53F7D42DF699_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibxpm2.dll</td><td/><td/><td>_9BDD5D42_0EC0_4BA1_AB94_BF452574B1BE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ibxwd2.dll</td><td/><td/><td>_57082757_071A_473C_97A3_72C68EEA4E60_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd32.dll</td><td/><td/><td>_16B8B1CB_9A81_45A4_8F8F_396188C98801_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd42.dll</td><td/><td/><td>_F5BAD775_2E2A_40EE_8F01_DFA983599CEC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd52.dll</td><td/><td/><td>_17EAC742_CD5F_49E2_9CED_2D471868CB45_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd62.dll</td><td/><td/><td>_DD8F2E91_919B_4CFC_8155_C0E4CCC55AD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd72.dll</td><td/><td/><td>_EF739E0C_3FAD_42DA_B683_086554B7469C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcd82.dll</td><td/><td/><td>_86B9C23E_181F_4ABC_BD4A_E61D5681C949_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcdr2.dll</td><td/><td/><td>_62E38B00_4B7B_4A4A_B2E8_1E5E2B382E6C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcm52.dll</td><td/><td/><td>_1CE19AC5_A771_4455_B14F_FDAF85D49E9D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcm72.dll</td><td/><td/><td>_2385FFA1_DB40_4965_99D7_4355EBB82674_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imcmx2.dll</td><td/><td/><td>_493B588E_0D4E_4F13_A150_5CE27CBA2986_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imdsf2.dll</td><td/><td/><td>_EC4AB083_E7C7_4E83_99B7_3CDCA2BC4655_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imfmv2.dll</td><td/><td/><td>_E13C0E85_DCF6_46E1_8B45_66E74583F21F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imgdf2.dll</td><td/><td/><td>_2392E43A_4049_40BE_98AB_B457C744ED0A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imgem2.dll</td><td/><td/><td>_035DD963_A52A_4691_B224_0100B9A0B23C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imigs2.dll</td><td/><td/><td>_8D563C4F_9826_479C_A58C_C49B6C7625AC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>immet2.dll</td><td/><td/><td>_2C5FD828_DD05_4A2D_BE2B_A4BA2D2390AE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>impif2.dll</td><td/><td/><td>_B7B3EBF8_8580_44A2_B9DC_1DA6ABBCA7BE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imps_2.dll</td><td/><td/><td>_72774D3F_3C1F_4C3C_A626_CD7FFC14C415_FILTER</td><td/><td/><td/><td/></row>
		<row><td>impsi2.dll</td><td/><td/><td>_ABA881AF_1A8F_45AE_A9FA_029928872D7F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>impsz2.dll</td><td/><td/><td>_9ACA4AC3_C7B0_415E_8BD4_755008174B1F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>imrnd2.dll</td><td/><td/><td>_8F632026_63D6_4FBC_9BFD_20C513F8AF0D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>instrument.dll</td><td/><td/><td>_A9ADCE6F_B0C0_46C2_BA81_E9A73ADC10AC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>iphgw2.dll</td><td/><td/><td>_03A17531_08A1_4D46_B632_680B6E8FD3E4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>isgdi32.dll</td><td/><td/><td>_EE057B5B_21D2_4C6E_980E_6335E392E5A2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>j2pcsc.dll</td><td/><td/><td>_5A232250_4F9C_45B5_B9C8_A37073926549_FILTER</td><td/><td/><td/><td/></row>
		<row><td>j2pkcs11.dll</td><td/><td/><td>_9B841663_9CDB_40FF_AAA0_C5B7E1BE7C1D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jaas_nt.dll</td><td/><td/><td>_5E4A4B98_BD98_4D56_8F1B_610D21DEB091_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jabswitch.exe</td><td/><td/><td>_87AD32F9_DEDD_4F2B_97A2_8429F45564DA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>java.dll</td><td/><td/><td>_6DF4886C_CC44_455D_AD58_46CB2A653DFB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>java.exe</td><td/><td/><td>_FB9A1ECF_FEBD_4BC0_911B_73B7B47B70F7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>java_crw_demo.dll</td><td/><td/><td>_4B78DF6A_0035_42DC_BB3B_3217DD271972_FILTER</td><td/><td/><td/><td/></row>
		<row><td>java_rmi.exe</td><td/><td/><td>_312E200F_99A0_41D6_B518_74D6891E3CA2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javacpl.exe</td><td/><td/><td>_84F854E8_F73D_4540_BB39_9CE956359A50_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javafx_font.dll</td><td/><td/><td>_FC50A9D6_D754_4D78_8A6F_52C6E5A60D40_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javafx_font_t2k.dll</td><td/><td/><td>_B6C5B7C0_31DF_4EF4_AD78_6FEAD3BA829E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javafx_iio.dll</td><td/><td/><td>_C985F2C4_F67A_4020_B923_1D32E538AF6C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javaw.exe</td><td/><td/><td>_5EA14AB9_CBB3_47C6_B0CE_A3EA04A43140_FILTER</td><td/><td/><td/><td/></row>
		<row><td>javaws.exe</td><td/><td/><td>_F4826802_8AB0_4F15_9E27_E2E7A04480F7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jawt.dll</td><td/><td/><td>_374CE311_BC9C_42C4_8F27_930EC873A28A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jdwp.dll</td><td/><td/><td>_F5F5057C_B288_4D1E_AEDF_1465F6A0D620_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jfr.dll</td><td/><td/><td>_EDC62058_9E3A_464B_BCAA_07012F2B01F1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jfxmedia.dll</td><td/><td/><td>_F66EC04C_8B94_4B27_B90C_05953245CD0F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jfxwebkit.dll</td><td/><td/><td>_12B9803B_81CC_4A8D_8564_7CC06AA16AA7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jjs.exe</td><td/><td/><td>_726970DB_609E_4AF0_A046_D36DE3E34582_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jli.dll</td><td/><td/><td>_826183CF_67E4_4F3C_90EF_30134642B875_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jp2iexp.dll</td><td/><td/><td>_1A04392D_64E6_4D9F_9D05_698B3BEA3A3D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jp2launcher.exe</td><td/><td/><td>_E2FB870D_D455_40DE_9B72_6088BBD8F5C3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jp2native.dll</td><td/><td/><td>_8A2A035B_D38E_4181_B707_D84B4792D501_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jp2ssv.dll</td><td/><td/><td>_C8936537_1AA2_4EF7_8AED_D3750D686E60_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jpeg.dll</td><td/><td/><td>_29175023_FF61_4CAD_80D2_9655B3170F15_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jsdt.dll</td><td/><td/><td>_14287C61_C08F_4334_88B6_28A2723BA062_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jsound.dll</td><td/><td/><td>_58DA4D28_0917_46A0_94B1_04CE74AD16B8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jsoundds.dll</td><td/><td/><td>_730FB4E3_5E1E_45E7_9655_FC730EBC4C35_FILTER</td><td/><td/><td/><td/></row>
		<row><td>jvm.dll</td><td/><td/><td>_5E31A7D6_5923_4206_9F53_713782D68271_FILTER</td><td/><td/><td/><td/></row>
		<row><td>kcms.dll</td><td/><td/><td>_6DC86560_8886_4B5A_B8E9_566622E63D81_FILTER</td><td/><td/><td/><td/></row>
		<row><td>keytool.exe</td><td/><td/><td>_133BA390_6083_4755_B0B9_EC995DA07E37_FILTER</td><td/><td/><td/><td/></row>
		<row><td>kinit.exe</td><td/><td/><td>_AD9C9A91_DE23_456B_9A8A_6531D0AF366C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>klist.exe</td><td/><td/><td>_E4E94D56_1179_4F33_8663_B2901BFFB08E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ktab.exe</td><td/><td/><td>_E1C398FE_E86C_44AC_B1DB_1B99E9F55793_FILTER</td><td/><td/><td/><td/></row>
		<row><td>lcms.dll</td><td/><td/><td>_E7FF27BD_00AF_46EC_816B_F4E515684B30_FILTER</td><td/><td/><td/><td/></row>
		<row><td>management.dll</td><td/><td/><td>_996EA9DE_A040_4F86_A1D1_F48CE54421C0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>mlib_image.dll</td><td/><td/><td>_A97A85A4_41C1_4772_933E_0B1A58557E6B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>msvcp120.dll</td><td/><td/><td>_27B8F3F8_A819_4C25_85C6_4CACD4A8B8E4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>msvcr100.dll</td><td/><td/><td>_6044FFCF_1F08_481F_A15A_46F78B50265F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>msvcr100.dll1</td><td/><td/><td>_A1753494_4C8E_429C_84B6_FC207C4C74F3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>msvcr120.dll</td><td/><td/><td>_B69F0F84_FA60_4862_BCD6_1D2EE7865E95_FILTER</td><td/><td/><td/><td/></row>
		<row><td>net.dll</td><td/><td/><td>_2686A229_7AB7_48BA_BE50_735DA20A3D70_FILTER</td><td/><td/><td/><td/></row>
		<row><td>nio.dll</td><td/><td/><td>_91A766E3_9E35_48F5_A97B_7579F09033EF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>npdeployJava1.dll</td><td/><td/><td>_793BD717_28E4_4BFD_852A_9F5C24F3BD51_FILTER</td><td/><td/><td/><td/></row>
		<row><td>npjp2.dll</td><td/><td/><td>_B8DFB160_85AB_49ED_9CF2_5983E85460EE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>npt.dll</td><td/><td/><td>_B2626B49_077D_4201_B1F6_F13712EC304C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ocemul.dll</td><td/><td/><td>_2A779A56_9D63_4A44_81BB_7EDD8CB857B9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>orbd.exe</td><td/><td/><td>_A62D613A_49D5_49CE_AB65_E25949DE0365_FILTER</td><td/><td/><td/><td/></row>
		<row><td>oswin32.dll</td><td/><td/><td>_5EEA052D_A162_44C6_85D1_F7B741610055_FILTER</td><td/><td/><td/><td/></row>
		<row><td>pack200.exe</td><td/><td/><td>_01432DDE_05F9_473C_8FB1_31219CFBE3C9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>policytool.exe</td><td/><td/><td>_D72F953C_5A5B_477D_981F_DCAC1E388626_FILTER</td><td/><td/><td/><td/></row>
		<row><td>prism_common.dll</td><td/><td/><td>_FE12F785_BA33_4C57_ACC7_0F4631060A1D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>prism_d3d.dll</td><td/><td/><td>_35B8949C_2019_4978_A686_743DA25EDDB4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>prism_sw.dll</td><td/><td/><td>_D0FD0339_2BCC_4D3E_AAC8_DA752881EF50_FILTER</td><td/><td/><td/><td/></row>
		<row><td>resource.dll</td><td/><td/><td>_AD9C6315_EEE3_49D3_B2DB_3FD4FAF4D379_FILTER</td><td/><td/><td/><td/></row>
		<row><td>rmid.exe</td><td/><td/><td>_03F4B7DF_1F7F_448E_9384_3F1BD73FD023_FILTER</td><td/><td/><td/><td/></row>
		<row><td>rmiregistry.exe</td><td/><td/><td>_1D8A905E_C1EE_4F41_BD3A_EB3C1FBE1335_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccanno.dll</td><td/><td/><td>_698377EA_7B5E_443B_B14E_AA06309D7A5B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccca.dll</td><td/><td/><td>_8D747312_AFD2_4745_A223_D1D6A47ADEB4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccch.dll</td><td/><td/><td>_055B489C_FFA5_427C_B423_77022F57A50C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccda.dll</td><td/><td/><td>_074EC3CE_BDA9_4144_B63F_B1DD5C9B342A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccdu.dll</td><td/><td/><td>_BE1EF603_F5F6_4B0C_9B84_EA10ABE957D9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccfa.dll</td><td/><td/><td>_4466827F_89DE_4314_802B_168D0248A571_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccfi.dll</td><td/><td/><td>_EDD3A95C_F15E_4F80_8F53_E665BAD9E0DA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccfmt.dll</td><td/><td/><td>_A9C86D8F_8D92_40EE_A851_F98FE53194A8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccfnt.dll</td><td/><td/><td>_A6B1441C_2D38_4C37_811C_F128A0840A84_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccfut.dll</td><td/><td/><td>_F1833C9B_B997_4311_8B0C_57BD9B763624_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccind.dll</td><td/><td/><td>_6CC8AFFE_0856_4F12_8756_1AA2AE489FE7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>scclo.dll</td><td/><td/><td>_ED153F9F_F93A_43F4_AB3A_B06375DE3701_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccole.dll</td><td/><td/><td>_F5309CD0_2A87_4AC4_BCE2_E448211CC961_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccra.dll</td><td/><td/><td>_43823AB3_8F7E_4BD4_B0CD_A3A373C09853_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccsd.dll</td><td/><td/><td>_1CA6F0D0_8346_4778_A019_E8A9E1E754CE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccta.dll</td><td/><td/><td>_EEA42AA4_E622_4CF4_AB3B_F51E36A32AD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccut.dll</td><td/><td/><td>_42F09FB7_35D0_4B1E_AE32_5BCC21072C7F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccvw.dll</td><td/><td/><td>_E3529650_A383_4A0E_BC98_4AC9F181F608_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sccxt.dll</td><td/><td/><td>_E93AF037_50A9_4138_BB9F_5EDF377841EB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>servertool.exe</td><td/><td/><td>_0B5FC04B_64C2_44D8_B9DA_8A270028631C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>splashscreen.dll</td><td/><td/><td>_63A385CD_B276_4713_9E40_7110650FE6AA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ssv.dll</td><td/><td/><td>_9270488A_DDFC_4DBD_9030_A160E848F4CE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ssvagent.exe</td><td/><td/><td>_1FD063B2_D388_47CE_9122_3702F25CA77C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sunec.dll</td><td/><td/><td>_8B6EF8A9_77CA_44AF_A39E_F20AA8D88AC7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>sunmscapi.dll</td><td/><td/><td>_825C48DE_8618_47C2_8298_40E52D9D0337_FILTER</td><td/><td/><td/><td/></row>
		<row><td>t2k.dll</td><td/><td/><td>_2906AF12_73A8_437A_A31C_E516172FB951_FILTER</td><td/><td/><td/><td/></row>
		<row><td>tnameserv.exe</td><td/><td/><td>_5C7CC910_9D54_4BF4_B8B4_F68BF1568187_FILTER</td><td/><td/><td/><td/></row>
		<row><td>unpack.dll</td><td/><td/><td>_A213657A_0164_4B5F_9C12_FD1530C270E4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>unpack200.exe</td><td/><td/><td>_EDAB804E_6E3D_4BF0_868F_384B598F8ADE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>verify.dll</td><td/><td/><td>_09FF2DD0_4AE6_4DC7_A592_5555187132B6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsacad.dll</td><td/><td/><td>_3D5E0DC2_8E26_4159_BAB3_13A05B2DA17A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsacs.dll</td><td/><td/><td>_63D72BE4_6299_4022_8B14_647ADA2EF731_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsami.dll</td><td/><td/><td>_D1723AB3_83AE_4A4C_8CE2_16D673A121FF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsarc.dll</td><td/><td/><td>_E19FC200_F542_48C9_A819_665F9DBA253C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsasf.dll</td><td/><td/><td>_7EBD1302_D311_4761_B915_6372EB47EFA7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsbdr.dll</td><td/><td/><td>_0C3DF5B2_7CED_4642_8D55_99C5DB7A9EA7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsbmp.dll</td><td/><td/><td>_5EA3C2DC_1FE8_4B05_9B47_017C08780CF8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vscdrx.dll</td><td/><td/><td>_AD54442C_9A29_45E1_AD0E_45C1146822BD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vscgm.dll</td><td/><td/><td>_C8A37525_5D55_43C7_BA4B_C9D647A9F50E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdbs.dll</td><td/><td/><td>_7812D0C6_F557_4BC2_8FC9_7E670898F2B3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdez.dll</td><td/><td/><td>_7F671CE3_A0D4_4585_A79E_A2DBC4BD86EF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdif.dll</td><td/><td/><td>_D0EF97E3_E786_4109_9ECF_7B463B900ADB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdrw.dll</td><td/><td/><td>_A7C98CCF_9E40_425B_A4C4_6D2E60CF463B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdx.dll</td><td/><td/><td>_C970CB21_8435_45BE_A74C_500818FD6490_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdxla.dll</td><td/><td/><td>_F0558F65_4DE0_418E_8F4B_2DE21C4A122D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsdxlm.dll</td><td/><td/><td>_C24F700E_DE76_4DD1_BD87_692B279BBCE6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsemf.dll</td><td/><td/><td>_3504E8A0_3FCE_42CA_8150_EACE52DD2C69_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsen4.dll</td><td/><td/><td>_7B2DC2EE_4942_4275_B509_F630050E6867_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsens.dll</td><td/><td/><td>_C9A998E5_7888_4C19_A9C8_448386262570_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsenw.dll</td><td/><td/><td>_5B6E9CEC_54DD_4465_A0AA_B5E8EFFC8F87_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vseshr.dll</td><td/><td/><td>_59CFC1EC_5F22_43CF_B6B5_767626162CBE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsexe2.dll</td><td/><td/><td>_82394668_398A_4175_93EF_5733D4DCF897_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsfax.dll</td><td/><td/><td>_B4A4EFBB_CFE9_45F3_9658_4C2249B011CB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsfcd.dll</td><td/><td/><td>_F5CFDCC0_7E53_4BA7_AC39_CB817F50FC62_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsfcs.dll</td><td/><td/><td>_242A127C_5AC6_4092_9B29_6BD3F5C4F7D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsfft.dll</td><td/><td/><td>_E58D3205_BF32_4082_82FE_D8CB943085C9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsflw.dll</td><td/><td/><td>_CEEF8B64_82B9_48BC_B9CA_BDC6792B1533_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsfwk.dll</td><td/><td/><td>_6C898CA2_790F_4758_AE27_CE2DE238ECBF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsgdsf.dll</td><td/><td/><td>_6355DC2F_9FEB_48C9_8A31_E9E72ADE57CA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsgif.dll</td><td/><td/><td>_E19789B8_CA32_40D5_B41E_483FCCA54674_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsgzip.dll</td><td/><td/><td>_B75221E8_390F_4914_9C36_AE8184EC80AC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vshgs.dll</td><td/><td/><td>_4FBEA0CB_80C7_4D78_A3FA_5A5B0B13511A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vshtml.dll</td><td/><td/><td>_B5E51571_642E_49B5_B0A1_74D8A2363D56_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vshwp.dll</td><td/><td/><td>_4ABBB988_5DF3_4FC8_8002_75D6D0223353_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vshwp2.dll</td><td/><td/><td>_B54EBDC8_A5FB_49DF_9394_A6125983D706_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsich.dll</td><td/><td/><td>_13947D5A_02E7_4925_BCA2_8055D50ABC8C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsich6.dll</td><td/><td/><td>_6DF5A759_EE5B_4519_9961_2ABEC6F98D0C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsid3.dll</td><td/><td/><td>_35F5DE47_C665_4895_8D68_60769C7FEF4B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsimg.dll</td><td/><td/><td>_4DE75149_7086_4EB6_8A99_39AC6CAFF257_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsiwok.dll</td><td/><td/><td>_EBA2BCAA_6E45_4596_97FE_777FB90CA875_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsiwok13.dll</td><td/><td/><td>_4244235B_8D77_4DEE_B991_FB05FE3D2A3E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsiwon.dll</td><td/><td/><td>_21DFA924_1BEE_4264_BAC0_B8D13CCBB99D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsiwop.dll</td><td/><td/><td>_83DC9043_EC43_417D_ADFE_F50A5E111A69_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsiwp.dll</td><td/><td/><td>_43CF2F41_7596_4E95_A0D7_C95223254FE1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsjbg2.dll</td><td/><td/><td>_B34941E8_036D_4E72_9432_C553E07720A6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsjp2.dll</td><td/><td/><td>_323442FF_8BDA_4653_9C21_14048F8FD4BE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsjw.dll</td><td/><td/><td>_FE79E324_9CD8_47FA_A741_EED42B353A21_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsleg.dll</td><td/><td/><td>_7F286C78_B1AD_44C1_9B7C_49926EC20554_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vslwp7.dll</td><td/><td/><td>_D50B962E_A7C9_4C80_8BBC_B8D712FC75A0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vslzh.dll</td><td/><td/><td>_234A7AD4_6017_4331_AE48_AFBA75ECFDBA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsm11.dll</td><td/><td/><td>_1B06A250_2148_4B89_86A5_353D09197FE6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmanu.dll</td><td/><td/><td>_3262E378_7834_4960_B571_44150B1F34BB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmbox.dll</td><td/><td/><td>_AA9BD1D1_4F7A_4BDB_A6C2_FA0BD985B47C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmcw.dll</td><td/><td/><td>_4C8DD9ED_198C_4C67_955C_BC8110312C58_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmdb.dll</td><td/><td/><td>_F6CA8C99_C806_43EB_91D2_187983AE286D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmif.dll</td><td/><td/><td>_627D73CB_6594_4A95_A040_4F7E933C7C37_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmime.dll</td><td/><td/><td>_72FD4E00_7E05_4E15_B50A_C3AC1AFBD3F5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmm.dll</td><td/><td/><td>_4BAA8BA7_9EA3_496E_BF67_C8F4B6E27AE0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmm4.dll</td><td/><td/><td>_48CD2F6B_6D66_493E_A618_2CFA098F078B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmmfn.dll</td><td/><td/><td>_9FB26B3C_3C55_45B4_B5BB_540A67539568_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmp.dll</td><td/><td/><td>_5E820CAE_397D_41DC_8AA0_498D21738BBB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmpp.dll</td><td/><td/><td>_222DFF2A_2876_432B_978E_0D6929A0CD84_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmsg.dll</td><td/><td/><td>_B8C2B348_0971_467A_8A68_7D524FCC2EE5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmsw.dll</td><td/><td/><td>_07850B17_A7EC_430F_A191_B8710640CB88_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmwkd.dll</td><td/><td/><td>_B27C233C_1C2D_4888_AA62_575EFE5BAB58_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmwks.dll</td><td/><td/><td>_3553D9E2_4D26_4949_BBFC_8FDEC63C10A0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmwp2.dll</td><td/><td/><td>_4423F92E_B721_4EAA_9298_9DC0DB5F3ACA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmwpf.dll</td><td/><td/><td>_89B847FF_97E6_47D3_A6FE_B12E02977636_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsmwrk.dll</td><td/><td/><td>_1D00D1D5_4278_4B99_B1FD_9F5A38724775_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsnsf.dll</td><td/><td/><td>_3C201302_A382_4C44_9F12_8CDC5198EA6F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsolm.dll</td><td/><td/><td>_23E98DCC_36F6_450B_A912_EE6B55258759_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsone.dll</td><td/><td/><td>_B015CF32_069A_4EFA_9016_2208308CE9D3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsow.dll</td><td/><td/><td>_C54F064A_B375_496A_AB3A_82D9431EF0A9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspbm.dll</td><td/><td/><td>_D8CFEE68_3CCC_4B0B_A1EB_753AFBA847DF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspcl.dll</td><td/><td/><td>_6E66338E_7DEB_4505_B4B9_1A783D79C31A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspcx.dll</td><td/><td/><td>_0D3D2024_2EB4_4AE8_9E43_3DBECAE5AED1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspdf.dll</td><td/><td/><td>_84E96712_265E_48B8_8AE0_38434A0E205C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspdfi.dll</td><td/><td/><td>_6E073811_C3DD_42B8_BC30_3F1006C349CB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspdx.dll</td><td/><td/><td>_FE5F0DAA_0492_4155_B19C_2EB7E9ABEEA8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspfs.dll</td><td/><td/><td>_9FDDF93A_7EEF_4B5E_9C1E_9A8B6996A423_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspgl.dll</td><td/><td/><td>_0A832C4B_16E7_490D_A6D0_27C73EC444E5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspic.dll</td><td/><td/><td>_FDE73132_4129_4922_917F_2C794FDFF085_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspict.dll</td><td/><td/><td>_4A838E38_228B_417C_8656_AE5842F4D7AA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspng.dll</td><td/><td/><td>_17504649_6116_48DE_B08A_5C4CF53601C6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspntg.dll</td><td/><td/><td>_87761C51_251C_4AFF_A97C_F31AF2226234_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspp12.dll</td><td/><td/><td>_5BDDECF2_15C3_4F20_8D53_4F70CB6167B0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspp2.dll</td><td/><td/><td>_12C67293_A45A_42E6_BA42_4FE7230CE76A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspp7.dll</td><td/><td/><td>_9A94D2AE_C99F_4B98_B2B0_8C36BB126198_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspp97.dll</td><td/><td/><td>_1C9A4E56_1A81_4841_B230_23936618745B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsppl.dll</td><td/><td/><td>_12875E4A_C924_4582_BCDA_FBC0FD2C14C7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspsp6.dll</td><td/><td/><td>_73678F1D_8966_4E23_B9C9_43FD6943848E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspst.dll</td><td/><td/><td>_ABE47BC5_43FC_41BC_A23A_EAA0E5475543_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vspstf.dll</td><td/><td/><td>_0695B658_7F03_45B4_9069_08551A537917_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsqa.dll</td><td/><td/><td>_02A4E186_A792_4D17_B59A_A0F02CEB6FB7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsqad.dll</td><td/><td/><td>_E41F0BE0_BEED_4A86_9FFB_673147842D5E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsqp6.dll</td><td/><td/><td>_98D37E10_D1F7_4B8C_AFC3_D9FC20E13867_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsqp9.dll</td><td/><td/><td>_D288A737_02E7_47FA_9D49_7DCF21794984_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsqt.dll</td><td/><td/><td>_E9CDA612_3AF6_4B0C_A7C0_4E3B3B0E7571_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsrar.dll</td><td/><td/><td>_735DD33B_FEC5_4B7C_BFAB_68DC3481410B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsras.dll</td><td/><td/><td>_F1FA1A58_4017_49C2_A3D4_A1F853315D54_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsrbs.dll</td><td/><td/><td>_A3FE76B8_5EEA_4731_9FF3_6344416D6CB7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsrft.dll</td><td/><td/><td>_1BE6194F_0785_4DD6_8EDA_0C2D0BFABEEC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsrfx.dll</td><td/><td/><td>_D049620B_B721_4CCA_96E9_E12604C0DA3E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsriff.dll</td><td/><td/><td>_AD53BB4A_B6B7_48F3_B887_CB55F39A10B2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsrtf.dll</td><td/><td/><td>_6A3B70EF_390F_47CF_8A79_8AFE37A86BD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssam.dll</td><td/><td/><td>_E36B74B6_178C_4938_A87D_0B412ABFAA99_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssc5.dll</td><td/><td/><td>_84798E44_972E_456B_B874_8015F1511D4F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssdw.dll</td><td/><td/><td>_22A8AE9F_F537_4F8F_885F_0317FEB93AB5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsshw3.dll</td><td/><td/><td>_2F65AF89_E0E4_4102_BC89_5A3D855E3F5B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssmd.dll</td><td/><td/><td>_4B49112A_65E0_4FBE_89E8_93D03DF4C5B1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssms.dll</td><td/><td/><td>_CED0EC7B_4308_476C_A4B1_F12F28FB32C3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssmt.dll</td><td/><td/><td>_566157CB_5413_45FF_839B_BDE4AC2BB8F5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssnap.dll</td><td/><td/><td>_D52D76B4_1B07_44B9_89C2_A445D07036A6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsso6.dll</td><td/><td/><td>_EC832186_DA48_47AA_81A1_CD1DE3EE9641_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssoc.dll</td><td/><td/><td>_F16E420F_A807_4BB1_8866_B0EF32B21A9B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssoc6.dll</td><td/><td/><td>_7F120DC3_E036_4B44_BFAE_DA431EAACB8E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssoi.dll</td><td/><td/><td>_E8BB1457_6E0A_444A_B3BD_697ADCF74A0F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssoi6.dll</td><td/><td/><td>_2EEAF15C_948C_4B8D_AC92_A7603428F792_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vssow.dll</td><td/><td/><td>_DD53BE85_96FB_472E_8081_028168CA07D1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsspt.dll</td><td/><td/><td>_54F9F355_FA14_4B9A_BDB3_C5021C896BC7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsssml.dll</td><td/><td/><td>_58233608_FD11_43AE_881C_243421465983_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsswf.dll</td><td/><td/><td>_0FA1F8D0_A28C_44AA_8937_8B63699E8252_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstaz.dll</td><td/><td/><td>_DB8768BA_07A3_4FCF_A16C_9D5B01C6AFC9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstext.dll</td><td/><td/><td>_1DEBD9FF_4386_41BF_83D1_13F66BC0B4A4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstga.dll</td><td/><td/><td>_C3222F82_4B72_4597_B4DC_17DB15F7D981_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstif6.dll</td><td/><td/><td>_91E6E20F_B073_48F6_99CD_47AEDF58C236_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstw.dll</td><td/><td/><td>_4908D134_184C_46B2_A0D2_92A421F8ED77_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vstxt.dll</td><td/><td/><td>_3F348A0A_E1DB_4FCE_BFF0_F7A19D36730D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsvcrd.dll</td><td/><td/><td>_CEADE800_1BA8_4D08_B5F8_F4966C959404_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsviso.dll</td><td/><td/><td>_EB0407E1_EA6E_49DF_973B_EDC3A930FA54_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsvsdx.dll</td><td/><td/><td>_E7E6E66E_BCF0_4508_BF26_39EC9A71A7EC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsvw3.dll</td><td/><td/><td>_77463F19_6E0A_424C_8076_5FA903768D7A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsw12.dll</td><td/><td/><td>_DF7E35D1_A74C_40B9_8B74_7A4247925989_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsw6.dll</td><td/><td/><td>_4A76B552_9FFA_4D12_91B9_19D64F079AA2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsw97.dll</td><td/><td/><td>_86308861_F6ED_45E4_AEBE_198A53A57225_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswbmp.dll</td><td/><td/><td>_B79C2544_0D25_4A5C_8409_A3B877DBD8E0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswg2.dll</td><td/><td/><td>_78CA1D73_12B3_4F11_8F4B_EE3F4C01396F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswk4.dll</td><td/><td/><td>_90B3EC55_4879_4E3E_8726_97870EF384E3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswk6.dll</td><td/><td/><td>_210DD365_37AA_4546_AE95_D5914281A1E8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswks.dll</td><td/><td/><td>_3FBF9972_4658_4DE1_B8A2_789B54E5125F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswm.dll</td><td/><td/><td>_E94729F0_D9A0_4CF3_8A1B_788BBB326611_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswmf.dll</td><td/><td/><td>_BF7217B8_011C_41E4_9FDE_ECBE52393FB3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswml.dll</td><td/><td/><td>_7E4B53A9_9201_41F4_85D7_BFD07746EA93_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsword.dll</td><td/><td/><td>_CA39191B_8C88_494C_B5C3_E58FBABACAF4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswork.dll</td><td/><td/><td>_75140183_CEDD_454A_A5AD_8198A8BF4BD2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswp5.dll</td><td/><td/><td>_7D499266_6508_4A87_8F67_7B690D3FCEBC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswp6.dll</td><td/><td/><td>_0959E319_5318_4B56_9632_7F8EB0D1853A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpf.dll</td><td/><td/><td>_F0555929_E6E9_46BB_BE7F_39515DB4A4B0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpg.dll</td><td/><td/><td>_BEA1C423_5DF7_41D0_913C_7A3F6E39669A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpg2.dll</td><td/><td/><td>_F2D8AB84_0AF0_4244_803D_2B47B428E859_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpl.dll</td><td/><td/><td>_EB3DEADC_F76D_4E31_A0AC_4B5E0013C801_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpml.dll</td><td/><td/><td>_0D5478E6_8041_4884_A71D_C162185E5B67_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vswpw.dll</td><td/><td/><td>_4C5B54B9_DDF2_485A_B08D_8BDD0F3525B3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsws.dll</td><td/><td/><td>_26F06E9A_8366_4F6F_A8B4_3394CF731401_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsws2.dll</td><td/><td/><td>_7642027F_6D6B_44D9_9283_2930878472A1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxl12.dll</td><td/><td/><td>_3C1943B6_122F_48C7_B6E2_BD5549329058_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxl5.dll</td><td/><td/><td>_F9E0DD3B_BEBA_4674_B514_8C7516D6A42E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxlsb.dll</td><td/><td/><td>_134F0233_4EDE_4F55_AA1A_6FA35FC98DCB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxml.dll</td><td/><td/><td>_F164323A_750F_43EF_84D8_8C439FB0AF3C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxps.dll</td><td/><td/><td>_B1635E28_9D00_48D7_8717_45E3466BA2D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsxy.dll</td><td/><td/><td>_093FC44C_1DAB_44E8_9EFF_ED7251CF002E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vsyim.dll</td><td/><td/><td>_A25ACE57_5F31_47D2_9162_C840ECD6E8E9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>vszip.dll</td><td/><td/><td>_2EF650CF_6D1C_439C_9C37_AACC3B47E72D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>w2k_lsa_auth.dll</td><td/><td/><td>_F585F2FD_7224_417D_9FFD_32E34E3FB56D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>wsdetect.dll</td><td/><td/><td>_81D60D26_9564_400A_91DB_25593E11ECF5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>wvcore.dll</td><td/><td/><td>_0A6385FE_E61B_47EA_808C_489CD47528F9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>zip.dll</td><td/><td/><td>_62765EA0_CA80_4015_82A8_BD10737B42A6_FILTER</td><td/><td/><td/><td/></row>
	</table>

	<table name="ISCustomActionReference">
		<col key="yes" def="s72">Action_</col>
		<col def="S0">Description</col>
		<col def="S255">FileType</col>
		<col def="S255">ISCAReferenceFilePath</col>
	</table>

	<table name="ISDIMDependency">
		<col key="yes" def="s72">ISDIMReference_</col>
		<col def="s255">RequiredUUID</col>
		<col def="S255">RequiredMajorVersion</col>
		<col def="S255">RequiredMinorVersion</col>
		<col def="S255">RequiredBuildVersion</col>
		<col def="S255">RequiredRevisionVersion</col>
	</table>

	<table name="ISDIMReference">
		<col key="yes" def="s72">ISDIMReference</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISDIMReferenceDependencies">
		<col key="yes" def="s72">ISDIMReference_Parent</col>
		<col key="yes" def="s72">ISDIMDependency_</col>
	</table>

	<table name="ISDIMVariable">
		<col key="yes" def="s72">ISDIMVariable</col>
		<col def="s72">ISDIMReference_</col>
		<col def="s0">Name</col>
		<col def="S0">NewValue</col>
		<col def="I4">Type</col>
	</table>

	<table name="ISDLLWrapper">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s0">Source</col>
		<col def="s255">Target</col>
	</table>

	<table name="ISDependency">
		<col key="yes" def="S50">ISDependency</col>
		<col def="I2">Exclude</col>
	</table>

	<table name="ISDisk1File">
		<col key="yes" def="s72">ISDisk1File</col>
		<col def="s255">ISBuildSourcePath</col>
		<col def="I4">Disk</col>
	</table>

	<table name="ISDynamicFile">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">SourceFolder</col>
		<col def="I2">IncludeFlags</col>
		<col def="S0">IncludeFiles</col>
		<col def="S0">ExcludeFiles</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISFeatureDIMReferences">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISDIMReference_</col>
	</table>

	<table name="ISFeatureMergeModuleExcludes">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ModuleID</col>
		<col key="yes" def="i2">Language</col>
	</table>

	<table name="ISFeatureMergeModules">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
		<row><td>AlwaysInstall</td><td>CodeMeter_Runtime_Merge_Module_Win32.A961A077_4BD0_4C98_86BC_EE4A98CE550D</td><td>0</td></row>
		<row><td>AlwaysInstall</td><td>CodeMeter_Runtime_Merge_Module_Win64.1992E333_D17A_448B_8484_ED047109D182</td><td>0</td></row>
		<row><td>AlwaysInstall</td><td>WIBU_ShellExtension_Installer.2C5CFD32_85E0_4EDF_BEF3_50462A17B50E</td><td>0</td></row>
		<row><td>AlwaysInstall</td><td>WIBU_ShellExtension_Installer.573670D2_95A5_4F72_BD6A_63FA56A6342E</td><td>0</td></row>
		<row><td>AlwaysInstall</td><td>WibuCmNETInstaller.B396C8F90D0349058867D3D7030330FB.B396C8F9_0D03_4905_8867_D3D7030330FB</td><td>0</td></row>
	</table>

	<table name="ISFeatureSetupPrerequisites">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISSetupPrerequisites_</col>
	</table>

	<table name="ISFileManifests">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Manifest_</col>
	</table>

	<table name="ISIISItem">
		<col key="yes" def="s72">ISIISItem</col>
		<col def="S72">ISIISItem_Parent</col>
		<col def="L255">DisplayName</col>
		<col def="i4">Type</col>
		<col def="S72">Component_</col>
	</table>

	<table name="ISIISProperty">
		<col key="yes" def="s72">ISIISProperty</col>
		<col key="yes" def="s72">ISIISItem_</col>
		<col def="S0">Schema</col>
		<col def="S255">FriendlyName</col>
		<col def="I4">MetaDataProp</col>
		<col def="I4">MetaDataType</col>
		<col def="I4">MetaDataUserType</col>
		<col def="I4">MetaDataAttributes</col>
		<col def="L0">MetaDataValue</col>
		<col def="I4">Order</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISInstallScriptAction">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s72">Source</col>
		<col def="S255">Target</col>
	</table>

	<table name="ISLanguage">
		<col key="yes" def="s50">ISLanguage</col>
		<col def="I2">Included</col>
		<row><td>1033</td><td>0</td></row>
		<row><td>2052</td><td>1</td></row>
	</table>

	<table name="ISLinkerLibrary">
		<col key="yes" def="s72">ISLinkerLibrary</col>
		<col def="s255">Library</col>
		<col def="i4">Order</col>
		<row><td>isrt.obl</td><td>isrt.obl</td><td>2</td></row>
		<row><td>iswi.obl</td><td>iswi.obl</td><td>1</td></row>
	</table>

	<table name="ISLocalControl">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="I2">X</col>
		<col def="I2">Y</col>
		<col def="I2">Width</col>
		<col def="I2">Height</col>
		<col def="S72">Binary_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISLocalDialog">
		<col key="yes" def="S50">Dialog_</col>
		<col key="yes" def="S50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="S72">TextStyle_</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLocalRadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISLogicalDisk">
		<col key="yes" def="i2">DiskId</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="ISLogicalDiskFeatures">
		<col key="yes" def="i2">ISLogicalDisk_</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col key="yes" def="S38">Feature_</col>
		<col def="i2">Sequence</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISMergeModule">
		<col key="yes" def="s255">ISMergeModule</col>
		<col key="yes" def="i2">Language</col>
		<col def="s255">Name</col>
		<col def="S255">Destination</col>
		<col def="I4">ISAttributes</col>
		<row><td>CodeMeter_Runtime_Merge_Module_Win32.A961A077_4BD0_4C98_86BC_EE4A98CE550D</td><td>0</td><td>CodeMeter Runtime Merge Module (Win32 / x86)</td><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win64.1992E333_D17A_448B_8484_ED047109D182</td><td>0</td><td>CodeMeter Runtime Merge Module (Win64 / x64)</td><td/><td/></row>
		<row><td>WIBU_ShellExtension_Installer.2C5CFD32_85E0_4EDF_BEF3_50462A17B50E</td><td>0</td><td>WIBU ShellExtension Merge Module (Win32 / x86)</td><td/><td/></row>
		<row><td>WIBU_ShellExtension_Installer.573670D2_95A5_4F72_BD6A_63FA56A6342E</td><td>0</td><td>WIBU ShellExtension Merge Module (Win64 / x64)</td><td/><td/></row>
		<row><td>WibuCmNETInstaller.B396C8F90D0349058867D3D7030330FB.B396C8F9_0D03_4905_8867_D3D7030330FB</td><td>0</td><td>Installs the CodeMeter .NET assemblies</td><td/><td/></row>
	</table>

	<table name="ISMergeModuleCfgValues">
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
		<col key="yes" def="s72">ModuleConfiguration_</col>
		<col def="L0">Value</col>
		<col def="i2">Format</col>
		<col def="L255">Type</col>
		<col def="L255">ContextData</col>
		<col def="L255">DefaultValue</col>
		<col def="I2">Attributes</col>
		<col def="L255">DisplayName</col>
		<col def="L255">Description</col>
		<col def="L255">HelpLocation</col>
		<col def="L255">HelpKeyword</col>
		<row><td>CodeMeter_Runtime_Merge_Module_Win32.A961A077_4BD0_4C98_86BC_EE4A98CE550D</td><td>0</td><td>cfgPROP_CHK_ISSDK</td><td>[PROP_CHK_ISSDK]</td><td>0</td><td/><td/><td>[PROP_CHK_ISSDK]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win32.A961A077_4BD0_4C98_86BC_EE4A98CE550D</td><td>0</td><td>cfgPROP_CMCC</td><td>[PROP_CMCC]</td><td>0</td><td/><td/><td>[PROP_CMCC]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win32.A961A077_4BD0_4C98_86BC_EE4A98CE550D</td><td>0</td><td>cfgPROP_MAKESC</td><td>[PROP_MAKESC]</td><td>0</td><td/><td/><td>[PROP_MAKESC]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win64.1992E333_D17A_448B_8484_ED047109D182</td><td>0</td><td>cfgPROP_CHK_ISSDK</td><td>[PROP_CHK_ISSDK]</td><td>0</td><td/><td/><td>[PROP_CHK_ISSDK]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win64.1992E333_D17A_448B_8484_ED047109D182</td><td>0</td><td>cfgPROP_CMCC</td><td>[PROP_CMCC]</td><td>0</td><td/><td/><td>[PROP_CMCC]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>CodeMeter_Runtime_Merge_Module_Win64.1992E333_D17A_448B_8484_ED047109D182</td><td>0</td><td>cfgPROP_MAKESC</td><td>[PROP_MAKESC]</td><td>0</td><td/><td/><td>[PROP_MAKESC]</td><td>0</td><td/><td/><td/><td/></row>
		<row><td>WibuCmNETInstaller.B396C8F90D0349058867D3D7030330FB.B396C8F9_0D03_4905_8867_D3D7030330FB</td><td>0</td><td>_4C4FD354DE0C42E7A28924159F07AF86</td><td>[TARGETDIR]</td><td>0</td><td>Formatted</td><td>_RetargetableFolder</td><td>[TARGETDIR]</td><td>2</td><td>Module Retargetable Folder</td><td/><td/><td/></row>
		<row><td>WibuCmNETInstaller.B396C8F90D0349058867D3D7030330FB.B396C8F9_0D03_4905_8867_D3D7030330FB</td><td>0</td><td>cfgPROP_CHK_DOTNET</td><td>[PROP_CHK_DOTNET]</td><td>0</td><td/><td/><td>[PROP_CHK_DOTNET]</td><td>0</td><td/><td/><td/><td/></row>
	</table>

	<table name="ISObject">
		<col key="yes" def="s50">ObjectName</col>
		<col def="s15">Language</col>
	</table>

	<table name="ISObjectProperty">
		<col key="yes" def="S50">ObjectName</col>
		<col key="yes" def="S50">Property</col>
		<col def="S0">Value</col>
		<col def="I2">IncludeInBuild</col>
	</table>

	<table name="ISPatchConfigImage">
		<col key="yes" def="S72">PatchConfiguration_</col>
		<col key="yes" def="s72">UpgradedImage_</col>
	</table>

	<table name="ISPatchConfiguration">
		<col key="yes" def="s72">Name</col>
		<col def="i2">CanPCDiffer</col>
		<col def="i2">CanPVDiffer</col>
		<col def="i2">IncludeWholeFiles</col>
		<col def="i2">LeaveDecompressed</col>
		<col def="i2">OptimizeForSize</col>
		<col def="i2">EnablePatchCache</col>
		<col def="S0">PatchCacheDir</col>
		<col def="i4">Flags</col>
		<col def="S0">PatchGuidsToReplace</col>
		<col def="s0">TargetProductCodes</col>
		<col def="s50">PatchGuid</col>
		<col def="s0">OutputPath</col>
		<col def="i2">MinMsiVersion</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISPatchConfigurationProperty">
		<col key="yes" def="S72">ISPatchConfiguration_</col>
		<col key="yes" def="S50">Property</col>
		<col def="S50">Value</col>
	</table>

	<table name="ISPatchExternalFile">
		<col key="yes" def="s50">Name</col>
		<col key="yes" def="s13">ISUpgradedImage_</col>
		<col def="s72">FileKey</col>
		<col def="s255">FilePath</col>
	</table>

	<table name="ISPatchWholeFile">
		<col key="yes" def="s50">UpgradedImage</col>
		<col key="yes" def="s72">FileKey</col>
		<col def="S72">Component</col>
	</table>

	<table name="ISPathVariable">
		<col key="yes" def="s72">ISPathVariable</col>
		<col def="S255">Value</col>
		<col def="S255">TestValue</col>
		<col def="i4">Type</col>
		<row><td>CommonFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>ISPROJECTDIR</td><td/><td/><td>1</td></row>
		<row><td>ISProductFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectDataFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectFolder</td><td/><td/><td>1</td></row>
		<row><td>ProgramFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>SystemFolder</td><td/><td/><td>1</td></row>
		<row><td>WindowsFolder</td><td/><td/><td>1</td></row>
	</table>

	<table name="ISProductConfiguration">
		<col key="yes" def="s72">ISProductConfiguration</col>
		<col def="S255">ProductConfigurationFlags</col>
		<col def="I4">GeneratePackageCode</col>
		<row><td>Express</td><td/><td>1</td></row>
	</table>

	<table name="ISProductConfigurationInstance">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="i2">InstanceId</col>
		<col key="yes" def="s72">Property</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISProductConfigurationProperty">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Property</col>
		<col def="L255">Value</col>
	</table>

	<table name="ISRelease">
		<col key="yes" def="s72">ISRelease</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="s255">BuildLocation</col>
		<col def="s255">PackageName</col>
		<col def="i4">Type</col>
		<col def="s0">SupportedLanguagesUI</col>
		<col def="i4">MsiSourceType</col>
		<col def="i4">ReleaseType</col>
		<col def="s72">Platforms</col>
		<col def="S0">SupportedLanguagesData</col>
		<col def="s6">DefaultLanguage</col>
		<col def="i4">SupportedOSs</col>
		<col def="s50">DiskSize</col>
		<col def="i4">DiskSizeUnit</col>
		<col def="i4">DiskClusterSize</col>
		<col def="S0">ReleaseFlags</col>
		<col def="i4">DiskSpanning</col>
		<col def="S255">SynchMsi</col>
		<col def="s255">MediaLocation</col>
		<col def="S255">URLLocation</col>
		<col def="S255">DigitalURL</col>
		<col def="S255">DigitalPVK</col>
		<col def="S255">DigitalSPC</col>
		<col def="S255">Password</col>
		<col def="S255">VersionCopyright</col>
		<col def="i4">Attributes</col>
		<col def="S255">CDBrowser</col>
		<col def="S255">DotNetBuildConfiguration</col>
		<col def="S255">MsiCommandLine</col>
		<col def="I4">ISSetupPrerequisiteLocation</col>
		<row><td>CD_ROM</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>0</td><td>2052</td><td>0</td><td>2</td><td>Intel</td><td/><td>2052</td><td>0</td><td>650</td><td>0</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>Custom</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>2</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>100</td><td>0</td><td>1024</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-10</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>8.75</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-18</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>15.83</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-5</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>2052</td><td>0</td><td>2</td><td>Intel</td><td/><td>2052</td><td>0</td><td>4.38</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-9</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>7.95</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>SingleImage</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>1</td><td>2052</td><td>0</td><td>1</td><td>Intel</td><td/><td>2052</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>108573</td><td/><td/><td/><td>3</td></row>
		<row><td>WebDeployment</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>4</td><td>1033</td><td>2</td><td>1</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>124941</td><td/><td/><td/><td>3</td></row>
	</table>

	<table name="ISReleaseASPublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="S0">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISReleaseExtended">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="I4">WebType</col>
		<col def="S255">WebURL</col>
		<col def="I4">WebCabSize</col>
		<col def="S255">OneClickCabName</col>
		<col def="S255">OneClickHtmlName</col>
		<col def="S255">WebLocalCachePath</col>
		<col def="I4">EngineLocation</col>
		<col def="S255">Win9xMsiUrl</col>
		<col def="S255">WinNTMsiUrl</col>
		<col def="I4">ISEngineLocation</col>
		<col def="S255">ISEngineURL</col>
		<col def="I4">OneClickTargetBrowser</col>
		<col def="S255">DigitalCertificateIdNS</col>
		<col def="S255">DigitalCertificateDBaseNS</col>
		<col def="S255">DigitalCertificatePasswordNS</col>
		<col def="I4">DotNetRedistLocation</col>
		<col def="S255">DotNetRedistURL</col>
		<col def="I4">DotNetVersion</col>
		<col def="S255">DotNetBaseLanguage</col>
		<col def="S0">DotNetLangaugePacks</col>
		<col def="S255">DotNetFxCmdLine</col>
		<col def="S255">DotNetLangPackCmdLine</col>
		<col def="S50">JSharpCmdLine</col>
		<col def="I4">Attributes</col>
		<col def="I4">JSharpRedistLocation</col>
		<col def="I4">MsiEngineVersion</col>
		<col def="S255">WinMsi30Url</col>
		<col def="S255">CertPassword</col>
		<row><td>CD_ROM</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>Custom</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-10</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-18</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-5</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-9</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>SingleImage</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>1</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>WebDeployment</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>setup</td><td>Default</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>2</td><td>http://www.Installengine.com/Msiengine20</td><td>http://www.Installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
	</table>

	<table name="ISReleaseProperty">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s0">Value</col>
	</table>

	<table name="ISReleasePublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="S255">Repository</col>
		<col def="S255">DisplayName</col>
		<col def="S255">Publisher</col>
		<col def="S255">Description</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISSQLConnection">
		<col key="yes" def="s72">ISSQLConnection</col>
		<col def="s255">Server</col>
		<col def="s255">Database</col>
		<col def="s255">UserName</col>
		<col def="s255">Password</col>
		<col def="s255">Authentication</col>
		<col def="i2">Attributes</col>
		<col def="i2">Order</col>
		<col def="S0">Comments</col>
		<col def="I4">CmdTimeout</col>
		<col def="S0">BatchSeparator</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
	</table>

	<table name="ISSQLConnectionDBServer">
		<col key="yes" def="s72">ISSQLConnectionDBServer</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLDBMetaData_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLConnectionScript">
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLDBMetaData">
		<col key="yes" def="s72">ISSQLDBMetaData</col>
		<col def="S0">DisplayName</col>
		<col def="S0">AdoDriverName</col>
		<col def="S0">AdoCxnDriver</col>
		<col def="S0">AdoCxnServer</col>
		<col def="S0">AdoCxnDatabase</col>
		<col def="S0">AdoCxnUserID</col>
		<col def="S0">AdoCxnPassword</col>
		<col def="S0">AdoCxnWindowsSecurity</col>
		<col def="S0">AdoCxnNetLibrary</col>
		<col def="S0">TestDatabaseCmd</col>
		<col def="S0">TestTableCmd</col>
		<col def="S0">VersionInfoCmd</col>
		<col def="S0">VersionBeginToken</col>
		<col def="S0">VersionEndToken</col>
		<col def="S0">LocalInstanceNames</col>
		<col def="S0">CreateDbCmd</col>
		<col def="S0">SwitchDbCmd</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">TestTableCmd2</col>
		<col def="S0">WinAuthentUserId</col>
		<col def="S0">DsnODBCName</col>
		<col def="S0">AdoCxnPort</col>
		<col def="S0">AdoCxnAdditional</col>
		<col def="S0">QueryDatabasesCmd</col>
		<col def="S0">CreateTableCmd</col>
		<col def="S0">InsertRecordCmd</col>
		<col def="S0">SelectTableCmd</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
		<col def="S0">ScriptVersion_ColumnType</col>
	</table>

	<table name="ISSQLRequirement">
		<col key="yes" def="s72">ISSQLRequirement</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col def="S15">MajorVersion</col>
		<col def="S25">ServicePackLevel</col>
		<col def="i4">Attributes</col>
		<col def="S72">ISSQLConnectionDBServer_</col>
	</table>

	<table name="ISSQLScriptError">
		<col key="yes" def="i4">ErrNumber</col>
		<col key="yes" def="S72">ISSQLScriptFile_</col>
		<col def="i2">ErrHandling</col>
		<col def="L255">Message</col>
		<col def="i2">Attributes</col>
	</table>

	<table name="ISSQLScriptFile">
		<col key="yes" def="s72">ISSQLScriptFile</col>
		<col def="s72">Component_</col>
		<col def="i2">Scheduling</col>
		<col def="L255">InstallText</col>
		<col def="L255">UninstallText</col>
		<col def="S0">ISBuildSourcePath</col>
		<col def="S0">Comments</col>
		<col def="i2">ErrorHandling</col>
		<col def="i2">Attributes</col>
		<col def="S255">Version</col>
		<col def="S255">Condition</col>
		<col def="S0">DisplayName</col>
	</table>

	<table name="ISSQLScriptImport">
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S255">Server</col>
		<col def="S255">Database</col>
		<col def="S255">UserName</col>
		<col def="S255">Password</col>
		<col def="i4">Authentication</col>
		<col def="S0">IncludeTables</col>
		<col def="S0">ExcludeTables</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISSQLScriptReplace">
		<col key="yes" def="s72">ISSQLScriptReplace</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S0">Search</col>
		<col def="S0">Replace</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISScriptFile">
		<col key="yes" def="s255">ISScriptFile</col>
	</table>

	<table name="ISSelfReg">
		<col key="yes" def="s72">FileKey</col>
		<col def="I2">Cost</col>
		<col def="I2">Order</col>
		<col def="S50">CmdLine</col>
	</table>

	<table name="ISSetupFile">
		<col key="yes" def="s72">ISSetupFile</col>
		<col def="S255">FileName</col>
		<col def="V0">Stream</col>
		<col def="S50">Language</col>
		<col def="I2">Splash</col>
		<col def="S0">Path</col>
	</table>

	<table name="ISSetupPrerequisites">
		<col key="yes" def="s72">ISSetupPrerequisites</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">Order</col>
		<col def="I2">ISSetupLocation</col>
		<col def="S255">ISReleaseFlags</col>
		<row><td>_6EBFB585_09EF_4539_9E08_93CBC1FE8FA7_</td><td>Microsoft .NET Framework 4.5.2 Full.prq</td><td/><td>0</td><td/></row>
	</table>

	<table name="ISSetupType">
		<col key="yes" def="s38">ISSetupType</col>
		<col def="L255">Description</col>
		<col def="L255">Display_Name</col>
		<col def="i2">Display</col>
		<col def="S255">Comments</col>
		<row><td>Custom</td><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Custom##</td><td>3</td><td/></row>
		<row><td>Minimal</td><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Minimal##</td><td>2</td><td/></row>
		<row><td>Typical</td><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td>1</td><td/></row>
	</table>

	<table name="ISSetupTypeFeatures">
		<col key="yes" def="s38">ISSetupType_</col>
		<col key="yes" def="s38">Feature_</col>
		<row><td>Custom</td><td>AlwaysInstall</td></row>
		<row><td>Minimal</td><td>AlwaysInstall</td></row>
		<row><td>Typical</td><td>AlwaysInstall</td></row>
	</table>

	<table name="ISStorages">
		<col key="yes" def="s72">Name</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISString">
		<col key="yes" def="s255">ISString</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="S0">Value</col>
		<col def="I2">Encoded</col>
		<col def="S0">Comment</col>
		<col def="I4">TimeStamp</col>
		<row><td>COMPANY_NAME</td><td>2052</td><td>天宇宁达</td><td>0</td><td/><td>-64666382</td></row>
		<row><td>DN_AlwaysInstall</td><td>2052</td><td>始终安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_COLOR</td><td>2052</td><td>系统颜色设置不足以运行 [ProductName]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_DOTNETVERSION45FULL</td><td>2052</td><td>Microsoft .NET Framework 4.5 Full package or greater needs to be installed for this installation to continue.</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_OS</td><td>2052</td><td>操作系统不足以运行 [ProductName]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_PROCESSOR</td><td>2052</td><td>处理器不足以运行 [ProductName]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_RAM</td><td>2052</td><td>RAM 量不足以运行 [ProductName]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_SCREEN</td><td>2052</td><td>屏幕分辨率不足以运行 [ProductName] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT</td><td>2052</td><td>压缩</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT_DESC</td><td>2052</td><td>压缩说明</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE</td><td>2052</td><td>完整安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE_DESC</td><td>2052</td><td>完整安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM</td><td>2052</td><td>自定义</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC</td><td>2052</td><td>自定义说明</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC_PRO</td><td>2052</td><td>自定义</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL</td><td>2052</td><td>典型</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL_DESC</td><td>2052</td><td>典型说明</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_1</td><td>2052</td><td>[1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_1b</td><td>2052</td><td>[1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_1c</td><td>2052</td><td>[1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_1d</td><td>2052</td><td>[1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Advertising</td><td>2052</td><td>通知应用程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_AllocatingRegistry</td><td>2052</td><td>正在分配注册表空间</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_AppCommandLine</td><td>2052</td><td>应用程序: [1]，命令行: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_AppId</td><td>2052</td><td>AppId: [1]{{, AppType: [2]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_AppIdAppTypeRSN</td><td>2052</td><td>AppId: [1]{{, AppType: [2], Users: [3], RSN: [4]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Application</td><td>2052</td><td>应用程序: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_BindingExes</td><td>2052</td><td>绑定可执行文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ClassId</td><td>2052</td><td>Class Id: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ClsID</td><td>2052</td><td>Class Id: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIDQualifier</td><td>2052</td><td>组件 ID: [1]，资格认证者: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIdQualifier2</td><td>2052</td><td>组件 ID: [1]，资格认证者: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace</td><td>2052</td><td>正在计算空间需求</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace2</td><td>2052</td><td>正在计算空间需求</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace3</td><td>2052</td><td>正在计算空间需求</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension</td><td>2052</td><td>MIME 内容类型: [1]，扩展: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension2</td><td>2052</td><td>MIME 内容类型: [1]，扩展: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNetworkFiles</td><td>2052</td><td>正在复制网络安装文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNewFiles</td><td>2052</td><td>正在复制新文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingDuplicate</td><td>2052</td><td>正在创建重复文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingFolders</td><td>2052</td><td>正在创建文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingIISRoots</td><td>2052</td><td>正在创建 IIS 虚拟根目录...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingShortcuts</td><td>2052</td><td>正在创建快捷方式</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_DeletingServices</td><td>2052</td><td>正在删除服务</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_EnvironmentStrings</td><td>2052</td><td>正在更新环境字符串</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_EvaluateLaunchConditions</td><td>2052</td><td>正在评估启动条件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Extension</td><td>2052</td><td>扩展: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Extension2</td><td>2052</td><td>扩展: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Feature</td><td>2052</td><td>功能: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FeatureColon</td><td>2052</td><td>功能: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_File</td><td>2052</td><td>文件: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_File2</td><td>2052</td><td>文件: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDependencies</td><td>2052</td><td>文件：[1],  依存： [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir</td><td>2052</td><td>文件: [1]，目录: [9]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir2</td><td>2052</td><td>File: [1], Directory: [9]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir3</td><td>2052</td><td>文件: [1]，目录: [9]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize</td><td>2052</td><td>文件: [1]，目录: [9]，大小: [6]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize2</td><td>2052</td><td>File: [1],  Directory: [9],  Size: [6]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize3</td><td>2052</td><td>文件: [1]，目录: [9]，大小: [6]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize4</td><td>2052</td><td>文件: [1]，目录: [2]，大小: [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirectorySize</td><td>2052</td><td>文件: [1]，目录: [9]，大小: [6]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder</td><td>2052</td><td>文件: [1]，文件夹: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder2</td><td>2052</td><td>文件: [1]，文件夹: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue</td><td>2052</td><td>文件: [1]，节: [2]，键: [3]，数值: [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue2</td><td>2052</td><td>文件: [1]，节: [2]，键: [3]，数值: [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Folder</td><td>2052</td><td>文件夹: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Folder1</td><td>2052</td><td>文件夹: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Font</td><td>2052</td><td>字体: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Font2</td><td>2052</td><td>字体: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FoundApp</td><td>2052</td><td>找到应用程序: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_FreeSpace</td><td>2052</td><td>自由空间: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_GeneratingScript</td><td>2052</td><td>正在生成脚本操作，用于：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsCost</td><td>2052</td><td>正在搜集对象的许可信息...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsInstall</td><td>2052</td><td>正在应用对象的许可信息...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_InitializeODBCDirs</td><td>2052</td><td>正在初始化 ODBC 目录</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_InstallODBC</td><td>2052</td><td>正在安装 ODBC 组件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_InstallServices</td><td>2052</td><td>正在安装新服务</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_InstallingSystemCatalog</td><td>2052</td><td>安装系统目录</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_KeyName</td><td>2052</td><td>键值: [1]，名称: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_KeyNameValue</td><td>2052</td><td>键: [1]，名称: [2]，数值: [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_LibId</td><td>2052</td><td>LibID: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Libid2</td><td>2052</td><td>LibID: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_MigratingFeatureStates</td><td>2052</td><td>正在从相关应用程序迁移功能</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_MovingFiles</td><td>2052</td><td>正在移动文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction</td><td>2052</td><td>名称: [1]，数值: [2]，动作 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction2</td><td>2052</td><td>名称: [1]，数值: [2]，动作 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_PatchingFiles</td><td>2052</td><td>正在修补文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID</td><td>2052</td><td>ProgId: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID2</td><td>2052</td><td>ProgId: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_PropertySignature</td><td>2052</td><td>属性: [1]，签名: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductFeatures</td><td>2052</td><td>正在发布产品功能</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductInfo</td><td>2052</td><td>正在发布产品信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_PublishingQualifiedComponents</td><td>2052</td><td>正在发布合格的组件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegUser</td><td>2052</td><td>正在注册用户</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterClassServer</td><td>2052</td><td>正在注册类服务器</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterExtensionServers</td><td>2052</td><td>正在注册扩展服务器</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterFonts</td><td>2052</td><td>正在注册字体</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterMimeInfo</td><td>2052</td><td>正在注册 MIME 信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterTypeLibs</td><td>2052</td><td>正在注册类型库</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringComPlus</td><td>2052</td><td>正在注册 COM+ 应用程序和组件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringModules</td><td>2052</td><td>正在注册模块</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProduct</td><td>2052</td><td>正在注册产品</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProgIdentifiers</td><td>2052</td><td>正在撤消程序标识符的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemoveApps</td><td>2052</td><td>正在删除应用程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingBackup</td><td>2052</td><td>正在删除备份文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingDuplicates</td><td>2052</td><td>正在删除重复的文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFiles</td><td>2052</td><td>正在删除文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFolders</td><td>2052</td><td>正在删除文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIISRoots</td><td>2052</td><td>正在删除 IIS 虚拟根目录...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIni</td><td>2052</td><td>正在删除 INI 文件条目</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingMoved</td><td>2052</td><td>正在删除移动过的文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingODBC</td><td>2052</td><td>正在删除 ODBC 组件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingRegistry</td><td>2052</td><td>正在删除系统注册表值</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingShortcuts</td><td>2052</td><td>正在删除快捷方式</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_RollingBack</td><td>2052</td><td>回滚操作: </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_SearchForRelated</td><td>2052</td><td>正在搜索相关产品</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_SearchInstalled</td><td>2052</td><td>正在搜索已安装的应用程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts</td><td>2052</td><td>正在搜索符合资格的产品</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts2</td><td>2052</td><td>正在搜索符合资格的产品</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Service</td><td>2052</td><td>服务: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Service2</td><td>2052</td><td>服务: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Service3</td><td>2052</td><td>服务: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Service4</td><td>2052</td><td>服务: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut</td><td>2052</td><td>快捷方式: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut1</td><td>2052</td><td>快捷方式: [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_StartingServices</td><td>2052</td><td>正在启动服务</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_StoppingServices</td><td>2052</td><td>正在停止服务</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishProductFeatures</td><td>2052</td><td>正在取消产品功能的发布</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishQualified</td><td>2052</td><td>正在取消合格组件的发布</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishingProductInfo</td><td>2052</td><td>正在取消产品信息的发布</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregTypeLibs</td><td>2052</td><td>正在撤消类型库的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterClassServers</td><td>2052</td><td>正在撤消类服务器的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterExtensionServers</td><td>2052</td><td>正在撤消扩展服务器的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterModules</td><td>2052</td><td>正在撤消模块的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringComPlus</td><td>2052</td><td>正在撤消 COM+ 应用程序和组件的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringFonts</td><td>2052</td><td>正在撤消字体的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringMimeInfo</td><td>2052</td><td>正在撤消 MIME 信息的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringProgramIds</td><td>2052</td><td>正在撤消程序标识符的注册</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateComponentRegistration</td><td>2052</td><td>正在更新组件注册表</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateEnvironmentStrings</td><td>2052</td><td>正在更新环境字符串</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_Validating</td><td>2052</td><td>正在验证安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_WritingINI</td><td>2052</td><td>正在写入 INI 文件数值</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ACTIONTEXT_WritingRegistry</td><td>2052</td><td>正在写入系统注册表值</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_BACK</td><td>2052</td><td>&lt; 上一步(&amp;B)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_CANCEL</td><td>2052</td><td>取消</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_CANCEL2</td><td>2052</td><td>{&amp;Tahoma8}取消(&amp;C)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_CHANGE</td><td>2052</td><td>更改(&amp;C)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_COST</td><td>2052</td><td>正在计算 COM+ 应用程序成本： [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>2052</td><td>正在安装 COM+ 应用程序： [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>2052</td><td>正在卸载 COM+ 应用程序： [1]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIALOG_TEXT2_DESCRIPTION</td><td>2052</td><td>对话框一般描述</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_EXTERIOR</td><td>2052</td><td>{&amp;TahomaBold10}对话框粗体标题</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_INTERIOR</td><td>2052</td><td>{&amp;MSSansBold8}对话框粗体标题</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIFX_AMD64</td><td>2052</td><td>[ProductName] 需要 X64 处理器。单击确定以退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIFX_IA64</td><td>2052</td><td>[ProductName] 需要 IA64 处理器。单击确定以退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DIFX_X86</td><td>2052</td><td>[ProductName] 需要 X86 处理器。单击确定以退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_DatabaseFolder_InstallDatabaseTo</td><td>2052</td><td>将 [ProductName] 数据库安装到：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_0</td><td>2052</td><td>{{致命错误: }}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1</td><td>2052</td><td>错误 [1]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_10</td><td>2052</td><td>=== 记录开始: [Date]  [Time] ===</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_100</td><td>2052</td><td>无法删除快捷方式 [2]。请确认该快捷方式文件存在，并且您可以访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_101</td><td>2052</td><td>无法将文件 [2] 注册到类型库中。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_102</td><td>2052</td><td>无法撤消文件 [2] 在类型库中的注册。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_103</td><td>2052</td><td>无法更新 INI 文件 [2][3]。请确认该文件存在并且您可以访问它。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_104</td><td>2052</td><td>无法安排在重新启动时用文件 [2] 替换文件 [3]。请确认您拥有对文件 [3] 的写权限。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_105</td><td>2052</td><td>删除 ODBC 驱动程序管理器时发生错误，ODBC 错误 [2]: [3]。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_106</td><td>2052</td><td>安装 ODBC 驱动程序管理器时发生错误，ODBC 错误 [2]: [3]。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_107</td><td>2052</td><td>删除 ODBC 驱动程序 [4] 时发生错误，ODBC 错误 [2]: [3]。请确认您有足够的权限删除 ODBC 驱动程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_108</td><td>2052</td><td>安装 ODBC 驱动程序 [4] 时发生错误，ODBC 错误 [2]: [3]。请确认文件 [4] 存在，并且您可以访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_109</td><td>2052</td><td>配置 ODBC 数据源 [4] 时发生错误，ODBC 错误 [2]: [3]。请确认文件 [4] 存在，并且您可以访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_11</td><td>2052</td><td>=== 记录停止: [Date]  [Time] ===</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_110</td><td>2052</td><td>服务 [2]（[3]）的启动失败。请确认您有足够的权限启动系统服务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_111</td><td>2052</td><td>无法终止服务 [2]（[3]）。请确认您有足够的权限终止系统服务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_112</td><td>2052</td><td>无法删除服务 [2]（[3]）。请确认您有足够的权限删除系统服务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_113</td><td>2052</td><td>无法安装服务 [2]（[3]）。请确认您有足够的权限安装系统服务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_114</td><td>2052</td><td>无法更新环境变量 [2]。请确认您有足够的权限修改环境变量。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_115</td><td>2052</td><td>您没有足够的权限为该计算机所有用户完成此安装。请以管理员的身份登录，然后重新尝试进行此安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_116</td><td>2052</td><td>无法对文件 [3] 的安全权限进行设置。错误: [2]。请确认您有足够的权限修改此文件的安全权限。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_117</td><td>2052</td><td>此计算机上未安装 Component Services (COM+ 1.0)。要成功完成安装需要 Component Services。Component Services 在 Windows 2000 上有效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_118</td><td>2052</td><td>注册 COM+ 应用程序时出错。详细信息，请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_119</td><td>2052</td><td>撤消注册 COM+ 应用程序时出错。详细信息，请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_12</td><td>2052</td><td>操作开始 [Time]: [1]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_120</td><td>2052</td><td>正在删除此应用程序旧的版本...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_121</td><td>2052</td><td>正在准备删除此应用程序旧的版本...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_122</td><td>2052</td><td>对文件 [2] 应用修补程序时出错。该文件可能被更新过，所以本修补程序不能修改它。详细信息，请与您的修补程序供应商联系。 {{系统错误: [3]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_123</td><td>2052</td><td>[2] 不能安装某一所需产品。请与您的技术支持人员联系。 {{系统错误: [3]。}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_124</td><td>2052</td><td>不能删除旧版本的 [2]。请与您的技术支持人员联系。 {{系统错误: [3]。}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_125</td><td>2052</td><td>服务 [2] ([3]) 的描述不能更改。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_126</td><td>2052</td><td>Windows Installer 服务不能更新系统文件 [2]，因为该文件被 Windows 保护。为使该程序正确工作，您可能需要更新操作系统。{{程序包版本: [3]，操作系统保护版本: [4]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_127</td><td>2052</td><td>Windows Installer 服务不能更新被保护的 Windows 文件 [2]。{{程序包版本: [3]，操作系统保护版本: [4]，SFP 错误: [5]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_128</td><td>2052</td><td>Windows Installer 服务无法更新一个或多个受保护的 Windows 文件。SFP 错误：[2]. 受保护文件列表：[3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_129</td><td>2052</td><td>计算机上的策略禁止用户安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_13</td><td>2052</td><td>操作结束 [Time]: [1]。返回值 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_130</td><td>2052</td><td>安装须使用互联网信息服务器，用以配置 IIS Virtual Roots。请检查是否已安装 IIS。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_131</td><td>2052</td><td>此安装程序要求管理员权限，才能配置 IIS 虚拟根目录。IDS_ERROR_13</td><td>0</td><td>操作结束 [Time]: [1]。返回值 [2]。	</td><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1329</td><td>2052</td><td>无法安装需要的文件，因为 CAB 文件 [2] 未经过数字签名。可能表明 CAB 文件已损坏。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1330</td><td>2052</td><td>无法安装需要的文件，因为 CAB 文件 [2] 的数字签名无效。可能表明 CAB 文件已损坏。{WinVerifyTrust 返回了错误 [3]。}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1331</td><td>2052</td><td>无法正确地复制 [2] 文件:CRC 错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1332</td><td>2052</td><td>无法正确地修补 [2] 文件:CRC 错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1333</td><td>2052</td><td>无法正确地修补 [2] 文件:CRC 错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1334</td><td>2052</td><td>无法安装文件 '[2]'，因为无法在 CAB 文件 '[3]' 中找到此文件。可能表明网络错误、读 CD-ROM 错误或此软件包有错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1335</td><td>2052</td><td>此安装所需的 CAB 文件 '[2]' 已损坏，且无法使用。可能表明网络错误、读 CD-ROM 错误或此软件包有错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1336</td><td>2052</td><td>创建完成此安装所需的临时文件时出错。文件夹:[3]。系统错误代码: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_14</td><td>2052</td><td>剩余时间: {[1] 分 }{[2] 秒}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_15</td><td>2052</td><td>内存不足。请先关闭其他应用程序，然后再重试。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_16</td><td>2052</td><td>安装程序已不再反应。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1609</td><td>2052</td><td>应用安全设置时出错。[2] 不是有效的用户或组。可能是软件包存在问题，或者连接到网络上的域控制器时出现问题。检查网络连接，然后单击重试或取消以结束安装。无法定位用户的 SID，系统错误 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1651</td><td>2052</td><td>管理用户无法对每用户管理的或每机器的处于广告状态的应用程序应用修补程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_17</td><td>2052</td><td>安装程序过早停止。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1715</td><td>2052</td><td>已安装 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1716</td><td>2052</td><td>已配置 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1717</td><td>2052</td><td>删除 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1718</td><td>2052</td><td>文件 [2] 被数字签名策略拒绝。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1719</td><td>2052</td><td>无法访问 Windows Installer 服务。请与支持人员联系，验证该服务是否已正确注册并启用。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1720</td><td>2052</td><td>Windows Installer 软件包存在问题。无法运行完成此安装所需的脚本。请与您的支持人员或软件包供应商联系。自定义操作 [2] 脚本错误 [3]，[4]:[5] 第 [6] 行，第 [7] 列，[8]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1721</td><td>2052</td><td>Windows Installer 软件包存在问题。无法运行完成此安装所需的程序。请与您的支持人员或软件包供应商联系。操作:[2]，位置:[3]，命令: [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1722</td><td>2052</td><td>Windows Installer 软件包存在问题。作为安装一部分的程序没有按预期完成。请与您的支持人员或软件包供应商联系。操作 [2]，位置:[3]，命令: [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1723</td><td>2052</td><td>Windows Installer 软件包存在问题。无法运行完成此安装所需的 DLL。请与您的支持人员或软件包供应商联系。操作 [2]，条目:[3]，库: [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1724</td><td>2052</td><td>成功地完成了删除。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1725</td><td>2052</td><td>删除失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1726</td><td>2052</td><td>成功地完成了广告。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1727</td><td>2052</td><td>广告失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1728</td><td>2052</td><td>成功地完成了配置。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1729</td><td>2052</td><td>配置失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1730</td><td>2052</td><td>只有管理员才可以删除该应用程序。要删除此应用程序，您可用管理员身份登录，或与您的技术支持小组联系以获得帮助。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1731</td><td>2052</td><td>产品 [2] 的源安装包与客户包不同步。使用安装包 '[3]' 的有效副本尝试重新安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1732</td><td>2052</td><td>为完成 [2] 的安装，必须重新启动计算机。当前有其他用户已登录到该计算机，因此重新启动可能使他们失去其工作。是否立即重新启动？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_18</td><td>2052</td><td>Windows 正在配置 [ProductName]，请稍等。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_19</td><td>2052</td><td>正在收集所需信息...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1935</td><td>2052</td><td>安装程序集组件 [2] 时出错。HRESULT:[3]。{{程序集界面:[4], 函数:[5]。{{程序集名称: [6]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1936</td><td>2052</td><td>安装程序集 '[6]' 时出错。此程序集没有命名不够安全或密钥长度未达到最低限制。HRESULT:[3]。{{程序集界面:[4], 函数:[5], 组件: [2]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1937</td><td>2052</td><td>安装程序集 '[6]' 时出错。无法验证签名或编录，或签名或编录无效。HRESULT:[3]。{{程序集界面:[4], 函数:[5], 组件: [2]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_1938</td><td>2052</td><td>安装程序集 '[6]' 时出错。无法找到程序集的一个或多个模块。HRESULT:[3]。{{程序集界面:[4], 函数:[5], 组件: [2]}}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2</td><td>2052</td><td>警告 [1]。 </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_20</td><td>2052</td><td>{[ProductName] }的安装已成功完成。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_21</td><td>2052</td><td>{[ProductName] }安装失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2101</td><td>2052</td><td>操作系统不支持快捷方式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2102</td><td>2052</td><td>无效的 .ini 操作: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2103</td><td>2052</td><td>无法解析 shell 文件夹 [2] 的路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2104</td><td>2052</td><td>写入 ini 文件:[3]: 系统错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2105</td><td>2052</td><td>创建快捷方式 [3] 失败。系统错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2106</td><td>2052</td><td>删除快捷方式 [3] 失败。系统错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2107</td><td>2052</td><td>错误 [3] 注册类型库 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2108</td><td>2052</td><td>错误 [3] 未注册类型库 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2109</td><td>2052</td><td>.ini 操作缺少区段。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2110</td><td>2052</td><td>.ini 操作缺少关键字。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2111</td><td>2052</td><td>检测运行的应用程序失败，无法获取性能数据。返回注册操作: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2112</td><td>2052</td><td>检测运行的应用程序失败，无法获取性能指数。返回注册操作: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2113</td><td>2052</td><td>检测运行的应用程序失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_22</td><td>2052</td><td>读取文件 [2] 时出错。{{ 系统错误 [3]。}}  请确认该文件的确存在并且您可以对其进行访问。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2200</td><td>2052</td><td>数据库:[2]。创建数据库对象失败，模式 = [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2201</td><td>2052</td><td>数据库:[2]。初始化失败，内存不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2202</td><td>2052</td><td>数据库:[2]。数据访问失败，内存不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2203</td><td>2052</td><td>数据库:[2]。无法打开数据库文件。系统错误 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2204</td><td>2052</td><td>数据库:[2]。表已存在: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2205</td><td>2052</td><td>数据库:[2]。表不存在: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2206</td><td>2052</td><td>数据库:[2]。不能丢弃表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2207</td><td>2052</td><td>数据库:[2]。意向冲突。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2208</td><td>2052</td><td>数据库:[2]。执行所需参数不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2209</td><td>2052</td><td>数据库:[2]。光标处于无效状态。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2210</td><td>2052</td><td>数据库:[2]。列 [3] 中的无效更新数据类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2211</td><td>2052</td><td>数据库:[2]。无法创建数据库表 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2212</td><td>2052</td><td>数据库:[2]。数据库不在可写入状态。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2213</td><td>2052</td><td>数据库:[2]。保存数据库表时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2214</td><td>2052</td><td>数据库:[2]。写入导出文件时出错: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2215</td><td>2052</td><td>数据库:[2]。无法打开导入文件: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2216</td><td>2052</td><td>数据库:[2]。导入文件格式错误:[3]，第 [4] 行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2217</td><td>2052</td><td>数据库:[2]。到 CreateOutputDatabase [3] 的错误状态。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2218</td><td>2052</td><td>数据库:[2]。未提供表名称。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2219</td><td>2052</td><td>数据库:[2]。无效的 Installer 数据库格式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2220</td><td>2052</td><td>数据库:[2]。无效行/字段数据。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2221</td><td>2052</td><td>数据库:[2]。导入文件中的代码页冲突: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2222</td><td>2052</td><td>数据库:[2]。转换或合并代码页 [3] 与数据库代码页 [4] 不同。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2223</td><td>2052</td><td>数据库:[2]。数据库均相同。未生成转换。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2224</td><td>2052</td><td>数据库:[2]。GenerateTransform:数据库已损坏。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2225</td><td>2052</td><td>数据库:[2]。转换:无法转换临时表。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2226</td><td>2052</td><td>数据库:[2]。转换失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2227</td><td>2052</td><td>数据库:[2]。SQL 查询中的无效标识符 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2228</td><td>2052</td><td>数据库:[2]。SQL 查询中的未知表 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2229</td><td>2052</td><td>数据库:[2]。无法在 SQL 查询中加载表 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2230</td><td>2052</td><td>数据库:[2]。SQL 查询中的重复表 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2231</td><td>2052</td><td>数据库:[2]。SQL 查询中缺少 ')': [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2232</td><td>2052</td><td>数据库:[2]。SQL 查询中的意外标记 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2233</td><td>2052</td><td>数据库:[2]。SQL 查询的 SELECT 子句中没有列: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2234</td><td>2052</td><td>数据库:[2]。SQL 查询的 ORDER BY 子句中没有列: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2235</td><td>2052</td><td>数据库:[2]。SQL 查询中列 '[3]' 不存在或不明确: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2236</td><td>2052</td><td>数据库:[2]。SQL 查询中的无效操作符 '[3]': [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2237</td><td>2052</td><td>数据库:[2]。无效或缺少查询字符串: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2238</td><td>2052</td><td>数据库:[2]。SQL 查询中缺少 FROM 子句: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2239</td><td>2052</td><td>数据库:[2]。INSERT SQL 语句中值不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2240</td><td>2052</td><td>数据库:[2]。UPDATE SQL 语句中缺少更新列。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2241</td><td>2052</td><td>数据库:[2]。INSERT SQL 语句中缺少插入列。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2242</td><td>2052</td><td>数据库:[2]。列 '[3]' 重复。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2243</td><td>2052</td><td>数据库:[2]。未定义主列以创建表。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2244</td><td>2052</td><td>数据库:[2]。SQL 查询 [4] 中的无效类型说明符 '[3]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2245</td><td>2052</td><td>IStorage::Stat 失败，错误 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2246</td><td>2052</td><td>数据库:[2]。无效的 Installer 转换格式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2247</td><td>2052</td><td>数据库:[2] 变换流读取/写入失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2248</td><td>2052</td><td>数据库:[2] GenerateTransform/Merge:基表中的列类型与引用表不匹配。表:[3] 列: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2249</td><td>2052</td><td>数据库:[2] GenerateTransform:基表中的列比引用表中的列多。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2250</td><td>2052</td><td>数据库:[2] 转换:无法添加现有行。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2251</td><td>2052</td><td>数据库:[2] 转换:无法删除不存在的行。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2252</td><td>2052</td><td>数据库:[2] 转换:无法添加现有表。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2253</td><td>2052</td><td>数据库:[2] 转换:无法删除不存在的表。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2254</td><td>2052</td><td>数据库:[2] 转换:无法更新不存在的行。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2255</td><td>2052</td><td>数据库:[2] 转换:已存在具有该名称的列。表:[3] 列: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2256</td><td>2052</td><td>数据库:[2] GenerateTransform/Merge:基表中的主键数与引用表不匹配。表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2257</td><td>2052</td><td>数据库:[2]。试图修改只读表: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2258</td><td>2052</td><td>数据库:[2]。参数类型不匹配: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2259</td><td>2052</td><td>数据库:[2] 表更新失败</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2260</td><td>2052</td><td>存储 CopyTo 失败。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2261</td><td>2052</td><td>无法删除流 [2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2262</td><td>2052</td><td>流不存在:[2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2263</td><td>2052</td><td>无法打开流 [2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2264</td><td>2052</td><td>无法删除流 [2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2265</td><td>2052</td><td>无法提交存储。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2266</td><td>2052</td><td>无法回滚存储。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2267</td><td>2052</td><td>无法删除存储 [2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2268</td><td>2052</td><td>数据库:[2]。Merge:在 [3] 个表中报告存在合并冲突。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2269</td><td>2052</td><td>数据库:[2]。Merge:两个数据库的 '[3]' 表中的列计数不同。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2270</td><td>2052</td><td>数据库:[2]。GenerateTransform/Merge:基表中的列名称与引用表不匹配。表:[3] 列: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2271</td><td>2052</td><td>转换的 SummaryInformation 写入失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2272</td><td>2052</td><td>数据库:[2]。MergeDatabase 将不会写入任何更改，因为数据库是以只读方式打开的。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2273</td><td>2052</td><td>数据库:[2]。MergeDatabase:将对基数据库的引用作为引用数据库传递。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2274</td><td>2052</td><td>数据库:[2]。MergeDatabase:无法将错误写入错误表。可能是预定义的错误表中某个不可为空的列造成的。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2275</td><td>2052</td><td>数据库:[2]。对于表联接，指定的修改 [3] 操作无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2276</td><td>2052</td><td>数据库:[2]。系统不支持代码页 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2277</td><td>2052</td><td>数据库:[2]。无法保存表 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2278</td><td>2052</td><td>数据库:[2]。超出 SQL 查询中 WHERE 子句 32 个表达式限制: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2279</td><td>2052</td><td>数据库:[2] 转换:基表 [3] 中列太多。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2280</td><td>2052</td><td>数据库:[2]。无法为表 [4] 创建列 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2281</td><td>2052</td><td>无法重命名流 [2]。系统错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2282</td><td>2052</td><td>流名称无效 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_23</td><td>2052</td><td>无法创建文件 [2]。同名目录已存在。请取消此次安装，然后安装到其他位置。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2302</td><td>2052</td><td>修补程序通知:目前为止修补了 [2] 个字节。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2303</td><td>2052</td><td>获取卷信息时出错。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2304</td><td>2052</td><td>获取可用磁盘空间时出错。GetLastError:[2]。卷: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2305</td><td>2052</td><td>等待修补线程时出错。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2306</td><td>2052</td><td>无法为修补应用程序创建线程。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2307</td><td>2052</td><td>源文件键名为空。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2308</td><td>2052</td><td>目标文件名为空。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2309</td><td>2052</td><td>在修补正在进行的同时，尝试修补文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2310</td><td>2052</td><td>在没有修补程序运行时尝试继续修补。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2315</td><td>2052</td><td>缺少路径分隔符: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2318</td><td>2052</td><td>文件不存在: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2319</td><td>2052</td><td>设置文件属性时出错:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2320</td><td>2052</td><td>文件不可写: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2321</td><td>2052</td><td>创建文件时出错: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2322</td><td>2052</td><td>用户已取消。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2323</td><td>2052</td><td>无效的文件属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2324</td><td>2052</td><td>无法打开文件:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2325</td><td>2052</td><td>无法获取文件的文件时间:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2326</td><td>2052</td><td>FileToDosDateTime 错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2327</td><td>2052</td><td>无法删除目录:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2328</td><td>2052</td><td>获取文件的文件版本信息时出错: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2329</td><td>2052</td><td>删除文件时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2330</td><td>2052</td><td>获取文件属性时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2331</td><td>2052</td><td>加载库 [2] 或查找入口点 [3] 时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2332</td><td>2052</td><td>获取文件属性时出错。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2333</td><td>2052</td><td>设置文件属性时出错。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2334</td><td>2052</td><td>将文件的文件时间转换为当地时间时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2335</td><td>2052</td><td>路径:[2] 不是 [3] 的父项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2336</td><td>2052</td><td>在路径上创建临时文件时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2337</td><td>2052</td><td>无法关闭文件:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2338</td><td>2052</td><td>无法为文件更新资源:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2339</td><td>2052</td><td>无法为文件设置文件时间:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2340</td><td>2052</td><td>无法为文件更新资源:[3]，资源丢失。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2341</td><td>2052</td><td>无法为文件更新资源:[3]，资源太大。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2342</td><td>2052</td><td>无法为文件更新资源:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2343</td><td>2052</td><td>指定的路径为空。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2344</td><td>2052</td><td>无法找到所需的文件 IMAGEHLP.DLL 来验证文件: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2345</td><td>2052</td><td>[2]: 文件未包括有效的校验值。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2347</td><td>2052</td><td>用户忽略。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2348</td><td>2052</td><td>试图从压缩包流读取时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2349</td><td>2052</td><td>使用不同的信息恢复复制。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2350</td><td>2052</td><td>FDI 服务器错误</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2351</td><td>2052</td><td>未在压缩包 '[3]' 中找到文件密钥 '[2]'。安装无法继续。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2352</td><td>2052</td><td>无法初始化 CAB 文件服务器。所需的文件 'CABINET.DLL' 可能找不到。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2353</td><td>2052</td><td>不是压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2354</td><td>2052</td><td>无法处理压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2355</td><td>2052</td><td>损坏压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2356</td><td>2052</td><td>无法定位流中的压缩包: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2357</td><td>2052</td><td>无法设置属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2358</td><td>2052</td><td>确定文件是否正在使用中时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2359</td><td>2052</td><td>无法创建目标文件 - 可能正在使用文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2360</td><td>2052</td><td>进程刻度。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2361</td><td>2052</td><td>需要下一压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2362</td><td>2052</td><td>未找到文件夹: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2363</td><td>2052</td><td>无法枚举文件夹的子文件夹: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2364</td><td>2052</td><td>CreateCopier 调用中的错误枚举常量。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2365</td><td>2052</td><td>无法 BindImage exe 文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2366</td><td>2052</td><td>用户故障。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2367</td><td>2052</td><td>用户终止。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2368</td><td>2052</td><td>未能获取网络资源信息。错误 [2]，网络路径 [3]。扩展错误# :网络提供者 [5]，错误代码 [4]，错误描述 [6]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2370</td><td>2052</td><td>无效的 [2] 文件 CRC 校验值。{其标头表示校验值为 [3]，而其计算出的值为 [4]。}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2371</td><td>2052</td><td>无法将修补程序应用到文件 [2]。GetLastError: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2372</td><td>2052</td><td>修补程序文件 [2] 已损坏或格式无效。尝试修补文件 [3]。GetLastError: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2373</td><td>2052</td><td>文件 [2] 不是有效的修补程序文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2374</td><td>2052</td><td>文件 [2] 不是修补文件 [3] 的有效目标文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2375</td><td>2052</td><td>未知修补错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2376</td><td>2052</td><td>未找到压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2379</td><td>2052</td><td>打开文件进行读取时出错:[3] GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2380</td><td>2052</td><td>打开文件进行写入时出错:[3]。GetLastError: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2381</td><td>2052</td><td>目录不存在: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2382</td><td>2052</td><td>驱动器未就绪: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_24</td><td>2052</td><td>请插入磁盘: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2401</td><td>2052</td><td>试图为密钥 [2] 在 32 位操作系统上进行 64 位注册表操作。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2402</td><td>2052</td><td>内存不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_25</td><td>2052</td><td>安装程序没有访问目录 [2] 的权限，安装无法继续进行。请以管理员身份登录，或与您的系统管理员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2501</td><td>2052</td><td>无法创建回滚脚本枚举器。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2502</td><td>2052</td><td>在没有正在执行的安装时调用 InstallFinalize。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2503</td><td>2052</td><td>在未标记为正在进行时，调用了 RunScript。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_26</td><td>2052</td><td>写至文件 [2] 时出错。请确认您有访问该目录的权限。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2601</td><td>2052</td><td>属性 [2] 的无效值: '[3]'</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2602</td><td>2052</td><td>[2] 表条目 '[3]' 在媒体表中没有相关的条目。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2603</td><td>2052</td><td>重复表名称 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2604</td><td>2052</td><td>未定义 [2] 属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2605</td><td>2052</td><td>无法在 [3] 或 [4] 中找到服务器 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2606</td><td>2052</td><td>属性 [2] 的值不是有效的完整路径: '[3]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2607</td><td>2052</td><td>找不到媒体表或其为空（安装文件所需）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2608</td><td>2052</td><td>无法为对象创建安全描述符。错误: '[2]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2609</td><td>2052</td><td>尝试在初始化前迁移产品设置。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2611</td><td>2052</td><td>文件 [2] 标记为压缩，但相关媒体条目没有指定压缩包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2612</td><td>2052</td><td>'[2]' 列中找不到流。主键: '[3]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2613</td><td>2052</td><td>RemoveExistingProducts 操作顺序不正确。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2614</td><td>2052</td><td>无法从安装包访问 IStorage 对象。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2615</td><td>2052</td><td>由于源解析错误，跳过模块 [2] 的注销。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2616</td><td>2052</td><td>缺少附带文件 [2] 父项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2617</td><td>2052</td><td>未在组件表中找到共享组件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2618</td><td>2052</td><td>未在组件表中找到独立的应用程序组件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2619</td><td>2052</td><td>独立的组件 [2]、[3] 不是同一功能的组成部分。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2620</td><td>2052</td><td>独立的应用程序组件 [2] 的密钥文件不在文件表中。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2621</td><td>2052</td><td>快捷方式 [2] 的资源 DLL 或资源 ID 信息设置不正确。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27</td><td>2052</td><td>读取文件 [2] 时出错。{{ 系统错误 [3]。}}请确认文件存在，并且您能够访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2701</td><td>2052</td><td>可接受的树深度为 [2] 级，某功能的深度超出了此限制。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2702</td><td>2052</td><td>功能表记录 ([2]) 引用属性字段中不存在的父项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2703</td><td>2052</td><td>未定义根源路径的属性名称: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2704</td><td>2052</td><td>未定义根目录属性: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2705</td><td>2052</td><td>无效表:[2]；无法链接为树。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2706</td><td>2052</td><td>未创建源路径。目录表中不存在条目 [2] 的路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2707</td><td>2052</td><td>未创建目标路径。目录表中不存在条目 [2] 的路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2708</td><td>2052</td><td>未在文件表中找到条目。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2709</td><td>2052</td><td>未在组件表中找到指定的组件名称 ('[2]')。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2710</td><td>2052</td><td>对于此组件，所请求的 'Select' 状态为非法。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2711</td><td>2052</td><td>未在功能表中找到指定的功能名称 ('[2]')。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2712</td><td>2052</td><td>来自无模式对话框的无效返回:[3]，操作 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2713</td><td>2052</td><td>不可为空的列中存在空值（表 '[4]' 第 '[3]' 列中的 '[2]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2714</td><td>2052</td><td>默认文件夹名称的无效值: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2715</td><td>2052</td><td>未在文件表中找到指定的文件密钥 ('[2]')。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2716</td><td>2052</td><td>无法为组件 '[2]' 创建随机子组件名。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2717</td><td>2052</td><td>无效操作条件或调用自定义操作 '[2]' 时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2718</td><td>2052</td><td>缺少产品代码 '[2]' 的软件包名称。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2719</td><td>2052</td><td>源 '[2]' 中未找到 UNC 或驱动器号路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2720</td><td>2052</td><td>打开源列表键时出错。错误: '[2]'</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2721</td><td>2052</td><td>未在二进制表流中找到自定义操作 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2722</td><td>2052</td><td>未在文件表中找到自定义操作 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2723</td><td>2052</td><td>自定义操作 [2] 指定不支持的类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2724</td><td>2052</td><td>正在运行的来源媒体上的卷标 '[2]' 与媒体表中给定的标签 '[3]' 不匹配。只有当媒体表中仅有一个条目时，才允许出现此类情况。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2725</td><td>2052</td><td>无效的数据库表</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2726</td><td>2052</td><td>未找到操作: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2727</td><td>2052</td><td>目录表中不存在目录条目 '[2]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2728</td><td>2052</td><td>表定义错误: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2729</td><td>2052</td><td>未初始化安装引擎。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2730</td><td>2052</td><td>数据库中的无效值。表:'[2]'；主键:'[3]'；列: '[4]'</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2731</td><td>2052</td><td>未初始化 Selection Manager。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2732</td><td>2052</td><td>未初始化 Directory Manager。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2733</td><td>2052</td><td>'[4]' 表的 '[3]' 列中的无效外键 ('[2]')。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2734</td><td>2052</td><td>无效的重安装模式字符。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2735</td><td>2052</td><td>自定义操作 '[2]' 导致未处理的异常，并已被停止。这可能是自定义操作中的内部错误（如访问冲突）所造成的。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2736</td><td>2052</td><td>生成自定义操作临时文件失败: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2737</td><td>2052</td><td>无法访问自定义操作 [2]，条目 [3]，库 [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2738</td><td>2052</td><td>无法访问自定义操作 [2] 的 VBScript 运行时间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2739</td><td>2052</td><td>无法访问自定义操作 [2] 的 JavaScript 运行时间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2740</td><td>2052</td><td>自定义操作 [2] 脚本错误 [3]，[4]:[5] 第 [6] 行，第 [7] 列，[8]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2741</td><td>2052</td><td>产品 [2] 的配置信息已损坏。无效信息: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2742</td><td>2052</td><td>整理至服务器失败: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2743</td><td>2052</td><td>无法执行自定义操作 [2]，位置:[3]，命令: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2744</td><td>2052</td><td>自定义操作 [2] 调用 EXE 失败，位置:[3]，命令: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2745</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的语言 [4]，找到的语言 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2746</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品 [4]，找到的产品 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2747</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品版本 &lt; [4]，找到的产品版本 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2748</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品版本 &lt;= [4]，找到的产品版本 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2749</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品版本 == [4]，找到的产品版本 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2750</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品版本 &gt;= [4]，找到的产品版本 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27502</td><td>2052</td><td>无法连接到 [2] '[3]'。 [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27503</td><td>2052</td><td>从 [2] '[3]' 检索版本字符串时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27504</td><td>2052</td><td>SQL 版本要求未达到：[3]。此安装需要 [2] [4] 或更新版本。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27505</td><td>2052</td><td>无法打开 SQL 脚本文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27506</td><td>2052</td><td>执行 SQL 脚本 [2] 时出错。 行 [3]。 [4]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27507</td><td>2052</td><td>浏览或连接数据库服务器要求安装 MDAC。安装程序将立即终止。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27508</td><td>2052</td><td>安装 COM+ 应用程序 [2] 时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27509</td><td>2052</td><td>卸载 COM+ 应用程序 [2] 时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2751</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的产品版本 &gt; [4]，找到的产品版本 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27510</td><td>2052</td><td>安装 COM+ 应用程序 [2] 时出错。 无法创建 System.EnterpriseServices.RegistrationHelper 对象。 注册 Microsoft(R) .NET 服务组件需要安装 Microsoft(R) .NET Framework。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27511</td><td>2052</td><td>无法执行 SQL 脚本文件 [2]。 未打开连接： [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27512</td><td>2052</td><td>开始 [2] '[3]' 事务时出错。 数据库 [4]。 [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27513</td><td>2052</td><td>提交 [2] '[3]' 事务时出错。 数据库 [4]。 [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27514</td><td>2052</td><td>此安装需要 Microsoft SQL Server。指定服务器"[3]"是 Microsoft SQL Server Desktop Engine 或 SQL Server Express。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27515</td><td>2052</td><td>从 [2] '[3]' 检索模式版本时出错。 数据库： '[4]'. [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27516</td><td>2052</td><td>将模式版本写入 [2] '[3]' 时出错。 数据库： '[4]'. [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27517</td><td>2052</td><td>在本次安装中，您须具有管理员特权方能安装 COM+ 应用程序。请以管理员身份登录，然后重新开始安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27518</td><td>2052</td><td>COM+ 应用程序 "[2]" 的配置是作为一项 NT 服务运行；运行时，系统上须有 COM+ 1.5 或更新版。由于所用系统上有 COM+ 1.0，故无法安装该应用程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27519</td><td>2052</td><td>更新 XML 文件 [2] 时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2752</td><td>2052</td><td>无法打开作为软件包 [4] 的子存储来存储的转换 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27520</td><td>2052</td><td>打开 XML 文件 [2] 时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27521</td><td>2052</td><td>此安装程序需要 MSXML 3.0 或更高版本才能配置 XML 文件。请确定您有 3.0 或更高版本。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27522</td><td>2052</td><td>创建 XML 文件 [2] 时出错。 [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27523</td><td>2052</td><td>加载服务器出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27524</td><td>2052</td><td>加载 NetApi32.DLL 时出错。ISNetApi.dll 要求正确地加载 NetApi32.DLL，同时需要基于 NT的操作系统。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27525</td><td>2052</td><td>找不到服务器。 检查指定的服务器是否存在。 服务器名称不能为空白。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27526</td><td>2052</td><td>从 ISNetApi.dll 返回未指定的错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27527</td><td>2052</td><td>缓冲区太小。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27528</td><td>2052</td><td>访问被拒。 检查管理权限。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27529</td><td>2052</td><td>无效的计算机。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2753</td><td>2052</td><td>未标记文件 '[2]'以进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27530</td><td>2052</td><td>NetAPI 返回一个错误，原因不详。 系统错误﹕ [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27531</td><td>2052</td><td>未处理的异常情况。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27532</td><td>2052</td><td>此服务器或域的用户名无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27533</td><td>2052</td><td>区分大小写的密码不符。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27534</td><td>2052</td><td>列表是空的。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27535</td><td>2052</td><td>访问违规。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27536</td><td>2052</td><td>获取组时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27537</td><td>2052</td><td>添加用户到组时出错。 检查该组是否存在于此域或服务器中。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27538</td><td>2052</td><td>创建用户时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27539</td><td>2052</td><td>已从 NetAPI 返回 ERROR_NETAPI_ERROR_NOT_PRIMARY。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2754</td><td>2052</td><td>文件 '[2]' 不是有效的修补程序文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27540</td><td>2052</td><td>指定的用户已存在。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27541</td><td>2052</td><td>指定的组已存在。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27542</td><td>2052</td><td>无效的密码。 检查密码是否与您的网络密码规则一致。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27543</td><td>2052</td><td>无效的名称。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27544</td><td>2052</td><td>无效的组。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27545</td><td>2052</td><td>用户名不得为空，且其格式必须为“域\用户名”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27546</td><td>2052</td><td>在用户 TEMP 目录中加载或创建 INI 文件时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27547</td><td>2052</td><td>ISNetAPI.dll 未加载或加载 dll 时出错。 这项操作需要加载此 dll。 检查 dll 是否已经在 SUPPORTDIR 目录中。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27548</td><td>2052</td><td>从用户的 TEMP 目录中删除包含新用户信息的 INI 文件时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27549</td><td>2052</td><td>获取主域控制器 (PDC) 时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2755</td><td>2052</td><td>试图安装软件包 [3] 时，服务器返回意外错误 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27550</td><td>2052</td><td>每个字段必须有值才能创建用户。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27551</td><td>2052</td><td>找不到 [2] 的 ODBC 驱动程序。这是与 [2] 数据库服务器连接的必备工具。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27552</td><td>2052</td><td>创建数据库 [4] 时出错。服务器：[2] [3]. [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27553</td><td>2052</td><td>连接到数据库 [4] 时出错。服务器：[2] [3]. [5]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27554</td><td>2052</td><td>试图打开连接 [2] 时出错。没有与此连接相关的有效数据库元数据。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_27555</td><td>2052</td><td>将许可应用于对象 '[2]' 时出错。 系统错误﹕ [3] ([4])</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2756</td><td>2052</td><td>属性 '[2]' 在一个或多个表中用作目录属性，但从未指定过值。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2757</td><td>2052</td><td>无法为转换 [2] 创建摘要信息。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2758</td><td>2052</td><td>转换 [2] 不包含 MSI 版本。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2759</td><td>2052</td><td>转换 [2] 版本 [3] 与引擎不兼容；最低:[4]，最高: [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2760</td><td>2052</td><td>对于软件包 [3]，转换 [2] 无效。预期的升级代码 [4]，找到 [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2761</td><td>2052</td><td>无法开始事务。未正确初始化全局互斥体。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2762</td><td>2052</td><td>无法写入脚本记录。未启动事务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2763</td><td>2052</td><td>无法运行脚本。未启动事务。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2765</td><td>2052</td><td>AssemblyName 表中缺少程序集名称:组件: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2766</td><td>2052</td><td>文件 [2] 为无效的 MSI 存储文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2767</td><td>2052</td><td>没有更多数据{ 枚举 [2] 时}。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2768</td><td>2052</td><td>修补软件包中的转换无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2769</td><td>2052</td><td>自定义操作 [2] 未关闭 [3] MSIHANDLE。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2770</td><td>2052</td><td>未在内部缓存文件夹表中定义缓存文件夹 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2771</td><td>2052</td><td>功能 [2] 的升级缺少一个组件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2772</td><td>2052</td><td>新升级功能 [2] 必须为分页功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_28</td><td>2052</td><td>另一应用程序已经以独占模式访问了文件 [2]。请关闭所有其他的应用程序，然后再单击“重试”按钮。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2801</td><td>2052</td><td>未知消息 -- 类型 [2]。不执行任何操作。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2802</td><td>2052</td><td>未找到事件 [2] 的发行程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2803</td><td>2052</td><td>Dialog View 未找到对话框 [2] 的记录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2804</td><td>2052</td><td>激活对话框 [2] 上的控件 [3] 时，CmsiDialog 无法评估条件 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2806</td><td>2052</td><td>对话框 [2] 无法评估条件 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2807</td><td>2052</td><td>未识别操作 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2808</td><td>2052</td><td>在对话框 [2] 上错误定义默认按钮。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2809</td><td>2052</td><td>在对话框 [2] 上，后面的控件指针没有形成循环。有从 [3] 指向 [4] 的指针，但没有其他指针。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2810</td><td>2052</td><td>在对话框 [2] 上，后面的控件指针没有形成循环。有从 [3] 和 [5] 指向 [4] 的指针。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2811</td><td>2052</td><td>在对话框 [2] 上，控件 [3] 必须取得焦点，但无法实现。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2812</td><td>2052</td><td>未识别事件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2813</td><td>2052</td><td>使用参数 [2] 调用了 EndDialog 事件，但对话框具有父项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2814</td><td>2052</td><td>在对话框 [2] 上，控件 [3] 将一个不存在的控件 [4] 作为下一控件命名。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2815</td><td>2052</td><td>ControlCondition 表有一行没有对话框 [2] 的条件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2816</td><td>2052</td><td>EventMapping 表为事件 [3] 引用对话框 [2] 上的无效控件 [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2817</td><td>2052</td><td>事件 [2] 无法为对话框 [3] 上的控件 [4] 设置属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2818</td><td>2052</td><td>在 ControlEvent 表中，EndDialog 具有无法识别的参数 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2819</td><td>2052</td><td>对话框 [2] 上的控件 [3] 需要一个链接到它的属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2820</td><td>2052</td><td>尝试初始化一个已初始化的处理程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2821</td><td>2052</td><td>尝试初始化一个已初始化的对话框: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2822</td><td>2052</td><td>只有添加所有控件后，才能在对话框 [2] 上调用其他方法。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2823</td><td>2052</td><td>尝试初始化一个已初始化的对话框:对话框 [2] 上的 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2824</td><td>2052</td><td>对话框属性 [3] 需要一条至少 [2] 个字段的记录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2825</td><td>2052</td><td>控件属性 [3] 需要一条至少 [2] 个字段的记录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2826</td><td>2052</td><td>对话框 [2] 上的控件 [3] 超出了对话框 [4] 的边界 [5] 个像素。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2827</td><td>2052</td><td>对话框 [2] 上的单选按钮组 [3] 中的按钮 [4] 超出组 [5] 的边界 [6] 个像素。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2828</td><td>2052</td><td>试图从对话框 [2] 删除控件 [3]，但该控件不是对话框的一部分。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2829</td><td>2052</td><td>尝试使用未初始化的对话框。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2830</td><td>2052</td><td>尝试使用对话框 [2] 上未初始化的控件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2831</td><td>2052</td><td>对话框 [2] 上的控件 [3] 不支持 [5] 属性 [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2832</td><td>2052</td><td>对话框 [2] 不支持属性 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2833</td><td>2052</td><td>对话框 [3] 上的控件 [4] 忽略消息 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2834</td><td>2052</td><td>对话框 [2] 上后面的指针没有形成单循环。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2835</td><td>2052</td><td>未在对话框 [3] 上找到控件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2836</td><td>2052</td><td>对话框 [2] 上的控件 [3] 无法取得焦点。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2837</td><td>2052</td><td>对话框 [2] 上的控件 [3] 希望 winproc 返回 [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2838</td><td>2052</td><td>期   选择表中的项目 [2] 将其自身作为父项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2839</td><td>2052</td><td>设置属性 [2] 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2840</td><td>2052</td><td>错误对话框名称不匹配。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2841</td><td>2052</td><td>未在错误对话框上找到确定按钮。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2842</td><td>2052</td><td>未在错误对话框中找到文本字段。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2843</td><td>2052</td><td>标准对话框不支持 ErrorString 属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2844</td><td>2052</td><td>如果未设置 Errorstring，则无法执行错误对话框。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2845</td><td>2052</td><td>按钮的总宽度超过错误对话框的尺寸。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2846</td><td>2052</td><td>SetFocus 未在错误对话框上找到所需的控件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2847</td><td>2052</td><td>对话框 [2] 上的控件 [3] 同时设置了图标和位图样式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2848</td><td>2052</td><td>试图将控件 [3] 设置为对话框 [2] 上的默认按钮，但该控件不存在。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2849</td><td>2052</td><td>对话框 [2] 上的控件 [3] 为一个值不能为整数的类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2850</td><td>2052</td><td>无法识别的卷类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2851</td><td>2052</td><td>图标 [2] 的数据无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2852</td><td>2052</td><td>使用前，必须至少将一个控件添加到对话框 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2853</td><td>2052</td><td>对话框 [2] 为无模式对话框。不应在此对话框上调用此执行方法。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2854</td><td>2052</td><td>在对话框 [2] 上，控件 [3] 被指定为第一活动控件，但不存在此类控件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2855</td><td>2052</td><td>对话框 [2] 上的单选按钮组 [3] 的按钮少于两个。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2856</td><td>2052</td><td>创建对话框 [2] 的第二个副本。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2857</td><td>2052</td><td>在选择表中提及了目录 [2]，但却并未找到。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2858</td><td>2052</td><td>位图 [2] 的数据无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2859</td><td>2052</td><td>测试错误消息。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2860</td><td>2052</td><td>在对话框 [2] 上错误定义取消按钮。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2861</td><td>2052</td><td>对话框 [2] 控件 [3] 上的单选按钮后面的指针没有形成循环。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2862</td><td>2052</td><td>对话框 [2] 上的控件 [3] 的属性没有定义有效的图标大小。将大小设置为 16。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2863</td><td>2052</td><td>对话框 [2] 上的控件 [3] 需要尺寸为 [5]x[5] 的图标 [4]，但该尺寸不可用。加载第一个可用的尺寸。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2864</td><td>2052</td><td>对话框 [2] 上的控件 [3] 收到了浏览事件，但没有用于当前选择的可配置目录。可能的原因有:未正确设计浏览按钮。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2865</td><td>2052</td><td>公告板 [2] 上的控件 [3] 超出了公告板 [4] 的边界 [5] 个像素。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2866</td><td>2052</td><td>不允许对话框 [2] 返回参数 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2867</td><td>2052</td><td>未设置错误对话框属性。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2868</td><td>2052</td><td>错误对话框 [2] 的样式位设置是正确的。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2869</td><td>2052</td><td>对话框 [2] 样式位设置错误，但它不是错误的对话框。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2870</td><td>2052</td><td>对话框 [2] 上控件 [3] 的帮助字符串 [4] 不包含分隔符。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2871</td><td>2052</td><td>[2] 表已过期: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2872</td><td>2052</td><td>对话框 [2] 上的 CheckPath 控件事件的参数无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2873</td><td>2052</td><td>在对话框 [2] 上，控件 [3] 的无效字符串长度限制为: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2874</td><td>2052</td><td>将文本字体更改为 [2] 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2875</td><td>2052</td><td>将文本颜色更改为 [2] 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2876</td><td>2052</td><td>对话框 [2] 上的控件 [3] 必须截断字符串: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2877</td><td>2052</td><td>未找到二进制数据 [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2878</td><td>2052</td><td>在对话框 [2] 上，控件 [3] 的可能值为:[4]。 这是一个无效值或重复值。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2879</td><td>2052</td><td>对话框 [2] 上的控件 [3] 无法解析掩码字符串: [4]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2880</td><td>2052</td><td>请勿执行剩余的控件事件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2881</td><td>2052</td><td>CmsiHandler 初始化失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2882</td><td>2052</td><td>对话框窗口类注册失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2883</td><td>2052</td><td>为对话框 [2] CreateNewDialog 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2884</td><td>2052</td><td>无法为对话框 [2] 创建窗口。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2885</td><td>2052</td><td>无法在对话框 [2] 上创建控件 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2886</td><td>2052</td><td>创建 [2] 表失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2887</td><td>2052</td><td>创建到 [2] 表的光标失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2888</td><td>2052</td><td>执行 [2] 视图失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2889</td><td>2052</td><td>为对话框 [2] 上的控件 [3] 创建窗口失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2890</td><td>2052</td><td>处理程序无法创建已初始化的对话框。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2891</td><td>2052</td><td>无法销毁对话框 [2] 的窗口。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2892</td><td>2052</td><td>控件 [2] 只能为整型，[3] 不是有效的整数值。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2893</td><td>2052</td><td>对话框 [2] 上的控件 [3] 可接受最多 [5] 个字符长的属性值。值 [4] 超过此限制，因此已被截断。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2894</td><td>2052</td><td>加载 RICHED20.DLL 失败。GetLastError() 返回: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2895</td><td>2052</td><td>释放 RICHED20.DLL 失败。GetLastError() 返回: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2896</td><td>2052</td><td>执行操作 [2] 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2897</td><td>2052</td><td>无法在此系统上创建任意 [2] 字体。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2898</td><td>2052</td><td>对于 [2] 文本样式，系统在 [4] 字符集中创建了 '[3]' 字体。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2899</td><td>2052</td><td>无法创建 [2] 文本样式。GetLastError() 返回: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_29</td><td>2052</td><td>没有足够的磁盘空间来安装文件 [2]。请释放一些磁盘空间后单击“重试”，或者单击“取消”退出。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2901</td><td>2052</td><td>操作 [2] 的无效参数:参数 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2902</td><td>2052</td><td>操作 [2] 调用顺序不正确。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2903</td><td>2052</td><td>缺少文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2904</td><td>2052</td><td>无法 BindImage 文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2905</td><td>2052</td><td>无法从脚本文件 [2] 读取记录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2906</td><td>2052</td><td>脚本文件 [2] 中缺少标题。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2907</td><td>2052</td><td>无法创建安全的安全描述符。错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2908</td><td>2052</td><td>无法注册组件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2909</td><td>2052</td><td>无法注销组件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2910</td><td>2052</td><td>无法确定用户的安全 ID。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2911</td><td>2052</td><td>无法删除文件夹 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2912</td><td>2052</td><td>无法计划在重新启动时要删除的文件 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2919</td><td>2052</td><td>没有为压缩文件指定压缩包: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2920</td><td>2052</td><td>未为文件 [2] 指定源目录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2924</td><td>2052</td><td>不支持脚本 [2] 版本。脚本版本:[3]，最低版本:[4]，最高版本: [5]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2927</td><td>2052</td><td>ShellFolder ID [2] 无效。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2928</td><td>2052</td><td>超出源的最大数。跳过源 '[2]'。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2929</td><td>2052</td><td>无法判断发布根目录。错误: [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2932</td><td>2052</td><td>无法从脚本数据创建文件 [2]。错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2933</td><td>2052</td><td>无法初始化回滚脚本 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2934</td><td>2052</td><td>无法保护转换 [2]。错误 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2935</td><td>2052</td><td>无法取消保护转换 [2]。错误 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2936</td><td>2052</td><td>无法找到转换 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2937</td><td>2052</td><td>Windows Installer 无法安装系统文件保护编录。编录:[2]，错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2938</td><td>2052</td><td>Windows Installer 无法从缓存检索系统文件保护编录。编录:[2]，错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2939</td><td>2052</td><td>Windows Installer 无法从缓存删除系统文件保护编录。编录:[2]，错误: [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2940</td><td>2052</td><td>未提供 Directory Manager 来实现源解析。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2941</td><td>2052</td><td>无法计算文件 [2] 的 CRC。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2942</td><td>2052</td><td>BindImage 操作尚未在 [2] 文件上执行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2943</td><td>2052</td><td>此版本的 Windows 不支持部署 64 位软件包。脚本 [2] 用于 64 位软件包。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2944</td><td>2052</td><td>GetProductAssignmentType 失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_2945</td><td>2052</td><td>安装 ComPlus App [2] 失败，错误 [3]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_3</td><td>2052</td><td>信息 [1]。 </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_30</td><td>2052</td><td>没有找到源文件 [2]。请确认文件存在，并且您能够访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_3001</td><td>2052</td><td>此列表中的修补程序包含不正确的顺序信息: [2][3][4][5][6][7][8][9][10][11][12][13][14][15][16]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_3002</td><td>2052</td><td>修补程序 [2] 包括无效的顺序信息。 </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_31</td><td>2052</td><td>读取文件 [3] 时出错。{{ 系统错误 [2]。}} 请确认文件存在，并且您能够访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_32</td><td>2052</td><td>写至文件 [3] 时出错。{{ 系统错误 [2]。}} 请确认您有权访问该目录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_33</td><td>2052</td><td>没有找到源文件{{(包)}}: [2]。请确认文件存在，并且您能够访问该文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_34</td><td>2052</td><td>无法创建目录 [2]。同名文件已经存在。请重命名或删除文件，然后单击“重试”按钮，或者单击“取消”按钮退出。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_35</td><td>2052</td><td>目前无法使用卷 [2]，请另选其他卷。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_36</td><td>2052</td><td>无法使用指定的路径 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_37</td><td>2052</td><td>无法写入指定的文件夹 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_38</td><td>2052</td><td>试图读取文件 [2] 时发生网络错误。 </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_39</td><td>2052</td><td>试图创建目录 [2] 时发生错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_4</td><td>2052</td><td>内部错误 [1]. [2]{, [3]}{, [4]}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_40</td><td>2052</td><td>试图创建目录 [2] 时发生网络错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_41</td><td>2052</td><td>试图打开源文件包 [2] 时发生网络错误。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_42</td><td>2052</td><td>指定的路径过长: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_43</td><td>2052</td><td>安装程序没有修改文件 [2] 的权限。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_44</td><td>2052</td><td>文件夹路径 [2] 的一部分无效。或者为空，或者超出了系统允许的长度。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_45</td><td>2052</td><td>文件夹路径 [2] 中含有无效单词。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_46</td><td>2052</td><td>文件夹路径 [2] 中含有无效的字符。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_47</td><td>2052</td><td>[2] 不是一个有效的短文件名。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_48</td><td>2052</td><td>获取文件 [3] 安全权限时发生错误 GetLastError: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_49</td><td>2052</td><td>无效驱动器: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_5</td><td>2052</td><td>{{磁盘已满: }}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_50</td><td>2052</td><td>无法创建键: [2]。{{ 系统错误 [3]。}} 请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_51</td><td>2052</td><td>无法打开键: [2]。{{ 系统错误 [3]。}}  请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。 </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_52</td><td>2052</td><td>无法删除值 [2]（从键 [3] 中）。{{ 系统错误 [4]。}}  请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_53</td><td>2052</td><td>无法删除键 [2]。{{ 系统错误 [3]。}}  请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_54</td><td>2052</td><td>无法读取值 [2]（从键 [3] 中）。{{ 系统错误 [4]。}}  请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_55</td><td>2052</td><td>无法将数值 [2] 写入键 [3]。{{ 系统错误 [4]。}} 请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_56</td><td>2052</td><td>无法获取键 [2] 的数值名称。{{ 系统错误 [3]。}} 请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_57</td><td>2052</td><td>无法获取键 [2] 的子键。{{ 系统错误 [3]。}} 请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_58</td><td>2052</td><td>无法读取键 [2] 的安全信息。{{ 系统错误 [3]。}} 请验证您对该键拥有足够的访问权限，或者与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_59</td><td>2052</td><td>无法增加可用的注册表空间。安装本应用程序需要 [2] 千字节的空闲注册表空间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_6</td><td>2052</td><td>操作 [Time]: [1]. [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_60</td><td>2052</td><td>另一安装过程正在进行。您必须先完成那次过程，然后才能继续本次过程。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_61</td><td>2052</td><td>访问受保护的数据时出错。请确认 Windows Installer 配置是否正确，然后重新安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_62</td><td>2052</td><td>用户 [2] 以前启动过产品 [3] 的安装程序。若要使用该产品，需要请此用户再次运行安装程序。现在将继续进行您的安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_63</td><td>2052</td><td>用户 [2] 以前启动过产品 [3] 的安装程序。若要使用该产品，需要请此用户再次运行安装程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_64</td><td>2052</td><td>磁盘空间不足 -- 卷: [2]；所需空间: [3] 千字节；可用空间: [4] 千字节。请在释放磁盘空间后再试。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_65</td><td>2052</td><td>是否确认要取消操作？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_66</td><td>2052</td><td>文件 [2][3] 正被使用 {使用者: 名称: [4]，Id: [5]，窗口标题: “[6]”}。请关闭那个应用程序后再重试。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_67</td><td>2052</td><td>产品 [2] 已经安装，现在无法安装本产品。这两种产品不兼容。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_68</td><td>2052</td><td>磁盘空间不足 -- 卷 [2]；所需空间: [3] 千字节；可用空间: [4] 千字节。如果禁用回滚功能，则空间足够。单击“取消”退出，单击“重试”再次检查可用空间，单击“忽略”在禁用回滚功能的情况下继续安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_69</td><td>2052</td><td>无法访问网络位置 [2]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_7</td><td>2052</td><td>[ProductName]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_70</td><td>2052</td><td>在继续安装之前，请关闭以下应用程序: </td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_71</td><td>2052</td><td>无法在计算机上找到安装本产品所需的任何以前安装的相应产品。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_72</td><td>2052</td><td>键 [2] 无效。请确认您输入的键是否正确。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_73</td><td>2052</td><td>安装程序必须先重新启动您的系统，然后才能继续配置 [2]。单击“是”按钮可立即重新启动；单击“否”按钮则可在以后以人工方式启动。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_74</td><td>2052</td><td>您必须先重新启动系统，然后才能使对 [2] 做出的配置修改生效。单击“是”按钮可立即重新启动；单击“否”按钮则可在以后以人工方式启动。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_75</td><td>2052</td><td>[2] 的一次安装过程正处于暂停状态，您必须先撤消该安装过程做出的修改，然后才能继续操作。是否撤消那些修改？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_76</td><td>2052</td><td>本产品的前一次安装正在进行，您必须先撤消该过程做出的修改，然后才能继续。是否撤消那些修改？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_77</td><td>2052</td><td>无法找到产品 [2] 的安装程序包。请使用安装程序包 [3] 的有效拷贝重新进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_78</td><td>2052</td><td>安装操作已成功完成。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_79</td><td>2052</td><td>安装操作失败。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_8</td><td>2052</td><td>{[2]}{, [3]}{, [4]}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_80</td><td>2052</td><td>产品: [2] -- [3]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_81</td><td>2052</td><td>您可以将计算机还原至其原始状态，也可在以后继续安装。是否要进行还原？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_82</td><td>2052</td><td>向硬盘写入安装信息时发生错误。请确认是否有足够的硬盘空间可供使用，然后单击“重试”按钮，或者单击“取消”按钮结束安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_83</td><td>2052</td><td>无法找到将您的计算机恢复至原始状态所需的一个或多个文件。不能进行恢复操作。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_84</td><td>2052</td><td>路径 [2] 无效，请指定有效路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_85</td><td>2052</td><td>内存不足。请先关闭其他应用程序，然后再重试。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_86</td><td>2052</td><td>驱动器 [2] 中没有磁盘。请先插入磁盘，然后单击“重试”按钮；或者单击“取消”按钮，返回前面选择的卷。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_87</td><td>2052</td><td>驱动器 [2] 中没有磁盘。请先插入磁盘，然后单击“重试”按钮；或者单击“取消”按钮，返回“浏览”对话框并选择其他卷。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_88</td><td>2052</td><td>文件夹 [2] 不存在。请输入某个原有文件夹的路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_89</td><td>2052</td><td>您读取此文件夹的权限不够。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_9</td><td>2052</td><td>消息类型: [1]， 参数: [2]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_90</td><td>2052</td><td>无法确定安装所需的有效路径。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_91</td><td>2052</td><td>试图读取源安装数据库 [2] 时出错。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_92</td><td>2052</td><td>正在安排重新启动操作: 将文件 [2] 重命名为 [3]。只有重新启动后操作才能完成。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_93</td><td>2052</td><td>正在安排重新启动操作: 删除文件 [2]。只有重新启动后操作才能完成。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_94</td><td>2052</td><td>无法注册模块 [2]。HRESULT [3]。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_95</td><td>2052</td><td>无法撤消注册模块 [2]。HRESULT [3]。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_96</td><td>2052</td><td>无法缓存包 [2]。错误: [3]。请与您的技术支持人员联系。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_97</td><td>2052</td><td>无法注册字体 [2]。请检查您是否有足够的权限安装字体，以及系统是否能够支持该字体。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_98</td><td>2052</td><td>无法撤消对字体 [2] 的注册。请检查您是否有足够的权限删除字体。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ERROR_99</td><td>2052</td><td>无法创建快捷方式 [2]。请检查目标文件夹是否存在，以及您是否可以访问该文件夹。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_INSTALLDIR</td><td>2052</td><td>{&amp;Tahoma8}[INSTALLDIR]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_INSTALLSHIELD</td><td>2052</td><td>InstallShield</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_INSTALLSHIELD_FORMATTED</td><td>2052</td><td>{&amp;MSSWhiteSerif8}InstallShield</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_MISSING</td><td>2052</td><td>该计算机缺少 InstallScript 引擎。请运行 ISScript.msi（如果有的话），或者联系您的支持人员获得进一步的帮助。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_OLD</td><td>2052</td><td>该计算机中的 InstallScript 引擎版本早于此安装程序需要的版本。请安装 ISScript.msi 的最新版本（如果有的话），或者联系您的支持人员获得进一步的帮助。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_NEXT</td><td>2052</td><td>下一步(&amp;N) &gt;</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_OK</td><td>2052</td><td>{&amp;Tahoma8}确定</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_BROWSE</td><td>2052</td><td>打开 [ProductName] 原版 [SETUPEXENAME]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_INVALID</td><td>2052</td><td>这一可执行文件好象不是 [ProductName] 原版的可执行文件。 若不用原版的 [SETUPEXENAME] 安装其他相关软件，[ProductName] 可能会出现问题。 是否寻找原版的 [SETUPEXENAME]？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_SEARCH</td><td>2052</td><td>本次安装可能需要使用其他相关软件。 若没有这些相关软件，[ProductName] 可能会出现问题。 是否寻找原来的 [SETUPEXENAME]？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PREVENT_DOWNGRADE_EXIT</td><td>2052</td><td>该应用程序的新版已在这台计算机上安装。如果您想安装这个版本，请先卸载已安装的新版程序。若需退出向导程序，请点击“确定”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PRINT_BUTTON</td><td>2052</td><td>打印(&amp;P)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PRODUCTNAME_INSTALLSHIELD</td><td>2052</td><td>[ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOL</td><td>2052</td><td>创建应用程序池 %s</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOLS</td><td>2052</td><td>正在创建应用程序池...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOT</td><td>2052</td><td>正在创建 IIS 虚拟目录 %s</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOTS</td><td>2052</td><td>正在创建 IIS 虚拟目录...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>2052</td><td>创建 Web 服务扩展</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>2052</td><td>正在创建 Web 服务扩展...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITE</td><td>2052</td><td>正在创建 IIS 网站 %s</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITES</td><td>2052</td><td>正在创建 IIS 网站...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACT</td><td>2052</td><td>正在检索 IIS 虚拟目录的信息...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACTDONE</td><td>2052</td><td>已检索 IIS 虚拟目录的信息...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOL</td><td>2052</td><td>删除应用程序池</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOLS</td><td>2052</td><td>正在删除应用程序池...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVESITE</td><td>2052</td><td>正在删除端口 %d 的网站</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOT</td><td>2052</td><td>正在删除 IIS 虚拟目录 %s</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOTS</td><td>2052</td><td>正在删除 IIS 虚拟目录...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>2052</td><td>删除 Web 服务扩展</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>2052</td><td>正在删除 Web 服务扩展...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSITES</td><td>2052</td><td>正在删除 IIS 网站...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>2052</td><td>正在回滚应用程序池...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKVROOTS</td><td>2052</td><td>正在回滚虚拟目录和网站更改...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>2052</td><td>正在回滚 Web 服务扩展...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>2052</td><td>替换 %s 中，使用的是﹕%s，位于 %s...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_COSTING</td><td>2052</td><td>正在计算 XML 文件成本...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_CREATE_FILE</td><td>2052</td><td>正在创建 XML 文件 %s...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_FILES</td><td>2052</td><td>正在执行 XML 文件更改...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_REMOVE_FILE</td><td>2052</td><td>正在删除 XML 文件 %s...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_ROLLBACK_FILES</td><td>2052</td><td>正在回滚 XML 文件更改...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_PROGMSG_XML_UPDATE_FILE</td><td>2052</td><td>正在更新 XML 文件 %s...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SETUPEXE_EXPIRE_MSG</td><td>2052</td><td>本安装程序的使用期到 %s 结束。安装程序现在将退出。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SETUPEXE_LAUNCH_COND_E</td><td>2052</td><td>本安装程序内建有 InstallShield 的评估版，只能用 setup.exe 文件启动。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME1</td><td>2052</td><td>奇点取证</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME10</td><td>1033</td><td/><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME10</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.vshost.exe</td><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME100</td><td>1033</td><td/><td>0</td><td/><td>-64682766</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME100</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>-64674574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME11</td><td>1033</td><td/><td>0</td><td/><td>1000704467</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME11</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1000692211</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME12</td><td>1033</td><td/><td>0</td><td/><td>1000704467</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME12</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1000704467</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME13</td><td>1033</td><td/><td>0</td><td/><td>1134916649</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME13</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1134916649</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME14</td><td>1033</td><td/><td>0</td><td/><td>1134916649</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME14</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1134916649</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME15</td><td>1033</td><td/><td>0</td><td/><td>1134914665</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME15</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1134914665</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME16</td><td>1033</td><td/><td>0</td><td/><td>1134914665</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME16</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1134914665</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME17</td><td>1033</td><td/><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME17</td><td>2052</td><td>LAUNCH~1.EXE|Launch jabswitch.exe</td><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME18</td><td>1033</td><td/><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME18</td><td>2052</td><td>LAUNCH~1.EXE|Launch java-rmi.exe</td><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME19</td><td>1033</td><td/><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME19</td><td>2052</td><td>LAUNCH~1.EXE|Launch java.exe</td><td>0</td><td/><td>1948600428</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME2</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME20</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME20</td><td>2052</td><td>LAUNCH~1.EXE|Launch javacpl.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME21</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME21</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaw.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME22</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME22</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaws.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME23</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME23</td><td>2052</td><td>LAUNCH~1.EXE|Launch jjs.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME24</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME24</td><td>2052</td><td>LAUNCH~1.EXE|Launch jp2launcher.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME25</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME25</td><td>2052</td><td>LAUNCH~1.EXE|Launch keytool.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME26</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME26</td><td>2052</td><td>LAUNCH~1.EXE|Launch kinit.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME27</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME27</td><td>2052</td><td>LAUNCH~1.EXE|Launch klist.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME28</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME28</td><td>2052</td><td>LAUNCH~1.EXE|Launch ktab.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME29</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME29</td><td>2052</td><td>LAUNCH~1.EXE|Launch orbd.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME3</td><td>2052</td><td>卸载流火手机取证分析系统</td><td>0</td><td/><td>1000705071</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME30</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME30</td><td>2052</td><td>LAUNCH~1.EXE|Launch pack200.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME31</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME31</td><td>2052</td><td>LAUNCH~1.EXE|Launch policytool.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME32</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME32</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmid.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME33</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME33</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmiregistry.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME34</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME34</td><td>2052</td><td>LAUNCH~1.EXE|Launch servertool.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME35</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME35</td><td>2052</td><td>LAUNCH~1.EXE|Launch ssvagent.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME36</td><td>1033</td><td/><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME36</td><td>2052</td><td>LAUNCH~1.EXE|Launch tnameserv.exe</td><td>0</td><td/><td>1948602476</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME37</td><td>1033</td><td/><td>0</td><td/><td>1948604524</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME37</td><td>2052</td><td>LAUNCH~1.EXE|Launch unpack200.exe</td><td>0</td><td/><td>1948604524</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME38</td><td>1033</td><td/><td>0</td><td/><td>1948612716</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME38</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1948612716</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME39</td><td>1033</td><td/><td>0</td><td/><td>1948612716</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME39</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1948612716</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME4</td><td>1033</td><td>奇点取证</td><td>0</td><td/><td>-2111479200</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME4</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>-2111487424</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME40</td><td>1033</td><td/><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME40</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME41</td><td>1033</td><td/><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME41</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME42</td><td>1033</td><td/><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME42</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME43</td><td>1033</td><td/><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME43</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.vshost.exe</td><td>0</td><td/><td>1948618860</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME44</td><td>1033</td><td/><td>0</td><td/><td>1948620908</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME44</td><td>2052</td><td>LAUNCH~1.EXE|Launch adb.exe</td><td>0</td><td/><td>1948620908</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME45</td><td>1033</td><td/><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME45</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME46</td><td>1033</td><td/><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME46</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME47</td><td>1033</td><td/><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME47</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME48</td><td>1033</td><td/><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME48</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.vshost.exe</td><td>0</td><td/><td>1948631148</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME49</td><td>1033</td><td/><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME49</td><td>2052</td><td>LAUNCH~1.EXE|Launch jabswitch.exe</td><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME5</td><td>1033</td><td/><td>0</td><td/><td>-2111487424</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME5</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>-2111487424</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME50</td><td>1033</td><td/><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME50</td><td>2052</td><td>LAUNCH~1.EXE|Launch java-rmi.exe</td><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME51</td><td>1033</td><td/><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME51</td><td>2052</td><td>LAUNCH~1.EXE|Launch java.exe</td><td>0</td><td/><td>1948612653</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME52</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME52</td><td>2052</td><td>LAUNCH~1.EXE|Launch javacpl.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME53</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME53</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaw.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME54</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME54</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaws.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME55</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME55</td><td>2052</td><td>LAUNCH~1.EXE|Launch jjs.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME56</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME56</td><td>2052</td><td>LAUNCH~1.EXE|Launch jp2launcher.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME57</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME57</td><td>2052</td><td>LAUNCH~1.EXE|Launch keytool.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME58</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME58</td><td>2052</td><td>LAUNCH~1.EXE|Launch kinit.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME59</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME59</td><td>2052</td><td>LAUNCH~1.EXE|Launch klist.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME6</td><td>1033</td><td/><td>0</td><td/><td>-2111458496</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME6</td><td>2052</td><td>奇点取证</td><td>0</td><td/><td>-2111503520</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME60</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME60</td><td>2052</td><td>LAUNCH~1.EXE|Launch ktab.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME61</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME61</td><td>2052</td><td>LAUNCH~1.EXE|Launch orbd.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME62</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME62</td><td>2052</td><td>LAUNCH~1.EXE|Launch pack200.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME63</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME63</td><td>2052</td><td>LAUNCH~1.EXE|Launch policytool.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME64</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME64</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmid.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME65</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME65</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmiregistry.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME66</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME66</td><td>2052</td><td>LAUNCH~1.EXE|Launch servertool.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME67</td><td>1033</td><td/><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME67</td><td>2052</td><td>LAUNCH~1.EXE|Launch ssvagent.exe</td><td>0</td><td/><td>1948614701</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME68</td><td>1033</td><td/><td>0</td><td/><td>1948616749</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME68</td><td>2052</td><td>LAUNCH~1.EXE|Launch tnameserv.exe</td><td>0</td><td/><td>1948616749</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME69</td><td>1033</td><td/><td>0</td><td/><td>1948616749</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME69</td><td>2052</td><td>LAUNCH~1.EXE|Launch unpack200.exe</td><td>0</td><td/><td>1948616749</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME7</td><td>1033</td><td/><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME7</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1000698927</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME70</td><td>1033</td><td/><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME70</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME71</td><td>1033</td><td/><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME71</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME72</td><td>1033</td><td/><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME72</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME73</td><td>1033</td><td/><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME73</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.vshost.exe</td><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME74</td><td>1033</td><td/><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME74</td><td>2052</td><td>LAUNCH~1.EXE|Launch adb.exe</td><td>0</td><td/><td>1948622893</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME75</td><td>1033</td><td/><td>0</td><td/><td>1948616941</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME75</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1948637421</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME76</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME76</td><td>2052</td><td>LAUNCH~1.EXE|Launch jabswitch.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME77</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME77</td><td>2052</td><td>LAUNCH~1.EXE|Launch java-rmi.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME78</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME78</td><td>2052</td><td>LAUNCH~1.EXE|Launch java.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME79</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME79</td><td>2052</td><td>LAUNCH~1.EXE|Launch javacpl.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME8</td><td>1033</td><td/><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME8</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.vshost.exe</td><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME80</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME80</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaw.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME81</td><td>1033</td><td/><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME81</td><td>2052</td><td>LAUNCH~1.EXE|Launch javaws.exe</td><td>0</td><td/><td>-64670574</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME82</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME82</td><td>2052</td><td>LAUNCH~1.EXE|Launch jjs.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME83</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME83</td><td>2052</td><td>LAUNCH~1.EXE|Launch jp2launcher.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME84</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME84</td><td>2052</td><td>LAUNCH~1.EXE|Launch keytool.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME85</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME85</td><td>2052</td><td>LAUNCH~1.EXE|Launch kinit.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME86</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME86</td><td>2052</td><td>LAUNCH~1.EXE|Launch klist.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME87</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME87</td><td>2052</td><td>LAUNCH~1.EXE|Launch ktab.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME88</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME88</td><td>2052</td><td>LAUNCH~1.EXE|Launch orbd.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME89</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME89</td><td>2052</td><td>LAUNCH~1.EXE|Launch pack200.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME9</td><td>1033</td><td/><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME9</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityUpdater.exe</td><td>0</td><td/><td>321194960</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME90</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME90</td><td>2052</td><td>LAUNCH~1.EXE|Launch policytool.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME91</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME91</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmid.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME92</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME92</td><td>2052</td><td>LAUNCH~1.EXE|Launch rmiregistry.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME93</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME93</td><td>2052</td><td>LAUNCH~1.EXE|Launch servertool.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME94</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME94</td><td>2052</td><td>LAUNCH~1.EXE|Launch ssvagent.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME95</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME95</td><td>2052</td><td>LAUNCH~1.EXE|Launch tnameserv.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME96</td><td>1033</td><td/><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME96</td><td>2052</td><td>LAUNCH~1.EXE|Launch unpack200.exe</td><td>0</td><td/><td>-64668526</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME97</td><td>1033</td><td/><td>0</td><td/><td>-64658286</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME97</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityShell.exe</td><td>0</td><td/><td>-64658286</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME98</td><td>1033</td><td/><td>0</td><td/><td>-64656238</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME98</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityShell.vshost.exe</td><td>0</td><td/><td>-64656238</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME99</td><td>1033</td><td/><td>0</td><td/><td>-64656238</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME99</td><td>2052</td><td>LAUNCH~1.EXE|Launch adb.exe</td><td>0</td><td/><td>-64656238</td></row>
		<row><td>IDS_SQLBROWSE_INTRO</td><td>2052</td><td>从以下服务器列表中选择要连接的数据库服务器。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_DB</td><td>2052</td><td>从以下的编录名称列表中，选择您希望将其作为目标的数据库编录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_TEMPLATE</td><td>2052</td><td>[IS_SQLBROWSE_INTRO]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE</td><td>2052</td><td>浏览(&amp;R)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE_DB</td><td>2052</td><td>浏览(&amp;O)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_CATALOG</td><td>2052</td><td>数据库编录的名称(&amp;N)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_CONNECT</td><td>2052</td><td>连接时使用：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_DESC</td><td>2052</td><td>选择数据库服务器和验证方法。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_ID</td><td>2052</td><td>登录 ID(&amp;L)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_INTRO</td><td>2052</td><td>从以下列表中选择要安装的数据库服务器或单击“浏览”查看所有数据库服务器的列表。您还可以指定验证方法，确定使用当前证书或 SQL 登录 ID 和密码来验证您的登录。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_PSWD</td><td>2052</td><td>密码(&amp;P)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_SERVER</td><td>2052</td><td>&amp;数据库服务器：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_SERVER2</td><td>2052</td><td>您要安装到的数据库服务器(&amp;D)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_SQL</td><td>2052</td><td>使用以下登录 ID 和密码进行服务器身份验证(&amp;E)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_TITLE</td><td>2052</td><td>{&amp;MSSansBold8}数据库服务器</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLLOGIN_WIN</td><td>2052</td><td>当前用户的 Windows 验证证书(&amp;W)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLSCRIPT_INSTALLING</td><td>2052</td><td>正在执行 SQL 安装脚本...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SQLSCRIPT_UNINSTALLING</td><td>2052</td><td>正在执行 SQL 卸载脚本...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_STANDARD_USE_SETUPEXE</td><td>2052</td><td>直接启动 MSI 包无法运行此安装程序，必须要运行 setup.exe。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_Advertise</td><td>2052</td><td>{&amp;Tahoma8}将在第一次使用时安装。 （只有在该功能支持此选项时有效）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_AllInstalledLocal</td><td>2052</td><td>{&amp;Tahoma8}将全部安装到本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_CustomSetup</td><td>2052</td><td>{&amp;MSSansBold8}自定义安装提示</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_CustomSetupDescription</td><td>2052</td><td>{&amp;Tahoma8}“自定义安装”允许有选择地安装程序功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_IconInstallState</td><td>2052</td><td>{&amp;Tahoma8}紧邻该功能名称的图标显示该功能的安装状态。 单击该图标可下拉每个功能的安装状态菜单。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_InstallState</td><td>2052</td><td>{&amp;Tahoma8}此安装状态意味着该功能...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_Network</td><td>2052</td><td>{&amp;Tahoma8}将安装从网络中运行。 （只有在该功能支持此选项时有效）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_OK</td><td>2052</td><td>{&amp;Tahoma8}确定</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_SubFeaturesInstalledLocal</td><td>2052</td><td>{&amp;Tahoma8}将把一些子功能安装到本地硬盘驱动器上。 （只有在该功能带有子功能时有效）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_SetupTips_WillNotBeInstalled</td><td>2052</td><td>{&amp;Tahoma8}将不安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Available</td><td>2052</td><td>可用</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Bytes</td><td>2052</td><td>字节</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_CompilingFeaturesCost</td><td>2052</td><td>正在编译此功能的代价...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Differences</td><td>2052</td><td>差异</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_DiskSize</td><td>2052</td><td>磁盘空间</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureCompletelyRemoved</td><td>2052</td><td>此功能将被全部删除。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureContinueNetwork</td><td>2052</td><td>此功能将继续从网络中运行</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureFreeSpace</td><td>2052</td><td>此功能释放硬盘驱动器上的 [1] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD</td><td>2052</td><td>此功能及所有子功能安装后将从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD2</td><td>2052</td><td>此功能安装后将从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal</td><td>2052</td><td>此功能及所有子功能将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal2</td><td>2052</td><td>此功能将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork</td><td>2052</td><td>此功能及所有子功能安装后将从网络上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork2</td><td>2052</td><td>此功能安装后将从网络中运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledRequired</td><td>2052</td><td>此功能将在需要时安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired</td><td>2052</td><td>此功能将被设置为在需要时安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired2</td><td>2052</td><td>此功能在需要时可进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureLocal</td><td>2052</td><td>此功能将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureLocal2</td><td>2052</td><td>此功能将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork</td><td>2052</td><td>此功能安装后将从网络中运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork2</td><td>2052</td><td>此功能可从网络中运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureNotAvailable</td><td>2052</td><td>此功能将不可用。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD</td><td>2052</td><td>此功能安装后将从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD2</td><td>2052</td><td>此功能可从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRemainLocal</td><td>2052</td><td>此功能将保留在本地硬盘驱动器中。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRemoveNetwork</td><td>2052</td><td>此功能将从本地硬盘驱动器中删除，但仍可以从网络中运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedCD</td><td>2052</td><td>此功能将从本地硬盘驱动器中删除，但仍可以从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedUnlessRequired</td><td>2052</td><td>此功能将从本地硬盘驱动器中删除，但需要时将进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRequiredSpace</td><td>2052</td><td>此功能需要硬盘驱动器上的 [1] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureRunFromCD</td><td>2052</td><td>此功能将继续从光盘上运行</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree</td><td>2052</td><td>此功能释放硬盘驱动器上的 [1] 。 选定了 [3] 子功能的 [2] 。 该子功能释放硬盘驱动器上的 [4] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree2</td><td>2052</td><td>此功能释放硬盘驱动器上的 [1] 。 选定了 [3] 子功能的 [2] 。 该子功能需要硬盘驱动器上的 [4] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree3</td><td>2052</td><td>此功能需要硬盘驱动器上的 [1] 。 选定了 [3] 子功能的 [2] 。 该子功能释放硬盘驱动器上的 [4] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree4</td><td>2052</td><td>此功能需要硬盘驱动器上的 [1] 。 选定了 [3] 子功能的 [2]。 该子功能需要硬盘驱动器上的 [4] 。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureUnavailable</td><td>2052</td><td>此功能将不安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureUninstallNoNetwork</td><td>2052</td><td>此功能将被全部卸载，您将无法从网络中运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureWasCD</td><td>2052</td><td>此功能可从光盘上运行，但被设置为需要时才进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureWasCDLocal</td><td>2052</td><td>此功能可从光盘上运行，但将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkInstalled</td><td>2052</td><td>此功能从网络中运行，但在需要时将进行安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkLocal</td><td>2052</td><td>此功能从网络中运行，但将安装在本地硬盘驱动器上。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_FeatureWillBeUninstalled</td><td>2052</td><td>此功能将全部卸载，您将无法从光盘上运行。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Folder</td><td>2052</td><td>文件夹|新文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_GB</td><td>2052</td><td>GB</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_KB</td><td>2052</td><td>KB</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_MB</td><td>2052</td><td>MB</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Required</td><td>2052</td><td>要求</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_TimeRemaining</td><td>2052</td><td>剩余时间：{[1] 分 }{[2] 秒}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS_UITEXT_Volume</td><td>2052</td><td>卷</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__AgreeToLicense_0</td><td>2052</td><td>{&amp;Tahoma8}我不接受该许可证协议中的条款(&amp;D)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__AgreeToLicense_1</td><td>2052</td><td>{&amp;Tahoma8}我接受该许可证协议中的条款(&amp;A)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DatabaseFolder_ChangeFolder</td><td>2052</td><td>单击“下一步”安装到此文件夹，或单击“更改”安装到不同的文件夹。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseDir</td><td>2052</td><td>[DATABASEDIR]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseFolder</td><td>2052</td><td>{&amp;MSSansBold8}数据库文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DestinationFolder_Change</td><td>2052</td><td>{&amp;Tahoma8}更改(&amp;C)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DestinationFolder_ChangeFolder</td><td>2052</td><td>{&amp;Tahoma8}单击“下一步”安装到此文件夹，或单击“更改”安装到不同的文件夹。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DestinationFolder_DestinationFolder</td><td>2052</td><td>{&amp;MSSansBold8}目的地文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DestinationFolder_InstallTo</td><td>2052</td><td>{&amp;Tahoma8}将 [ProductName] 安装到：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DisplayName_Custom</td><td>2052</td><td>自定义</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DisplayName_Minimal</td><td>2052</td><td>最小化安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__DisplayName_Typical</td><td>2052</td><td>典型</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_11</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_4</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_8</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_BrowseDestination</td><td>2052</td><td>{&amp;Tahoma8}浏览目的地文件夹。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_ChangeDestination</td><td>2052</td><td>{&amp;MSSansBold8}更改当前目的地文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_CreateFolder</td><td>2052</td><td>创建新文件夹|</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_FolderName</td><td>2052</td><td>{&amp;Tahoma8}文件夹名称(&amp;F)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_LookIn</td><td>2052</td><td>{&amp;Tahoma8}搜索范围(&amp;L)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallBrowse_UpOneLevel</td><td>2052</td><td>上一级|</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_ServerImage</td><td>2052</td><td>{&amp;Tahoma8}InstallShield(R) Wizard 将在指定的网络位置创建 [ProductName] 的服务器映象。 要继续，请单击“下一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_Wizard</td><td>2052</td><td>{&amp;TahomaBold10}欢迎使用 [ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_Change</td><td>2052</td><td>{&amp;Tahoma8}更改(&amp;C)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_EnterNetworkLocation</td><td>2052</td><td>{&amp;Tahoma8}输入网络位置或单击“更改”以浏览网络位置。  单击“安装”在指定的网络位置创建 [ProductName] 的服务器映象，或单击“取消”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_Install</td><td>2052</td><td>{&amp;Tahoma8}安装(&amp;I)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocation</td><td>2052</td><td>{&amp;Tahoma8}网络位置(&amp;N)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocationFormatted</td><td>2052</td><td>{&amp;MSSansBold8}网络位置</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsAdminInstallPoint_SpecifyNetworkLocation</td><td>2052</td><td>{&amp;Tahoma8}为产品的服务器映象指定网络位置。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseButton</td><td>2052</td><td>浏览(&amp;B)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_11</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_4</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_8</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_BrowseDestFolder</td><td>2052</td><td>{&amp;Tahoma8}浏览目的地文件夹。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_ChangeCurrentFolder</td><td>2052</td><td>{&amp;MSSansBold8}更改当前目的地文件夹</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_CreateFolder</td><td>2052</td><td>创建新文件夹|</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_FolderName</td><td>2052</td><td>{&amp;Tahoma8}文件夹名称(&amp;F)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_LookIn</td><td>2052</td><td>{&amp;Tahoma8}搜索范围(&amp;L)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_OK</td><td>2052</td><td>{&amp;Tahoma8}确定</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseFolderDlg_UpOneLevel</td><td>2052</td><td>上一级|</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseForAccount</td><td>2052</td><td>浏览用户帐号</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseGroup</td><td>2052</td><td>选择用户列表组</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsBrowseUsernameTitle</td><td>2052</td><td>选择用户名</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCancelDlg_ConfirmCancel</td><td>2052</td><td>{&amp;Tahoma8}要取消 [ProductName] 安装吗？</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCancelDlg_No</td><td>2052</td><td>{&amp;Tahoma8}否(&amp;N)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCancelDlg_Yes</td><td>2052</td><td>{&amp;Tahoma8}是(&amp;Y)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsConfirmPassword</td><td>2052</td><td>确认密码(&amp;F):</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCreateNewUserTitle</td><td>2052</td><td>新用户信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCreateUserBrowse</td><td>2052</td><td>新用户信息(&amp;E)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Change</td><td>2052</td><td>{&amp;Tahoma8}更改(&amp;A)...</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_ClickFeatureIcon</td><td>2052</td><td>{&amp;Tahoma8}单击下面列表内的图标以更改功能的安装方式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_CustomSetup</td><td>2052</td><td>{&amp;MSSansBold8}自定义安装</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureDescription</td><td>2052</td><td>{&amp;Tahoma8}功能说明</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeaturePath</td><td>2052</td><td>{&amp;Tahoma8}&lt;selected feature path&gt;</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureSize</td><td>2052</td><td>{&amp;Tahoma8}功能大小</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Help</td><td>2052</td><td>{&amp;Tahoma8}帮助(&amp;H)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_InstallTo</td><td>2052</td><td>{&amp;Tahoma8}安装到：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_MultilineDescription</td><td>2052</td><td>{&amp;Tahoma8}当前选定项目的多行说明</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_SelectFeatures</td><td>2052</td><td>{&amp;Tahoma8}选择要安装的程序功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Space</td><td>2052</td><td>{&amp;Tahoma8}空间(&amp;S)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsDiskSpaceDlg_DiskSpace</td><td>2052</td><td>{&amp;Tahoma8}安装所需的磁盘空间超出了可用的磁盘空间。</td><td>0</td><td>	</td><td>-2111467008</td></row>
		<row><td>IDS__IsDiskSpaceDlg_HighlightedVolumes</td><td>2052</td><td>{&amp;Tahoma8}突出显示的卷没有足够的磁盘空间可供当前选定的功能使用。 可以从突出显示的卷中删除文件，或选择安装较少的功能到本地驱动器，还可选择不同的目的地驱动器。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsDiskSpaceDlg_Numbers</td><td>2052</td><td>{&amp;Tahoma8}{120}{70}{70}{70}{70}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OK</td><td>2052</td><td>{&amp;Tahoma8}确定(&amp;O)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OutOfDiskSpace</td><td>2052</td><td>{&amp;MSSansBold8}磁盘空间不足</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsDomainOrServer</td><td>2052</td><td>域或服务器(&amp;D):</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_Abort</td><td>2052</td><td>{&amp;Tahoma8}放弃(&amp;A)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_ErrorText</td><td>2052</td><td>{&amp;Tahoma8}&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_Ignore</td><td>2052</td><td>{&amp;Tahoma8}忽略(&amp;I)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_InstallerInfo</td><td>2052</td><td>[ProductName] 安装程序信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_NO</td><td>2052</td><td>{&amp;Tahoma8}否(&amp;N)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_OK</td><td>2052</td><td>{&amp;Tahoma8}确定(&amp;O)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_Retry</td><td>2052</td><td>{&amp;Tahoma8}重试(&amp;R)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsErrorDlg_Yes</td><td>2052</td><td>{&amp;Tahoma8}是(&amp;Y)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_Finish</td><td>2052</td><td>{&amp;Tahoma8}完成(&amp;F)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_InstallSuccess</td><td>2052</td><td>{&amp;Tahoma8}InstallShield Wizard 成功地安装了 [ProductName] 。 单击“完成”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_LaunchProgram</td><td>2052</td><td>启动程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_ShowReadMe</td><td>2052</td><td>显示自述文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_UninstallSuccess</td><td>2052</td><td>{&amp;Tahoma8}InstallShield Wizard 成功地卸载了 [ProductName] 。 单击“完成”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_Update_InternetConnection</td><td>2052</td><td>只要与因特网相连就可以确保得到最新的更新。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_Update_PossibleUpdates</td><td>2052</td><td>在您购买 [ProductName] 后，有些程序文件可能已经更新。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_Update_SetupFinished</td><td>2052</td><td>安装程序已完成对 [ProductName] 的安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_Update_YesCheckForUpdates</td><td>2052</td><td>是(&amp;Y)，安装完成后检查程序更新（建议）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsExitDialog_WizardCompleted</td><td>2052</td><td>{&amp;TahomaBold10} InstallShield Wizard 完成</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_ClickFinish</td><td>2052</td><td>{&amp;Tahoma8}单击“完成”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_Finish</td><td>2052</td><td>{&amp;Tahoma8}完成(&amp;F)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_KeepOrRestore</td><td>2052</td><td>{&amp;Tahoma8}可以保留系统中现存的已安装内容，以后再继续此安装过程，也可以将系统恢复到安装前的原始状态。</td><td>0</td><td>	</td><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_NotModified</td><td>2052</td><td>{&amp;Tahoma8}系统未被修改。 要再次完成安装过程，请重新运行安装程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_RestoreOrContinueLater</td><td>2052</td><td>{&amp;Tahoma8}单击“恢复”或“以后继续”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_WizardCompleted</td><td>2052</td><td>{&amp;TahomaBold10} InstallShield Wizard 完成</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFatalError_WizardInterrupted</td><td>2052</td><td>{&amp;Tahoma8}在 [ProductName] 完整安装之前向导已中断。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_DiskSpaceRequirements</td><td>2052</td><td>{&amp;MSSansBold8}磁盘空间需求</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_Numbers</td><td>2052</td><td>{&amp;Tahoma8}{120}{70}{70}{70}{70}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_OK</td><td>2052</td><td>{&amp;Tahoma8}确定</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_SpaceRequired</td><td>2052</td><td>{&amp;Tahoma8}安装选定功能所需的磁盘空间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_VolumesTooSmall</td><td>2052</td><td>{&amp;Tahoma8}突出显示的卷没有足够的磁盘空间可供当前选定的功能使用。 可以从突出显示的卷中删除文件，或选择安装较少的功能到本地驱动器，还可选择不同的目的地驱动器。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_ApplicationsUsingFiles</td><td>2052</td><td>{&amp;Tahoma8}下列应用程序正在使用此安装程序需要更新的文件。 关闭这些应用程序并单击“重试”继续。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_Exit</td><td>2052</td><td>{&amp;Tahoma8}退出(&amp;E)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUse</td><td>2052</td><td>{&amp;MSSansBold8}正在使用的文件</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUseMessage</td><td>2052</td><td>{&amp;Tahoma8}某些需要更新的文件当前正在使用。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_Ignore</td><td>2052</td><td>{&amp;Tahoma8}忽略(&amp;I)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsFilesInUse_Retry</td><td>2052</td><td>{&amp;Tahoma8}重试(&amp;R)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsGroup</td><td>2052</td><td>用户列表组(&amp;U):</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsGroupLabel</td><td>2052</td><td>用户列表组(&amp;O):</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsInitDlg_1</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsInitDlg_2</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsInitDlg_PreparingWizard</td><td>2052</td><td>{&amp;Tahoma8}安装程序正在准备 InstallShield Wizard，InstallShield Wizard 将引导您完成程序安装过程，请稍候。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsInitDlg_WelcomeWizard</td><td>2052</td><td>{&amp;TahomaBold10}欢迎使用 [ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLicenseDlg_LicenseAgreement</td><td>2052</td><td>{&amp;MSSansBold8}许可证协议</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLicenseDlg_ReadLicenseAgreement</td><td>2052</td><td>{&amp;Tahoma8}请仔细阅读下面的许可证协议。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLogonInfoDescription</td><td>2052</td><td>指定此应用程序使用的用户帐号。 用户帐号的格式必须为“域\用户名”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLogonInfoTitle</td><td>2052</td><td>{&amp;MSSansBold8}登录信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLogonInfoTitleDescription</td><td>2052</td><td>指定用户名和密码</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsLogonNewUserDescription</td><td>2052</td><td>选取以下按钮，指定有关安装过程中要创建的新用户信息。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_ChangeFeatures</td><td>2052</td><td>{&amp;Tahoma8}更改要安装的程序功能。 此选项可显示“自定义选择”对话框，在其中您可以更改安装功能的方式。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_MaitenanceOptions</td><td>2052</td><td>{&amp;Tahoma8}修改、修复或删除程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_Modify</td><td>2052</td><td>{&amp;TahomaBold10}修改(&amp;M)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_ProgramMaintenance</td><td>2052</td><td>{&amp;MSSansBold8}程序维护</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_Remove</td><td>2052</td><td>{&amp;TahomaBold10}删除(&amp;R)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_RemoveProductName</td><td>2052</td><td>{&amp;Tahoma8}从计算机中删除 [ProductName]。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_Repair</td><td>2052</td><td>{&amp;TahomaBold10}修复(&amp;P)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceDlg_RepairMessage</td><td>2052</td><td>{&amp;Tahoma8}修复程序中的错误。 通过此选项您可修复缺少或损坏的文件、快捷方式和注册表项。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription</td><td>2052</td><td>{&amp;Tahoma8}InstallShield(R) Wizard 允许修改、修复或删除 [ProductName] 。 要继续，请单击“下一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMaintenanceWelcome_WizardWelcome</td><td>2052</td><td>{&amp;TahomaBold10}欢迎使用 [ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles</td><td>2052</td><td>下列应用程序正在使用此安装程序需要更新的文件。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_CloseRestart</td><td>2052</td><td>自动关闭并试图重新启动应用程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_RebootAfter</td><td>2052</td><td>不要关闭应用程序。（将需要重新启动。）</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsPatchDlg_PatchClickUpdate</td><td>2052</td><td>InstallShield(R) Wizard 将在您的计算机中安装 [ProductName] 的修补程序。  要继续，请单击“更新”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsPatchDlg_PatchWizard</td><td>2052</td><td>[ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsPatchDlg_Update</td><td>2052</td><td>更新(&amp;U) &gt;</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsPatchDlg_WelcomePatchWizard</td><td>2052</td><td>{&amp;TahomaBold10}欢迎使用 [ProductName] 的修补程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_2</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_Hidden</td><td>2052</td><td>{&amp;Tahoma8}（现在隐藏）</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_HiddenTimeRemaining</td><td>2052</td><td>{&amp;Tahoma8}（现在隐藏）估计剩余时间：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_InstallingProductName</td><td>2052</td><td>{&amp;MSSansBold8}正在安装 [ProductName]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_ProgressDone</td><td>2052</td><td>已完成进度</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_SecHidden</td><td>2052</td><td>{&amp;Tahoma8}（现在隐藏）秒</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_Status</td><td>2052</td><td>{&amp;Tahoma8}状态：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_Uninstalling</td><td>2052</td><td>{&amp;MSSansBold8}正在卸载 [ProductName]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures</td><td>2052</td><td>{&amp;Tahoma8}正在卸载您选择的程序功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures2</td><td>2052</td><td>{&amp;Tahoma8}正在安装您选择的程序功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall</td><td>2052</td><td>{&amp;Tahoma8}InstallShield Wizard 正在卸载 [ProductName] ，请稍候。 这需要几分钟的时间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall2</td><td>2052</td><td>{&amp;Tahoma8}InstallShield Wizard 正在安装 [ProductName] ，请稍候。 这需要几分钟的时间。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsReadmeDlg_Cancel</td><td>2052</td><td>取消(&amp;C)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsReadmeDlg_PleaseReadInfo</td><td>2052</td><td>请仔细阅读下面的自述文件信息。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsReadmeDlg_ReadMeInfo</td><td>2052</td><td>{&amp;MSSansBold8}自述文件信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_16</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_Anyone</td><td>2052</td><td>{&amp;Tahoma8}使用本机的任何人(&amp;A)（所有用户）</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_CustomerInformation</td><td>2052</td><td>{&amp;MSSansBold8}用户信息</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_InstallFor</td><td>2052</td><td>{&amp;Tahoma8}此应用程序的使用者：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_OnlyMe</td><td>2052</td><td>{&amp;Tahoma8}仅限本人(&amp;M) ([USERNAME])</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_Organization</td><td>2052</td><td>{&amp;Tahoma8}单位(&amp;O)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_PleaseEnterInfo</td><td>2052</td><td>{&amp;Tahoma8}请输入您的信息。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_SerialNumber</td><td>2052</td><td>序列号(&amp;S)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma50</td><td>2052</td><td>{50}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma80</td><td>2052</td><td>{80}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsRegisterUserDlg_UserName</td><td>2052</td><td>{&amp;Tahoma8}用户姓名(&amp;U)：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsResumeDlg_ResumeSuspended</td><td>2052</td><td>{&amp;Tahoma8}InstallShield(R) Wizard 将完成计算机上挂起的 [ProductName] 安装过程。 要继续，请单击“下一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsResumeDlg_Resuming</td><td>2052</td><td>{&amp;TahomaBold10}继续执行 [ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsResumeDlg_WizardResume</td><td>2052</td><td>{&amp;Tahoma8}InstallShield(R) Wizard 将在计算机上完成 [ProductName] 的安装过程。 要继续，请单击“下一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSelectDomainOrServer</td><td>2052</td><td>选择域或服务器</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSelectDomainUserInstructions</td><td>2052</td><td>使用浏览按钮选择域\服务器和用户名。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupComplete_ShowMsiLog</td><td>2052</td><td>显示 Windows Installer 日志</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_13</td><td>2052</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_AllFeatures</td><td>2052</td><td>{&amp;Tahoma8}将安装所有的程序功能。 （需要的磁盘空间最大）。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseFeatures</td><td>2052</td><td>{&amp;Tahoma8}选择要安装的程序功能和将要安装的位置。 建议高级用户使用。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseSetupType</td><td>2052</td><td>{&amp;Tahoma8}选择最适合自己需要的安装类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Complete</td><td>2052</td><td>{&amp;MSSansBold8}完整安装(&amp;O)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Custom</td><td>2052</td><td>{&amp;MSSansBold8}自定义(&amp;S)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Minimal</td><td>2052</td><td>{&amp;MSSansBold8}最小化安装(&amp;M)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_MinimumFeatures</td><td>2052</td><td>{&amp;Tahoma8}将安装最低要求的功能。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SelectSetupType</td><td>2052</td><td>{&amp;Tahoma8}请选择一个安装类型。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SetupType</td><td>2052</td><td>{&amp;MSSansBold8}安装类型</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Typical</td><td>2052</td><td>{&amp;MSSansBold8}典型(&amp;T)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_ClickFinish</td><td>2052</td><td>{&amp;Tahoma8}单击“完成”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_Finish</td><td>2052</td><td>{&amp;Tahoma8}完成(&amp;F)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_KeepOrRestore</td><td>2052</td><td>{&amp;Tahoma8}可以保留系统中现存的已安装内容，以后再继续此安装过程，也可以将系统恢复到安装前的原始状态。</td><td>0</td><td>	</td><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_NotModified</td><td>2052</td><td>{&amp;Tahoma8}系统未被修改。 以后要再完成安装过程，请重新运行安装程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_RestoreOrContinue</td><td>2052</td><td>{&amp;Tahoma8}单击“恢复”或“以后继续”退出该向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_WizardCompleted</td><td>2052</td><td>{&amp;TahomaBold10} InstallShield Wizard 完成</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserExit_WizardInterrupted</td><td>2052</td><td>{&amp;Tahoma8}在 [ProductName] 完整安装之前向导已中断。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsUserNameLabel</td><td>2052</td><td>用户名(&amp;U):</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_BackOrCancel</td><td>2052</td><td>{&amp;Tahoma8}要查看或更改任何安装设置，请单击“上一步”。 单击“取消”退出向导。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ClickInstall</td><td>2052</td><td>{&amp;Tahoma8}单击“安装”开始安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Company</td><td>2052</td><td>公司： [COMPANYNAME]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_CurrentSettings</td><td>2052</td><td>当前设置：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_DestFolder</td><td>2052</td><td>目的地文件夹：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Install</td><td>2052</td><td>{&amp;Tahoma8}安装(&amp;I)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Installdir</td><td>2052</td><td>[INSTALLDIR]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ModifyReady</td><td>2052</td><td>{&amp;MSSansBold8}已做好修改程序的准备</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyInstall</td><td>2052</td><td>{&amp;MSSansBold8}已做好安装程序的准备</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyRepair</td><td>2052</td><td>{&amp;MSSansBold8}已做好修复程序的准备</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SelectedSetupType</td><td>2052</td><td>[SelectedSetupType]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Serial</td><td>2052</td><td>序列号： [ISX_SERIALNUM]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SetupType</td><td>2052</td><td>安装类型：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserInfo</td><td>2052</td><td>用户信息：</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserName</td><td>2052</td><td>姓名： [USERNAME]</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyReadyDlg_WizardReady</td><td>2052</td><td>{&amp;Tahoma8}向导准备开始安装。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram</td><td>2052</td><td>{&amp;Tahoma8}您已经选择从系统中删除此程序。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickBack</td><td>2052</td><td>{&amp;Tahoma8}要查看或更改任何设置，请单击“上一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickRemove</td><td>2052</td><td>{&amp;Tahoma8}单击“删除”从计算机中删除 [ProductName] 。 删除后此程序将不能再使用。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_Remove</td><td>2052</td><td>{&amp;Tahoma8}删除(&amp;R)</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_RemoveProgram</td><td>2052</td><td>{&amp;MSSansBold8}删除程序</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsWelcomeDlg_InstallProductName</td><td>2052</td><td>{&amp;Tahoma8}InstallShield(R) Wizard 将要在您的计算机中安装 [ProductName] 。 要继续，请单击“下一步”。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsWelcomeDlg_WarningCopyright</td><td>2052</td><td>警告：本程序受版权法和国际条约的保护。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__IsWelcomeDlg_WelcomeProductName</td><td>2052</td><td>{&amp;TahomaBold10}欢迎使用 [ProductName] InstallShield Wizard</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__TargetReq_DESC_COLOR</td><td>2052</td><td>对于运行 [ProductName] 系统颜色设置不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__TargetReq_DESC_OS</td><td>2052</td><td>对于运行 [ProductName] 操作系统不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__TargetReq_DESC_PROCESSOR</td><td>2052</td><td>对于运行 [ProductName] 处理器不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__TargetReq_DESC_RAM</td><td>2052</td><td>对于运行 [ProductName] 内存量不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>IDS__TargetReq_DESC_RESOLUTION</td><td>2052</td><td>对于运行 [ProductName] 屏幕分辨率不足。</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>ID_STRING1</td><td>2052</td><td>http://www.天宇宁达.com</td><td>0</td><td/><td>-64666382</td></row>
		<row><td>ID_STRING2</td><td>2052</td><td>天宇宁达</td><td>0</td><td/><td>-64666382</td></row>
		<row><td>ID_STRING3</td><td>2052</td><td>奇点取证</td><td>0</td><td/><td>-2111467008</td></row>
		<row><td>ID_STRING4</td><td>1033</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>-2111475104</td></row>
		<row><td>ID_STRING4</td><td>2052</td><td>LAUNCH~1.EXE|Launch SingularityForensic.exe</td><td>0</td><td/><td>-2111475104</td></row>
		<row><td>ID_STRING5</td><td>1033</td><td>星云手机取证分析系统</td><td>0</td><td/><td>321248240</td></row>
		<row><td>ID_STRING5</td><td>2052</td><td>星云手机取证分析系统</td><td>0</td><td/><td>321248240</td></row>
		<row><td>ID_STRING6</td><td>1033</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1000718867</td></row>
		<row><td>ID_STRING6</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1000718867</td></row>
		<row><td>ID_STRING7</td><td>1033</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1948633517</td></row>
		<row><td>ID_STRING7</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>1948633517</td></row>
		<row><td>ID_STRING8</td><td>1033</td><td>流火手机取证分析系统</td><td>0</td><td/><td>-64672526</td></row>
		<row><td>ID_STRING8</td><td>2052</td><td>流火手机取证分析系统</td><td>0</td><td/><td>-64672526</td></row>
		<row><td>IIDS_UITEXT_FeatureUninstalled</td><td>2052</td><td>系统将不安装此功能。</td><td>0</td><td/><td>-2111467008</td></row>
	</table>

	<table name="ISSwidtagProperty">
		<col key="yes" def="s72">Name</col>
		<col def="s0">Value</col>
		<row><td>UniqueId</td><td>A249E242-9DC4-4C6D-A298-6465F0B1BAC6</td></row>
	</table>

	<table name="ISTargetImage">
		<col key="yes" def="s13">UpgradedImage_</col>
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="i2">Order</col>
		<col def="I4">Flags</col>
		<col def="i2">IgnoreMissingFiles</col>
	</table>

	<table name="ISUpgradeMsiItem">
		<col key="yes" def="s72">UpgradeItem</col>
		<col def="s0">ObjectSetupPath</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="i2">ISAttributes</col>
	</table>

	<table name="ISUpgradedImage">
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="s8">Family</col>
	</table>

	<table name="ISVirtualDirectory">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualPackage">
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRegistry">
		<col key="yes" def="s72">Registry_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRelease">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualShortcut">
		<col key="yes" def="s72">Shortcut_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISWSEWrap">
		<col key="yes" def="s72">Action_</col>
		<col key="yes" def="s72">Name</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISXmlElement">
		<col key="yes" def="s72">ISXmlElement</col>
		<col def="s72">ISXmlFile_</col>
		<col def="S72">ISXmlElement_Parent</col>
		<col def="L0">XPath</col>
		<col def="L0">Content</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlElementAttrib">
		<col key="yes" def="s72">ISXmlElementAttrib</col>
		<col key="yes" def="s72">ISXmlElement_</col>
		<col def="L255">Name</col>
		<col def="L0">Value</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlFile">
		<col key="yes" def="s72">ISXmlFile</col>
		<col def="l255">FileName</col>
		<col def="s72">Component_</col>
		<col def="s72">Directory</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">SelectionNamespaces</col>
		<col def="S255">Encoding</col>
	</table>

	<table name="ISXmlLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col def="S255">Element</col>
		<col def="S255">Attribute</col>
		<col def="I2">ISAttributes</col>
	</table>

	<table name="Icon">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">ISIconIndex</col>
		<row><td>ARPPRODUCTICON.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\setupicon.ico</td><td>0</td></row>
		<row><td>Shortcut_8CBEA929EFDD431C9E909E92750734E0.exe</td><td/><td>C:\Program Files (x86)\InstallShield\2015LE\Redist\Language Independent\OS Independent\uninstall.ico</td><td>0</td></row>
		<row><td>SingularityForensi_01F2305CA18444A7B12DE66230DAF101.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityForensi_0671913A4759496FB5BE0B830068DCA0.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_07B750D0F5B6492B812E5CE410ADE9C0.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_2CE6D2F9C814494D9DD714BDDA6BB56C.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_2D9D7D16FEE04134AEB479649A633BE4.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_36006126B3E042B7ADB39B40505D41C5.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityForensi_482DABC335644483836F446E4891F749.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_6BC7D044CB6A410D95C3D2D5F8D94355.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_6F46163713A74A91B5560AA4E92A74E2.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_742BD338492D4EC2AFF2E63BEA2DD388.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityForensi_9383B5F10E3A4875867D4A5F0A054D9C.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_9F370B70C3B547718891EFF68B3E0153.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityForensi_A15F8C62BF1C4D50BC20D850254B4EAA.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_A5F1A20A1F964535900B08411A062BD0.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_A73AD6A2C7C943ECB29CB50ECC0227A9.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_CE4821A75901481C88B7DD1B794F31BE.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityForensi_CF0C0E6B36E3486BADE8C9CAEAF5986D.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_E6BEAF5012D940829F0E7CDC0E12B1E7.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_F4DA8F560B4543DB87ACB573CBFD9DB8.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityForensi_F8664244DA2041C6A185B3B75281688C.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityForensic.exe</td><td>0</td></row>
		<row><td>SingularityShell.e_494882FB7120450DB282E6DE8B7837B9.exe</td><td/><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.exe</td><td>0</td></row>
		<row><td>SingularityShell.e_D5B81EE8FF80445EBA74BF76F37FBDAA.exe</td><td/><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.exe</td><td>0</td></row>
		<row><td>SingularityShell.e_FFC381C5028B4D749A58544295AF5368.exe</td><td/><td>D:\SingularitySolution\SingularityShell\bin\Release\WiBu\WiBu\SingularityShell.exe</td><td>0</td></row>
		<row><td>SingularityShell.v_C533268128A3481585EF2FEC8FAC9A2A.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityUpdater_06FD94BCC08847D185B844133C6E17D8.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityUpdater_42914814937B4030871649230B80B391.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_5459394EAC6744E99B5FABDCE50A94F1.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_6981A5F24FFE431B8DD9E4E9BB47E38A.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_7F5C7025F3E0476D9E932E47DCC5A8BA.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityUpdater_B5842CA9578D4BEDB02560DB01BA1BD2.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>SingularityUpdater_C6D3ED0898A84F9AA9FCD6EF4A384954.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_D2646172A9FC46BFB06F9DB936612AE5.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\WiBu\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_F31738781B534DC287CAB928DC96FF01.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\NetReactor\SingularityUpdater.exe</td><td>0</td></row>
		<row><td>SingularityUpdater_FF4762CBA7E349F9A2AF1A337C9EBC81.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>adb.exe_ED8BA18441A2428CBE39C9317E09E917.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>jabswitch.exe_3B2A263E182840C4AAF7312FBBD55A03.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>java.exe_0985538674E24723B7AD0AE85F175D1C.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\jre\bin\java.exe</td><td>0</td></row>
		<row><td>java_rmi.exe_C1057F87E7C34C8494BE061DBBCBD459.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>javacpl.exe_F067CDA91C7B4B1396674EDF3673747F.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\jre\bin\javacpl.exe</td><td>0</td></row>
		<row><td>javaw.exe_E7A02D3D04E94341A3316C19EDD7E81E.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\jre\bin\javaw.exe</td><td>0</td></row>
		<row><td>javaws.exe_118CAE44AA96489FBEC9C4389C9D4F7D.exe</td><td/><td>D:\SingularitySolution\SingularityForensic\bin\Release\jre\bin\javaws.exe</td><td>0</td></row>
		<row><td>jjs.exe_CF7A68CEC1E1497F913A874118841956.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>jp2launcher.exe_F05A53F8AE3F439FB9A85FE1E87F7CC3.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>keytool.exe_2DFD04B9A55646A6BF77BF93FE5CE68C.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>kinit.exe_C592DA88B0244AA5B14D0699E2F92586.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>klist.exe_3D105EE0842444C4AAEBA2C969E8658D.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>ktab.exe_D9C4E4F658204B76A00187FE2C7BCAF3.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>orbd.exe_EA36CE44264F46908BDB1C040D3C6837.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>pack200.exe_FC0FBFFA9FB24C97B15BF08D2BA80FB9.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>policytool.exe_6DC415BB980A42158116C05CF25ABB3B.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>rmid.exe_471843DC3BE94A0C9D81328091E91770.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>rmiregistry.exe_D415100A09CD4BB2BC9F4ECA1DC5A2B7.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>servertool.exe_CC94F8C198D240C180EC9DD152AF125C.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>ssvagent.exe_758D40403F334A63AFE19AE9A2812AEE.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>tnameserv.exe_494F47520D4E45ABBD8882810C59425D.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>unpack200.exe_4994EA35A1514E5582F7598B1176BB51.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
	</table>

	<table name="IniFile">
		<col key="yes" def="s72">IniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l255">Section</col>
		<col def="l128">Key</col>
		<col def="s255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="IniLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s255">FileName</col>
		<col def="s96">Section</col>
		<col def="s128">Key</col>
		<col def="I2">Field</col>
		<col def="I2">Type</col>
	</table>

	<table name="InstallExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AllocateRegistrySpace</td><td>NOT Installed</td><td>1550</td><td>AllocateRegistrySpace</td><td/></row>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>BindImage</td><td/><td>4300</td><td>BindImage</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateFolders</td><td/><td>3700</td><td>CreateFolders</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>DeleteServices</td><td>VersionNT</td><td>2000</td><td>DeleteServices</td><td/></row>
		<row><td>DuplicateFiles</td><td/><td>4210</td><td>DuplicateFiles</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td>NOT ISSETUPDRIVEN</td><td>420</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>Not Installed And Not ISRUNSETUPTYPEADDLOCALEVENT</td><td>1050</td><td>ISRunSetupTypeAddLocalEvent</td><td/></row>
		<row><td>ISSelfRegisterCosting</td><td/><td>2201</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td/><td>5601</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td/><td>6601</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td/><td>2202</td><td/><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1501</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallODBC</td><td/><td>5400</td><td>InstallODBC</td><td/></row>
		<row><td>InstallServices</td><td>VersionNT</td><td>5800</td><td>InstallServices</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1010</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>MoveFiles</td><td/><td>3800</td><td>MoveFiles</td><td/></row>
		<row><td>MsiConfigureServices</td><td>VersionMsi &gt;= "5.00"</td><td>5850</td><td>MSI5 MsiConfigureServices</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>MsiUnpublishAssemblies</td><td/><td>1750</td><td>MsiUnpublishAssemblies</td><td/></row>
		<row><td>PatchFiles</td><td/><td>4090</td><td>PatchFiles</td><td/></row>
		<row><td>ProcessComponents</td><td/><td>1600</td><td>ProcessComponents</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterComPlus</td><td/><td>5700</td><td>RegisterComPlus</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterFonts</td><td/><td>5300</td><td>RegisterFonts</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProduct</td><td/><td>6100</td><td>RegisterProduct</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>5500</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>RegisterUser</td><td/><td>6000</td><td>RegisterUser</td><td/></row>
		<row><td>RemoveDuplicateFiles</td><td/><td>3400</td><td>RemoveDuplicateFiles</td><td/></row>
		<row><td>RemoveEnvironmentStrings</td><td/><td>3300</td><td>RemoveEnvironmentStrings</td><td/></row>
		<row><td>RemoveExistingProducts</td><td/><td>1410</td><td>RemoveExistingProducts</td><td/></row>
		<row><td>RemoveFiles</td><td/><td>3500</td><td>RemoveFiles</td><td/></row>
		<row><td>RemoveFolders</td><td/><td>3600</td><td>RemoveFolders</td><td/></row>
		<row><td>RemoveIniValues</td><td/><td>3100</td><td>RemoveIniValues</td><td/></row>
		<row><td>RemoveODBC</td><td/><td>2400</td><td>RemoveODBC</td><td/></row>
		<row><td>RemoveRegistryValues</td><td/><td>2600</td><td>RemoveRegistryValues</td><td/></row>
		<row><td>RemoveShortcuts</td><td/><td>3200</td><td>RemoveShortcuts</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>850</td><td>ResolveSource</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
		<row><td>SelfRegModules</td><td/><td>5600</td><td>SelfRegModules</td><td/></row>
		<row><td>SelfUnregModules</td><td/><td>2200</td><td>SelfUnregModules</td><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td/><td>1100</td><td>SetARPINSTALLLOCATION</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetODBCFolders</td><td/><td>1200</td><td>SetODBCFolders</td><td/></row>
		<row><td>StartServices</td><td>VersionNT</td><td>5900</td><td>StartServices</td><td/></row>
		<row><td>StopServices</td><td>VersionNT</td><td>1900</td><td>StopServices</td><td/></row>
		<row><td>UnpublishComponents</td><td/><td>1700</td><td>UnpublishComponents</td><td/></row>
		<row><td>UnpublishFeatures</td><td/><td>1800</td><td>UnpublishFeatures</td><td/></row>
		<row><td>UnregisterClassInfo</td><td/><td>2700</td><td>UnregisterClassInfo</td><td/></row>
		<row><td>UnregisterComPlus</td><td/><td>2100</td><td>UnregisterComPlus</td><td/></row>
		<row><td>UnregisterExtensionInfo</td><td/><td>2800</td><td>UnregisterExtensionInfo</td><td/></row>
		<row><td>UnregisterFonts</td><td/><td>2500</td><td>UnregisterFonts</td><td/></row>
		<row><td>UnregisterMIMEInfo</td><td/><td>3000</td><td>UnregisterMIMEInfo</td><td/></row>
		<row><td>UnregisterProgIdInfo</td><td/><td>2900</td><td>UnregisterProgIdInfo</td><td/></row>
		<row><td>UnregisterTypeLibraries</td><td/><td>2300</td><td>UnregisterTypeLibraries</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>WriteEnvironmentStrings</td><td/><td>5200</td><td>WriteEnvironmentStrings</td><td/></row>
		<row><td>WriteIniValues</td><td/><td>5100</td><td>WriteIniValues</td><td/></row>
		<row><td>WriteRegistryValues</td><td/><td>5000</td><td>WriteRegistryValues</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="InstallShield">
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
		<row><td>ActiveLanguage</td><td>2052</td></row>
		<row><td>Comments</td><td/></row>
		<row><td>CurrentMedia</td><td dt:dt="bin.base64" md5="de9f554a3bc05c12be9c31b998217995">
UwBpAG4AZwBsAGUASQBtAGEAZwBlAAEARQB4AHAAcgBlAHMAcwA=
			</td></row>
		<row><td>DefaultProductConfiguration</td><td>Express</td></row>
		<row><td>EnableSwidtag</td><td>1</td></row>
		<row><td>ISCompilerOption_CompileBeforeBuild</td><td>1</td></row>
		<row><td>ISCompilerOption_Debug</td><td>0</td></row>
		<row><td>ISCompilerOption_IncludePath</td><td/></row>
		<row><td>ISCompilerOption_LibraryPath</td><td/></row>
		<row><td>ISCompilerOption_MaxErrors</td><td>50</td></row>
		<row><td>ISCompilerOption_MaxWarnings</td><td>50</td></row>
		<row><td>ISCompilerOption_OutputPath</td><td>&lt;ISProjectDataFolder&gt;\Script Files</td></row>
		<row><td>ISCompilerOption_PreProcessor</td><td>_ISSCRIPT_NEW_STYLE_DLG_DEFS</td></row>
		<row><td>ISCompilerOption_WarningLevel</td><td>3</td></row>
		<row><td>ISCompilerOption_WarningsAsErrors</td><td>1</td></row>
		<row><td>ISTheme</td><td>InstallShield Blue.theme</td></row>
		<row><td>ISUSLock</td><td>{854A1B41-6104-4E15-8247-15B11A8727A0}</td></row>
		<row><td>ISUSSignature</td><td>{07C853A7-3B82-4EC6-A42B-1CC57AF72CA7}</td></row>
		<row><td>ISVisitedViews</td><td>viewAssistant,viewObjects,viewUI,viewProject</td></row>
		<row><td>Limited</td><td>1</td></row>
		<row><td>LockPermissionMode</td><td>1</td></row>
		<row><td>MsiExecCmdLineOptions</td><td/></row>
		<row><td>MsiLogFile</td><td/></row>
		<row><td>OnUpgrade</td><td>0</td></row>
		<row><td>Owner</td><td/></row>
		<row><td>PatchFamily</td><td>MyPatchFamily1</td></row>
		<row><td>PatchSequence</td><td>1.0.0</td></row>
		<row><td>SaveAsSchema</td><td/></row>
		<row><td>SccEnabled</td><td>0</td></row>
		<row><td>SccPath</td><td/></row>
		<row><td>SchemaVersion</td><td>776</td></row>
		<row><td>Type</td><td>MSIE</td></row>
	</table>

	<table name="InstallUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td/><td>430</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>InstallWelcome</td><td>Not Installed</td><td>1210</td><td>InstallWelcome</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>Installed And Not RESUME And Not Preselected And Not PATCH</td><td>1230</td><td>MaintenanceWelcome</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1200</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>PatchWelcome</td><td>Installed And PATCH And Not IS_MAJOR_UPGRADE</td><td>1205</td><td>Patch Panel</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>990</td><td>ResolveSource</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>420</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1240</td><td>SetupProgress</td><td/></row>
		<row><td>SetupResume</td><td>Installed And (RESUME Or Preselected) And Not PATCH</td><td>1220</td><td>SetupResume</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="IsolatedComponent">
		<col key="yes" def="s72">Component_Shared</col>
		<col key="yes" def="s72">Component_Application</col>
	</table>

	<table name="LaunchCondition">
		<col key="yes" def="s255">Condition</col>
		<col def="l255">Description</col>
		<row><td>(Not Version9X) And (Not (VersionNT&gt;=400 And VersionNT&lt;=502))</td><td>##IDPROP_EXPRESS_LAUNCH_CONDITION_OS##</td></row>
		<row><td>DOTNETVERSION45FULL&gt;="#1"</td><td>##IDPROP_EXPRESS_LAUNCH_CONDITION_DOTNETVERSION45FULL##</td></row>
	</table>

	<table name="ListBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="ListView">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
		<col def="S72">Binary_</col>
	</table>

	<table name="LockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
	</table>

	<table name="MIME">
		<col key="yes" def="s64">ContentType</col>
		<col def="s255">Extension_</col>
		<col def="S38">CLSID</col>
	</table>

	<table name="Media">
		<col key="yes" def="i2">DiskId</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="MoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">SourceName</col>
		<col def="L255">DestName</col>
		<col def="S72">SourceFolder</col>
		<col def="s72">DestFolder</col>
		<col def="i2">Options</col>
	</table>

	<table name="MsiAssembly">
		<col key="yes" def="s72">Component_</col>
		<col def="s38">Feature_</col>
		<col def="S72">File_Manifest</col>
		<col def="S72">File_Application</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="MsiAssemblyName">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="MsiDigitalCertificate">
		<col key="yes" def="s72">DigitalCertificate</col>
		<col def="v0">CertData</col>
	</table>

	<table name="MsiDigitalSignature">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s72">SignObject</col>
		<col def="s72">DigitalCertificate_</col>
		<col def="V0">Hash</col>
	</table>

	<table name="MsiDriverPackages">
		<col key="yes" def="s72">Component</col>
		<col def="i4">Flags</col>
		<col def="I4">Sequence</col>
		<col def="S0">ReferenceComponents</col>
	</table>

	<table name="MsiEmbeddedChainer">
		<col key="yes" def="s72">MsiEmbeddedChainer</col>
		<col def="S255">Condition</col>
		<col def="S255">CommandLine</col>
		<col def="s72">Source</col>
		<col def="I4">Type</col>
	</table>

	<table name="MsiEmbeddedUI">
		<col key="yes" def="s72">MsiEmbeddedUI</col>
		<col def="s255">FileName</col>
		<col def="i2">Attributes</col>
		<col def="I4">MessageFilter</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="MsiFileHash">
		<col key="yes" def="s72">File_</col>
		<col def="i2">Options</col>
		<col def="i4">HashPart1</col>
		<col def="i4">HashPart2</col>
		<col def="i4">HashPart3</col>
		<col def="i4">HashPart4</col>
	</table>

	<table name="MsiLockPermissionsEx">
		<col key="yes" def="s72">MsiLockPermissionsEx</col>
		<col def="s72">LockObject</col>
		<col def="s32">Table</col>
		<col def="s0">SDDLText</col>
		<col def="S255">Condition</col>
	</table>

	<table name="MsiPackageCertificate">
		<col key="yes" def="s72">PackageCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchCertificate">
		<col key="yes" def="s72">PatchCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchMetadata">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="S72">Company</col>
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="MsiPatchOldAssemblyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="S72">Assembly_</col>
	</table>

	<table name="MsiPatchOldAssemblyName">
		<col key="yes" def="s72">Assembly</col>
		<col key="yes" def="s255">Name</col>
		<col def="S255">Value</col>
	</table>

	<table name="MsiPatchSequence">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="s0">PatchFamily</col>
		<col key="yes" def="S0">Target</col>
		<col def="s0">Sequence</col>
		<col def="i2">Supersede</col>
	</table>

	<table name="MsiServiceConfig">
		<col key="yes" def="s72">MsiServiceConfig</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="i4">ConfigType</col>
		<col def="S0">Argument</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiServiceConfigFailureActions">
		<col key="yes" def="s72">MsiServiceConfigFailureActions</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="I4">ResetPeriod</col>
		<col def="L255">RebootMessage</col>
		<col def="L255">Command</col>
		<col def="S0">Actions</col>
		<col def="S0">DelayActions</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiShortcutProperty">
		<col key="yes" def="s72">MsiShortcutProperty</col>
		<col def="s72">Shortcut_</col>
		<col def="s0">PropertyKey</col>
		<col def="s0">PropVariantValue</col>
	</table>

	<table name="ODBCAttribute">
		<col key="yes" def="s72">Driver_</col>
		<col key="yes" def="s40">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCDataSource">
		<col key="yes" def="s72">DataSource</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s255">DriverDescription</col>
		<col def="i2">Registration</col>
	</table>

	<table name="ODBCDriver">
		<col key="yes" def="s72">Driver</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="ODBCSourceAttribute">
		<col key="yes" def="s72">DataSource_</col>
		<col key="yes" def="s32">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCTranslator">
		<col key="yes" def="s72">Translator</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="Patch">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="i2">Sequence</col>
		<col def="i4">PatchSize</col>
		<col def="i2">Attributes</col>
		<col def="V0">Header</col>
		<col def="S38">StreamRef_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="PatchPackage">
		<col key="yes" def="s38">PatchId</col>
		<col def="i2">Media_</col>
	</table>

	<table name="ProgId">
		<col key="yes" def="s255">ProgId</col>
		<col def="S255">ProgId_Parent</col>
		<col def="S38">Class_</col>
		<col def="L255">Description</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="Property">
		<col key="yes" def="s72">Property</col>
		<col def="L0">Value</col>
		<col def="S255">ISComments</col>
		<row><td>ALLUSERS</td><td>1</td><td/></row>
		<row><td>ARPINSTALLLOCATION</td><td/><td/></row>
		<row><td>ARPPRODUCTICON</td><td>ARPPRODUCTICON.exe</td><td/></row>
		<row><td>ARPSIZE</td><td/><td/></row>
		<row><td>ARPURLINFOABOUT</td><td>##ID_STRING1##</td><td/></row>
		<row><td>AgreeToLicense</td><td>No</td><td/></row>
		<row><td>ApplicationUsers</td><td>AllUsers</td><td/></row>
		<row><td>DWUSINTERVAL</td><td>30</td><td/></row>
		<row><td>DWUSLINK</td><td>CE2CB7F859EC80D8AEACC7B84ECC978FCE8BA78F59CCF7D8CEACF09F8ECC9768CEACD08FBEAC</td><td/></row>
		<row><td>DefaultUIFont</td><td>ExpressDefault</td><td/></row>
		<row><td>DialogCaption</td><td>InstallShield for Windows Installer</td><td/></row>
		<row><td>DiskPrompt</td><td>[1]</td><td/></row>
		<row><td>DiskSerial</td><td>1234-5678</td><td/></row>
		<row><td>DisplayNameCustom</td><td>##IDS__DisplayName_Custom##</td><td/></row>
		<row><td>DisplayNameMinimal</td><td>##IDS__DisplayName_Minimal##</td><td/></row>
		<row><td>DisplayNameTypical</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>Display_IsBitmapDlg</td><td>1</td><td/></row>
		<row><td>ErrorDialog</td><td>SetupError</td><td/></row>
		<row><td>INSTALLLEVEL</td><td>200</td><td/></row>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td><td/></row>
		<row><td>ISENABLEDWUSFINISHDIALOG</td><td/><td/></row>
		<row><td>ISSHOWMSILOG</td><td/><td/></row>
		<row><td>ISVROOT_PORT_NO</td><td>0</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_COST</td><td>##IDS_COMPLUS_PROGRESSTEXT_COST##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_INSTALL##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_UNINSTALL##</td><td/></row>
		<row><td>IS_PREVENT_DOWNGRADE_EXIT</td><td>##IDS_PREVENT_DOWNGRADE_EXIT##</td><td/></row>
		<row><td>IS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>##IDS_PROGMSG_TEXTFILECHANGS_REPLACE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_COSTING</td><td>##IDS_PROGMSG_XML_COSTING##</td><td/></row>
		<row><td>IS_PROGMSG_XML_CREATE_FILE</td><td>##IDS_PROGMSG_XML_CREATE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_FILES</td><td>##IDS_PROGMSG_XML_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_REMOVE_FILE</td><td>##IDS_PROGMSG_XML_REMOVE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_ROLLBACK_FILES</td><td>##IDS_PROGMSG_XML_ROLLBACK_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_UPDATE_FILE</td><td>##IDS_PROGMSG_XML_UPDATE_FILE##</td><td/></row>
		<row><td>IS_SQLSERVER_AUTHENTICATION</td><td>0</td><td/></row>
		<row><td>IS_SQLSERVER_DATABASE</td><td/><td/></row>
		<row><td>IS_SQLSERVER_PASSWORD</td><td/><td/></row>
		<row><td>IS_SQLSERVER_SERVER</td><td/><td/></row>
		<row><td>IS_SQLSERVER_USERNAME</td><td>sa</td><td/></row>
		<row><td>InstallChoice</td><td>AR</td><td/></row>
		<row><td>LAUNCHREADME</td><td>1</td><td/></row>
		<row><td>Manufacturer</td><td>##COMPANY_NAME##</td><td/></row>
		<row><td>PIDKEY</td><td/><td/></row>
		<row><td>PIDTemplate</td><td>12345&lt;###-%%%%%%%&gt;@@@@@</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOL</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOT</td><td>##IDS_PROGMSG_IIS_CREATEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOTS</td><td>##IDS_PROGMSG_IIS_CREATEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITE</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITE##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITES</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACT</td><td>##IDS_PROGMSG_IIS_EXTRACT##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONEz</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTzDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOL</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVESITE</td><td>##IDS_PROGMSG_IIS_REMOVESITE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOT</td><td>##IDS_PROGMSG_IIS_REMOVEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOTS</td><td>##IDS_PROGMSG_IIS_REMOVEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSITES</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>##IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKVROOTS</td><td>##IDS_PROGMSG_IIS_ROLLBACKVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>ProductCode</td><td>{E4AB4ABD-A265-4E4F-A937-19992E33B202}</td><td/></row>
		<row><td>ProductName</td><td>流火手机取证分析系统</td><td/></row>
		<row><td>ProductVersion</td><td>3.3</td><td/></row>
		<row><td>ProgressType0</td><td>install</td><td/></row>
		<row><td>ProgressType1</td><td>Installing</td><td/></row>
		<row><td>ProgressType2</td><td>installed</td><td/></row>
		<row><td>ProgressType3</td><td>installs</td><td/></row>
		<row><td>RebootYesNo</td><td>Yes</td><td/></row>
		<row><td>ReinstallFileVersion</td><td>o</td><td/></row>
		<row><td>ReinstallModeText</td><td>omus</td><td/></row>
		<row><td>ReinstallRepair</td><td>r</td><td/></row>
		<row><td>RestartManagerOption</td><td>CloseRestart</td><td/></row>
		<row><td>SERIALNUMBER</td><td/><td/></row>
		<row><td>SERIALNUMVALSUCCESSRETVAL</td><td>1</td><td/></row>
		<row><td>SHOWLAUNCHPROGRAM</td><td>0</td><td/></row>
		<row><td>SecureCustomProperties</td><td>ISFOUNDNEWERPRODUCTVERSION;USERNAME;COMPANYNAME;ISX_SERIALNUM;SUPPORTDIR;DOTNETVERSION45FULL</td><td/></row>
		<row><td>SelectedSetupType</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>SetupType</td><td>Typical</td><td/></row>
		<row><td>UpgradeCode</td><td>{9E6C908F-755F-42C7-B2D0-1999B12EFD5B}</td><td/></row>
		<row><td>_IsMaintenance</td><td>Change</td><td/></row>
		<row><td>_IsSetupTypeMin</td><td>Typical</td><td/></row>
	</table>

	<table name="PublishComponent">
		<col key="yes" def="s38">ComponentId</col>
		<col key="yes" def="s255">Qualifier</col>
		<col key="yes" def="s72">Component_</col>
		<col def="L0">AppData</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="RadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="L64">Text</col>
		<col def="L50">Help</col>
		<col def="I4">ISControlId</col>
		<row><td>AgreeToLicense</td><td>1</td><td>No</td><td>0</td><td>15</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_0##</td><td/><td/></row>
		<row><td>AgreeToLicense</td><td>2</td><td>Yes</td><td>0</td><td>0</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_1##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>1</td><td>AllUsers</td><td>1</td><td>7</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_Anyone##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>2</td><td>OnlyCurrentUser</td><td>1</td><td>23</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>1</td><td>CloseRestart</td><td>6</td><td>9</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_CloseRestart##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>2</td><td>Reboot</td><td>6</td><td>21</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_RebootAfter##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>1</td><td>Change</td><td>0</td><td>0</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Modify##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>2</td><td>Reinstall</td><td>0</td><td>60</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Repair##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>3</td><td>Remove</td><td>0</td><td>120</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Remove##</td><td/><td/></row>
		<row><td>_IsSetupTypeMin</td><td>1</td><td>Typical</td><td>1</td><td>6</td><td>264</td><td>14</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td/><td/></row>
	</table>

	<table name="RegLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="I2">Type</col>
		<row><td>DotNet45Full</td><td>2</td><td>SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full</td><td>Version</td><td>2</td></row>
	</table>

	<table name="Registry">
		<col key="yes" def="s72">Registry</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="S0">Value</col>
		<col def="s72">Component_</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="RemoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">FileName</col>
		<col def="s72">DirProperty</col>
		<col def="i2">InstallMode</col>
		<row><td>Shortcut</td><td>IS_ININSTALL_SHORTCUT</td><td/><td>product_name</td><td>2</td></row>
		<row><td>SingularityShell.exe</td><td>SingularityShell.exe2</td><td/><td>product_name3</td><td>2</td></row>
	</table>

	<table name="RemoveIniFile">
		<col key="yes" def="s72">RemoveIniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l96">Section</col>
		<col def="l128">Key</col>
		<col def="L255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="RemoveRegistry">
		<col key="yes" def="s72">RemoveRegistry</col>
		<col def="i2">Root</col>
		<col def="l255">Key</col>
		<col def="L255">Name</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ReserveCost">
		<col key="yes" def="s72">ReserveKey</col>
		<col def="s72">Component_</col>
		<col def="S72">ReserveFolder</col>
		<col def="i4">ReserveLocal</col>
		<col def="i4">ReserveSource</col>
	</table>

	<table name="SFPCatalog">
		<col key="yes" def="s255">SFPCatalog</col>
		<col def="V0">Catalog</col>
		<col def="S0">Dependency</col>
	</table>

	<table name="SelfReg">
		<col key="yes" def="s72">File_</col>
		<col def="I2">Cost</col>
	</table>

	<table name="ServiceControl">
		<col key="yes" def="s72">ServiceControl</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="S255">Arguments</col>
		<col def="I2">Wait</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ServiceInstall">
		<col key="yes" def="s72">ServiceInstall</col>
		<col def="s255">Name</col>
		<col def="L255">DisplayName</col>
		<col def="i4">ServiceType</col>
		<col def="i4">StartType</col>
		<col def="i4">ErrorControl</col>
		<col def="S255">LoadOrderGroup</col>
		<col def="S255">Dependencies</col>
		<col def="S255">StartName</col>
		<col def="S255">Password</col>
		<col def="S255">Arguments</col>
		<col def="s72">Component_</col>
		<col def="L255">Description</col>
	</table>

	<table name="Shortcut">
		<col key="yes" def="s72">Shortcut</col>
		<col def="s72">Directory_</col>
		<col def="l128">Name</col>
		<col def="s72">Component_</col>
		<col def="s255">Target</col>
		<col def="S255">Arguments</col>
		<col def="L255">Description</col>
		<col def="I2">Hotkey</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I2">ShowCmd</col>
		<col def="S72">WkDir</col>
		<col def="S255">DisplayResourceDLL</col>
		<col def="I2">DisplayResourceId</col>
		<col def="S255">DescriptionResourceDLL</col>
		<col def="I2">DescriptionResourceId</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISShortcutName</col>
		<col def="I4">ISAttributes</col>
		<row><td>Shortcut</td><td>product_name</td><td>##IDS_SHORTCUT_DISPLAY_NAME3##</td><td>IS_ININSTALL_SHORTCUT</td><td>[SystemFolder]msiexec.exe</td><td>/x {E4AB4ABD-A265-4E4F-A937-19992E33B202}</td><td/><td/><td>Shortcut_8CBEA929EFDD431C9E909E92750734E0.exe</td><td>0</td><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>SingularityShell.exe</td><td>product_name3</td><td>##IDS_SHORTCUT_DISPLAY_NAME100##</td><td>SingularityShell.exe2</td><td>AlwaysInstall</td><td/><td/><td/><td>SingularityShell.e_FFC381C5028B4D749A58544295AF5368.exe</td><td>1</td><td>1</td><td>INSTALLDIR</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>SingularityShell.exe1</td><td>DesktopFolder</td><td>##IDS_SHORTCUT_DISPLAY_NAME100##</td><td>SingularityShell.exe2</td><td>AlwaysInstall</td><td/><td/><td/><td>SingularityShell.e_494882FB7120450DB282E6DE8B7837B9.exe</td><td>1</td><td>1</td><td>INSTALLDIR</td><td/><td/><td/><td/><td/><td/><td/></row>
	</table>

	<table name="Signature">
		<col key="yes" def="s72">Signature</col>
		<col def="s255">FileName</col>
		<col def="S20">MinVersion</col>
		<col def="S20">MaxVersion</col>
		<col def="I4">MinSize</col>
		<col def="I4">MaxSize</col>
		<col def="I4">MinDate</col>
		<col def="I4">MaxDate</col>
		<col def="S255">Languages</col>
	</table>

	<table name="TextStyle">
		<col key="yes" def="s72">TextStyle</col>
		<col def="s32">FaceName</col>
		<col def="i2">Size</col>
		<col def="I4">Color</col>
		<col def="I2">StyleBits</col>
		<row><td>Arial8</td><td>Arial</td><td>8</td><td/><td/></row>
		<row><td>Arial9</td><td>Arial</td><td>9</td><td/><td/></row>
		<row><td>ArialBlue10</td><td>Arial</td><td>10</td><td>16711680</td><td/></row>
		<row><td>ArialBlueStrike10</td><td>Arial</td><td>10</td><td>16711680</td><td>8</td></row>
		<row><td>CourierNew8</td><td>Courier New</td><td>8</td><td/><td/></row>
		<row><td>CourierNew9</td><td>Courier New</td><td>9</td><td/><td/></row>
		<row><td>ExpressDefault</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>MSGothic9</td><td>MS Gothic</td><td>9</td><td/><td/></row>
		<row><td>MSSGreySerif8</td><td>MS Sans Serif</td><td>8</td><td>8421504</td><td/></row>
		<row><td>MSSWhiteSerif8</td><td>Tahoma</td><td>8</td><td>16777215</td><td/></row>
		<row><td>MSSansBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>MSSansSerif8</td><td>MS Sans Serif</td><td>8</td><td/><td/></row>
		<row><td>MSSansSerif9</td><td>MS Sans Serif</td><td>9</td><td/><td/></row>
		<row><td>Tahoma10</td><td>Tahoma</td><td>10</td><td/><td/></row>
		<row><td>Tahoma8</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>Tahoma9</td><td>Tahoma</td><td>9</td><td/><td/></row>
		<row><td>TahomaBold10</td><td>Tahoma</td><td>10</td><td/><td>1</td></row>
		<row><td>TahomaBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>Times8</td><td>Times New Roman</td><td>8</td><td/><td/></row>
		<row><td>Times9</td><td>Times New Roman</td><td>9</td><td/><td/></row>
		<row><td>TimesItalic12</td><td>Times New Roman</td><td>12</td><td/><td>2</td></row>
		<row><td>TimesItalicBlue10</td><td>Times New Roman</td><td>10</td><td>16711680</td><td>2</td></row>
		<row><td>TimesRed16</td><td>Times New Roman</td><td>16</td><td>255</td><td/></row>
		<row><td>VerdanaBold14</td><td>Verdana</td><td>13</td><td/><td>1</td></row>
	</table>

	<table name="TypeLib">
		<col key="yes" def="s38">LibID</col>
		<col key="yes" def="i2">Language</col>
		<col key="yes" def="s72">Component_</col>
		<col def="I4">Version</col>
		<col def="L128">Description</col>
		<col def="S72">Directory_</col>
		<col def="s38">Feature_</col>
		<col def="I4">Cost</col>
	</table>

	<table name="UIText">
		<col key="yes" def="s72">Key</col>
		<col def="L255">Text</col>
		<row><td>AbsentPath</td><td/></row>
		<row><td>GB</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>KB</td><td>##IDS_UITEXT_KB##</td></row>
		<row><td>MB</td><td>##IDS_UITEXT_MB##</td></row>
		<row><td>MenuAbsent</td><td>##IDS_UITEXT_FeatureNotAvailable##</td></row>
		<row><td>MenuAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired2##</td></row>
		<row><td>MenuAllCD</td><td>##IDS_UITEXT_FeatureInstalledCD##</td></row>
		<row><td>MenuAllLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal##</td></row>
		<row><td>MenuAllNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork##</td></row>
		<row><td>MenuCD</td><td>##IDS_UITEXT_FeatureInstalledCD2##</td></row>
		<row><td>MenuLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal2##</td></row>
		<row><td>MenuNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork2##</td></row>
		<row><td>NewFolder</td><td>##IDS_UITEXT_Folder##</td></row>
		<row><td>SelAbsentAbsent</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>SelAbsentAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired##</td></row>
		<row><td>SelAbsentCD</td><td>##IDS_UITEXT_FeatureOnCD##</td></row>
		<row><td>SelAbsentLocal</td><td>##IDS_UITEXT_FeatureLocal##</td></row>
		<row><td>SelAbsentNetwork</td><td>##IDS_UITEXT_FeatureNetwork##</td></row>
		<row><td>SelAdvertiseAbsent</td><td>##IDS_UITEXT_FeatureUnavailable##</td></row>
		<row><td>SelAdvertiseAdvertise</td><td>##IDS_UITEXT_FeatureInstalledRequired##</td></row>
		<row><td>SelAdvertiseCD</td><td>##IDS_UITEXT_FeatureOnCD2##</td></row>
		<row><td>SelAdvertiseLocal</td><td>##IDS_UITEXT_FeatureLocal2##</td></row>
		<row><td>SelAdvertiseNetwork</td><td>##IDS_UITEXT_FeatureNetwork2##</td></row>
		<row><td>SelCDAbsent</td><td>##IDS_UITEXT_FeatureWillBeUninstalled##</td></row>
		<row><td>SelCDAdvertise</td><td>##IDS_UITEXT_FeatureWasCD##</td></row>
		<row><td>SelCDCD</td><td>##IDS_UITEXT_FeatureRunFromCD##</td></row>
		<row><td>SelCDLocal</td><td>##IDS_UITEXT_FeatureWasCDLocal##</td></row>
		<row><td>SelChildCostNeg</td><td>##IDS_UITEXT_FeatureFreeSpace##</td></row>
		<row><td>SelChildCostPos</td><td>##IDS_UITEXT_FeatureRequiredSpace##</td></row>
		<row><td>SelCostPending</td><td>##IDS_UITEXT_CompilingFeaturesCost##</td></row>
		<row><td>SelLocalAbsent</td><td>##IDS_UITEXT_FeatureCompletelyRemoved##</td></row>
		<row><td>SelLocalAdvertise</td><td>##IDS_UITEXT_FeatureRemovedUnlessRequired##</td></row>
		<row><td>SelLocalCD</td><td>##IDS_UITEXT_FeatureRemovedCD##</td></row>
		<row><td>SelLocalLocal</td><td>##IDS_UITEXT_FeatureRemainLocal##</td></row>
		<row><td>SelLocalNetwork</td><td>##IDS_UITEXT_FeatureRemoveNetwork##</td></row>
		<row><td>SelNetworkAbsent</td><td>##IDS_UITEXT_FeatureUninstallNoNetwork##</td></row>
		<row><td>SelNetworkAdvertise</td><td>##IDS_UITEXT_FeatureWasOnNetworkInstalled##</td></row>
		<row><td>SelNetworkLocal</td><td>##IDS_UITEXT_FeatureWasOnNetworkLocal##</td></row>
		<row><td>SelNetworkNetwork</td><td>##IDS_UITEXT_FeatureContinueNetwork##</td></row>
		<row><td>SelParentCostNegNeg</td><td>##IDS_UITEXT_FeatureSpaceFree##</td></row>
		<row><td>SelParentCostNegPos</td><td>##IDS_UITEXT_FeatureSpaceFree2##</td></row>
		<row><td>SelParentCostPosNeg</td><td>##IDS_UITEXT_FeatureSpaceFree3##</td></row>
		<row><td>SelParentCostPosPos</td><td>##IDS_UITEXT_FeatureSpaceFree4##</td></row>
		<row><td>TimeRemaining</td><td>##IDS_UITEXT_TimeRemaining##</td></row>
		<row><td>VolumeCostAvailable</td><td>##IDS_UITEXT_Available##</td></row>
		<row><td>VolumeCostDifference</td><td>##IDS_UITEXT_Differences##</td></row>
		<row><td>VolumeCostRequired</td><td>##IDS_UITEXT_Required##</td></row>
		<row><td>VolumeCostSize</td><td>##IDS_UITEXT_DiskSize##</td></row>
		<row><td>VolumeCostVolume</td><td>##IDS_UITEXT_Volume##</td></row>
		<row><td>bytes</td><td>##IDS_UITEXT_Bytes##</td></row>
	</table>

	<table name="Upgrade">
		<col key="yes" def="s38">UpgradeCode</col>
		<col key="yes" def="S20">VersionMin</col>
		<col key="yes" def="S20">VersionMax</col>
		<col key="yes" def="S255">Language</col>
		<col key="yes" def="i4">Attributes</col>
		<col def="S255">Remove</col>
		<col def="s72">ActionProperty</col>
		<col def="S72">ISDisplayName</col>
		<row><td>{00000000-0000-0000-0000-000000000000}</td><td>***ALL_VERSIONS***</td><td></td><td></td><td>2</td><td/><td>ISFOUNDNEWERPRODUCTVERSION</td><td>ISPreventDowngrade</td></row>
	</table>

	<table name="Verb">
		<col key="yes" def="s255">Extension_</col>
		<col key="yes" def="s32">Verb</col>
		<col def="I2">Sequence</col>
		<col def="L255">Command</col>
		<col def="L255">Argument</col>
	</table>

	<table name="_Validation">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s32">Column</col>
		<col def="s4">Nullable</col>
		<col def="I4">MinValue</col>
		<col def="I4">MaxValue</col>
		<col def="S255">KeyTable</col>
		<col def="I2">KeyColumn</col>
		<col def="S32">Category</col>
		<col def="S255">Set</col>
		<col def="S255">Description</col>
		<row><td>ActionText</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to be described.</td></row>
		<row><td>ActionText</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description displayed in progress dialog and log when action is executing.</td></row>
		<row><td>ActionText</td><td>Template</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Optional localized format template used to format action data records for display during action execution.</td></row>
		<row><td>AdminExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdminUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AppId</td><td>ActivateAtStorage</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>AppId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td/></row>
		<row><td>AppId</td><td>DllSurrogate</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>LocalService</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>RemoteServerName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>AppId</td><td>RunAsInteractiveUser</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>ServiceParameters</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppSearch</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The property associated with a Signature</td></row>
		<row><td>AppSearch</td><td>Signature_</td><td>N</td><td/><td/><td>ISXmlLocator;Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>BBControl</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>BBControl</td><td>BBControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a billboard, but can repeat on different billboard.</td></row>
		<row><td>BBControl</td><td>Billboard_</td><td>N</td><td/><td/><td>Billboard</td><td>1</td><td>Identifier</td><td/><td>External key to the Billboard table, name of the billboard.</td></row>
		<row><td>BBControl</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>BBControl</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>BBControl</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Billboard</td><td>Action</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of an action. The billboard is displayed during the progress messages received from this action.</td></row>
		<row><td>Billboard</td><td>Billboard</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the billboard.</td></row>
		<row><td>Billboard</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>An external key to the Feature Table. The billboard is shown only if this feature is being installed.</td></row>
		<row><td>Billboard</td><td>Ordering</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>A positive integer. If there is more than one billboard corresponding to an action they will be shown in the order defined by this column.</td></row>
		<row><td>Binary</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Binary</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Binary</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique key identifying the binary data.</td></row>
		<row><td>BindImage</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>The index into the File table. This must be an executable file.</td></row>
		<row><td>BindImage</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Paths</td><td/><td>A list of ;  delimited paths that represent the paths to be searched for the import DLLS. The list is usually a list of properties each enclosed within square brackets [] .</td></row>
		<row><td>CCPSearch</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>CheckBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to the item.</td></row>
		<row><td>CheckBox</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with the item.</td></row>
		<row><td>Class</td><td>AppId_</td><td>Y</td><td/><td/><td>AppId</td><td>1</td><td>Guid</td><td/><td>Optional AppID containing DCOM information for associated application (string GUID).</td></row>
		<row><td>Class</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>optional argument for LocalServers.</td></row>
		<row><td>Class</td><td>Attributes</td><td>Y</td><td/><td>32767</td><td/><td/><td/><td/><td>Class registration attributes.</td></row>
		<row><td>Class</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The CLSID of an OLE factory.</td></row>
		<row><td>Class</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Class</td><td>Context</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The numeric server context for this server. CLSCTX_xxxx</td></row>
		<row><td>Class</td><td>DefInprocHandler</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td>1;2;3</td><td>Optional default inproc handler.  Only optionally provided if Context=CLSCTX_LOCAL_SERVER.  Typically "ole32.dll" or "mapi32.dll"</td></row>
		<row><td>Class</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Class.</td></row>
		<row><td>Class</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Class</td><td>FileTypeMask</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional string containing information for the HKCRthis CLSID) key. If multiple patterns exist, they must be delimited by a semicolon, and numeric subkeys will be generated: 0,1,2...</td></row>
		<row><td>Class</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>Class</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this CLSID. Will be written under the DefaultIcon key.</td></row>
		<row><td>Class</td><td>ProgId_Default</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this CLSID.</td></row>
		<row><td>ComboBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list.	The integers do not have to be consecutive.</td></row>
		<row><td>ComboBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same combobox.</td></row>
		<row><td>ComboBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ComboBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>CompLocator</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>CompLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>CompLocator</td><td>Type</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>A boolean value that determines if the registry value is a filename or a directory location.</td></row>
		<row><td>Complus</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the ComPlus component.</td></row>
		<row><td>Complus</td><td>ExpType</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>ComPlus component attributes.</td></row>
		<row><td>Component</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Remote execution option, one of irsEnum</td></row>
		<row><td>Component</td><td>Component</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>Component</td><td>ComponentId</td><td>Y</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>Component</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this component if the specified condition evaluates to the 'True' state. If a component is disabled, it will not be installed, regardless of the 'Action' state associated with the component.</td></row>
		<row><td>Component</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Required key of a Directory table record. This is actually a property name whose value contains the actual path, set either by the AppSearch action or with the default setting obtained from the Directory table.</td></row>
		<row><td>Component</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component.</td></row>
		<row><td>Component</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsCommit</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsInstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsRollback</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsUninstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISRegFileToMergeAtBuild</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path and File name of a .REG file to merge into the component at build time.</td></row>
		<row><td>Component</td><td>ISScanAtBuildFile</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File used by the Dot Net scanner to populate dependant assemblies' File_Application field.</td></row>
		<row><td>Component</td><td>KeyPath</td><td>Y</td><td/><td/><td>File;ODBCDataSource;Registry</td><td>1</td><td>Identifier</td><td/><td>Either the primary key into the File table, Registry table, or ODBCDataSource table. This extract path is stored when the component is installed, and is used to detect the presence of the component and to return the path to it.</td></row>
		<row><td>Condition</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression evaluated to determine if Level in the Feature table is to change.</td></row>
		<row><td>Condition</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Reference to a Feature entry in Feature table.</td></row>
		<row><td>Condition</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>New selection Level to set in Feature table if Condition evaluates to TRUE.</td></row>
		<row><td>Control</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>Control</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>Control</td><td>Control</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>Control</td><td>Control_Next</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>The name of an other control on the same dialog. This link defines the tab order of the controls. The links have to form one or more cycles!</td></row>
		<row><td>Control</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>Control</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>Control</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>Control</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>Control</td><td>ISWindowStyle</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control.</td></row>
		<row><td>Control</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of a defined property to be linked to this control.</td></row>
		<row><td>Control</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>Control</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>Control</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ControlCondition</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>Default;Disable;Enable;Hide;Show</td><td>The desired action to be taken on the specified control.</td></row>
		<row><td>ControlCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions the action should be triggered.</td></row>
		<row><td>ControlCondition</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>ControlCondition</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Argument</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A value to be used as a modifier when triggering a particular event.</td></row>
		<row><td>ControlEvent</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions an event should be triggered.</td></row>
		<row><td>ControlEvent</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control</td></row>
		<row><td>ControlEvent</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>An identifier that specifies the type of the event that should take place when the user interacts with control specified by the first two entries.</td></row>
		<row><td>ControlEvent</td><td>Ordering</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>An integer used to order several events tied to the same control. Can be left blank.</td></row>
		<row><td>CreateFolder</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>CreateFolder</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Primary key, could be foreign key into the Directory table.</td></row>
		<row><td>CustomAction</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, name of action, normally appears in sequence table unless private use.</td></row>
		<row><td>CustomAction</td><td>ExtendedType</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The numeric custom action type info flags.</td></row>
		<row><td>CustomAction</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this custom action.</td></row>
		<row><td>CustomAction</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>CustomSource</td><td/><td>The table reference of the source of the code.</td></row>
		<row><td>CustomAction</td><td>Target</td><td>Y</td><td/><td/><td>ISDLLWrapper;ISInstallScriptAction</td><td>1</td><td>Formatted</td><td/><td>Excecution parameter, depends on the type of custom action</td></row>
		<row><td>CustomAction</td><td>Type</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>The numeric custom action type, consisting of source location, code type, entry, option flags.</td></row>
		<row><td>Dialog</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>Dialog</td><td>Control_Cancel</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the cancel control. Hitting escape or clicking on the close icon on the dialog is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_Default</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the default control. Hitting return is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_First</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the control that has the focus when the dialog is created.</td></row>
		<row><td>Dialog</td><td>Dialog</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>Dialog</td><td>HCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Horizontal position of the dialog on a 0-100 scale. 0 means left end, 100 means right end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>Dialog</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this dialog.</td></row>
		<row><td>Dialog</td><td>ISResourceId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A Number the Specifies the Dialog ID to be used in Dialog Export</td></row>
		<row><td>Dialog</td><td>ISWindowStyle</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control. This is only used in Script Based Setups.</td></row>
		<row><td>Dialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>Dialog</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A text string specifying the title to be displayed in the title bar of the dialog's window.</td></row>
		<row><td>Dialog</td><td>VCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Vertical position of the dialog on a 0-100 scale. 0 means top end, 100 means bottom end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>Directory</td><td>DefaultDir</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The default sub-path under parent's path.</td></row>
		<row><td>Directory</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for directory entry, primary key. If a property by this name is defined, it contains the full path to the directory.</td></row>
		<row><td>Directory</td><td>Directory_Parent</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Reference to the entry in this table specifying the default parent directory. A record parented to itself or with a Null parent represents a root of the install tree.</td></row>
		<row><td>Directory</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7</td><td>This is used to store Installshield custom properties of a directory.  Currently the only one is Shortcut.</td></row>
		<row><td>Directory</td><td>ISDescription</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of folder</td></row>
		<row><td>Directory</td><td>ISFolderName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is used in Pro projects because the pro identifier used in the tree wasn't necessarily unique.</td></row>
		<row><td>DrLocator</td><td>Depth</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The depth below the path to which the Signature_ is recursively searched. If absent, the depth is assumed to be 0.</td></row>
		<row><td>DrLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table. If null and the Path column does not expand to a full path, then all the fixed drives of the user system are searched using the Path.</td></row>
		<row><td>DrLocator</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>AnyPath</td><td/><td>The path on the user system. This is a either a subpath below the value of the Parent or a full path. The path may contain properties enclosed within [ ] that will be expanded.</td></row>
		<row><td>DrLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>DuplicateFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>DestFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to a destination folder.</td></row>
		<row><td>DuplicateFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename to be given to the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>DuplicateFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing the source file to be duplicated.</td></row>
		<row><td>Environment</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the environmental value.</td></row>
		<row><td>Environment</td><td>Environment</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the environmental variable setting</td></row>
		<row><td>Environment</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the environmental value.</td></row>
		<row><td>Environment</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to set in the environmental settings.</td></row>
		<row><td>Error</td><td>Error</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer error number, obtained from header file IError(...) macros.</td></row>
		<row><td>Error</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Error formatting template, obtained from user ed. or localizers.</td></row>
		<row><td>EventMapping</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of the control attribute, that is set when this event is received.</td></row>
		<row><td>EventMapping</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>EventMapping</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the Dialog.</td></row>
		<row><td>EventMapping</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>An identifier that specifies the type of the event that the control subscribes to.</td></row>
		<row><td>Extension</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Extension</td><td>Extension</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Extension</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Extension</td><td>MIME_</td><td>Y</td><td/><td/><td>MIME</td><td>1</td><td>Text</td><td/><td>Optional Context identifier, typically "type/format" associated with the extension</td></row>
		<row><td>Extension</td><td>ProgId_</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this extension.</td></row>
		<row><td>Feature</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2;4;5;6;8;9;10;16;17;18;20;21;22;24;25;26;32;33;34;36;37;38;48;49;50;52;53;54</td><td>Feature attributes</td></row>
		<row><td>Feature</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>Feature</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>UpperCase</td><td/><td>The name of the Directory that can be configured by the UI. A non-null value will enable the browse button.</td></row>
		<row><td>Feature</td><td>Display</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>Feature</td><td>Feature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>Feature</td><td>Feature_Parent</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Optional key of a parent record in the same table. If the parent is not selected, then the record will not be installed. Null indicates a root item.</td></row>
		<row><td>Feature</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Comments</td></row>
		<row><td>Feature</td><td>ISFeatureCabName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Name of CAB used when compressing CABs by Feature. Used to override build generated name for CAB file.</td></row>
		<row><td>Feature</td><td>ISProFeatureName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the feature used by pro projects.  This doesn't have to be unique.</td></row>
		<row><td>Feature</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this  feature will be built in a particular release.</td></row>
		<row><td>Feature</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The install level at which record will be initially selected. An install level of 0 will disable an item and prevent its display.</td></row>
		<row><td>Feature</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Short text identifying a visible feature item.</td></row>
		<row><td>FeatureComponents</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>FeatureComponents</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>File</td><td>Attributes</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing file attributes (with the decimal value of each bit position in parentheses)</td></row>
		<row><td>File</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>File</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token, must match identifier in cabinet.  For uncompressed files, this field is ignored.</td></row>
		<row><td>File</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>File name used for installation.  This may contain a "short name|long name" pair.  It may be just a long name, hence it cannot be of the Filename data type.</td></row>
		<row><td>File</td><td>FileSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>File</td><td>ISAttributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>This field contains the following attributes: UseSystemSettings(0x1)</td></row>
		<row><td>File</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>File</td><td>ISComponentSubFolder_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key referencing component subfolder containing this file.  Only for Pro.</td></row>
		<row><td>File</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>List of decimal language Ids, comma-separated if more than one.</td></row>
		<row><td>File</td><td>Sequence</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>File</td><td>Version</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Version</td><td/><td>Version string for versioned files;  Blank for unversioned files.</td></row>
		<row><td>FileSFPCatalog</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>File associated with the catalog</td></row>
		<row><td>FileSFPCatalog</td><td>SFPCatalog_</td><td>N</td><td/><td/><td>SFPCatalog</td><td>1</td><td>Text</td><td/><td>Catalog associated with the file</td></row>
		<row><td>Font</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing font file.</td></row>
		<row><td>Font</td><td>FontTitle</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Font name.</td></row>
		<row><td>ISAssistantTag</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISAssistantTag</td><td>Tag</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Color</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Duration</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Effect</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Font</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>ISBillboard</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Origin</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Sequence</td><td>N</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Style</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Target</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Display name for the chained package. Used only in the IDE.</td></row>
		<row><td>ISChainPackage</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Options</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Package</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>ProductCode</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>SourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>ISChainPackageData</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>ISChainPackageData</td><td>Options</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Package_</td><td>N</td><td/><td/><td>ISChainPackage</td><td>1</td><td>Identifier</td><td/><td/></row>
		<row><td>ISClrWrap</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into CustomAction table</td></row>
		<row><td>ISClrWrap</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property associated with this Action</td></row>
		<row><td>ISClrWrap</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value associated with this Property</td></row>
		<row><td>ISComCatalogAttribute</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The named attribute for a catalog object.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A value associated with the attribute defined in the ItemName column.</td></row>
		<row><td>ISComCatalogCollection</td><td>CollectionName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A catalog collection name.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogCollection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogCollection_</td><td>N</td><td/><td/><td>ISComCatalogCollection</td><td>1</td><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogObject</td><td>DisplayName</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The display name of a catalog object.</td></row>
		<row><td>ISComCatalogObject</td><td>ISComCatalogObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplication</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>ComputerName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Computer name that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusApplication</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application.</td></row>
		<row><td>ISComPlusApplication</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>AlterDLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Alternate filename of the COM+ application component. Will be used for a .NET serviced component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>CLSID of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>DLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplicationDLL</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ProgId</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ProgId of the COM+ application component.</td></row>
		<row><td>ISComPlusProxy</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusProxy</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application proxy.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusProxy</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusProxy table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusProxyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusServerFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComponentExtended</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>ISComponentExtended</td><td>FTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>FTP Location</td></row>
		<row><td>ISComponentExtended</td><td>FilterProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property to set if you want to filter a component</td></row>
		<row><td>ISComponentExtended</td><td>HTTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HTTP Location</td></row>
		<row><td>ISComponentExtended</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Language</td></row>
		<row><td>ISComponentExtended</td><td>Miscellaneous</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Miscellaneous</td></row>
		<row><td>ISComponentExtended</td><td>OS</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of OSs</td></row>
		<row><td>ISComponentExtended</td><td>Platforms</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of Platforms.</td></row>
		<row><td>ISCustomActionReference</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into theICustomAction table.</td></row>
		<row><td>ISCustomActionReference</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Contents of the file speciifed in ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>FileType</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>file type of the file specified  ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>ISCAReferenceFilePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.  This column only exists in ISM.</td></row>
		<row><td>ISDIMDependency</td><td>ISDIMReference_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMDependency table</td></row>
		<row><td>ISDIMDependency</td><td>RequiredBuildVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the build version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMajorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the major version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMinorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the minor version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredRevisionVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the revision version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredUUID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>the UUID identifying the required DIM</td></row>
		<row><td>ISDIMReference</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISDIMReference</td><td>ISDIMReference</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>This is the primary key to the ISDIMReference table</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMDependency_</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMDependency table.</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMReference_Parent</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMVariable</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMVariable table</td></row>
		<row><td>ISDIMVariable</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of a variable defined in the .dim file</td></row>
		<row><td>ISDIMVariable</td><td>NewValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>New value that you want to override with</td></row>
		<row><td>ISDIMVariable</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type of the variable. 0: Build Variable, 1: Runtime Variable</td></row>
		<row><td>ISDLLWrapper</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISDLLWrapper</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISDLLWrapper</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISDLLWrapper</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISDependency</td><td>Exclude</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDependency</td><td>ISDependency</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDisk1File</td><td>Disk</td><td>Y</td><td/><td/><td/><td/><td/><td>-1;0;1</td><td>Used to differentiate between disk1(1), last disk(-1), and other(0).</td></row>
		<row><td>ISDisk1File</td><td>ISBuildSourcePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of file to be copied to Disk1 folder</td></row>
		<row><td>ISDisk1File</td><td>ISDisk1File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for ISDisk1File table</td></row>
		<row><td>ISDynamicFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>ISDynamicFile</td><td>ExcludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for excluded files.</td></row>
		<row><td>ISDynamicFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15</td><td>This is used to store Installshield custom properties of a dynamic filet.  Currently the only one is SelfRegister.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for included files.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Include flags.</td></row>
		<row><td>ISDynamicFile</td><td>SourceFolder</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Feature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>ModuleID</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>ISSetupPrerequisites_</td><td>N</td><td/><td/><td>ISSetupPrerequisites</td><td>1</td><td/><td/><td/></row>
		<row><td>ISFileManifests</td><td>File_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISFileManifests</td><td>Manifest_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISIISItem</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key to Component table.</td></row>
		<row><td>ISIISItem</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable Item Name.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for each item.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem_Parent</td><td>Y</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>This record's parent record.</td></row>
		<row><td>ISIISItem</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td/><td>IIS resource type.</td></row>
		<row><td>ISIISProperty</td><td>FriendlyName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property name.</td></row>
		<row><td>ISIISProperty</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Flags.</td></row>
		<row><td>ISIISProperty</td><td>ISIISItem_</td><td>N</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>Primary key for table, foreign key into ISIISItem.</td></row>
		<row><td>ISIISProperty</td><td>ISIISProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for table.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property attributes.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataProp</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property ID.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataUserType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property user data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property value.</td></row>
		<row><td>ISIISProperty</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Order sequencing.</td></row>
		<row><td>ISIISProperty</td><td>Schema</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS7 schema information.</td></row>
		<row><td>ISInstallScriptAction</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISInstallScriptAction</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISInstallScriptAction</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISInstallScriptAction</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISLanguage</td><td>ISLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the language ID.</td></row>
		<row><td>ISLanguage</td><td>Included</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1</td><td>Specify whether this language should be included.</td></row>
		<row><td>ISLinkerLibrary</td><td>ISLinkerLibrary</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the link library.</td></row>
		<row><td>ISLinkerLibrary</td><td>Library</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the object library (.obl file).</td></row>
		<row><td>ISLinkerLibrary</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Order of the Library</td></row>
		<row><td>ISLocalControl</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>ISLocalControl</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>ISLocalControl</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>ISLocalControl</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>ISLocalControl</td><td>Height</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>ISLocalControl</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalControl</td><td>Width</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>X</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>Y</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalDialog</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Dialog_</td><td>Y</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>ISLanguage_</td><td>Y</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalDialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>ISLocalDialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalRadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalRadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td>RadioButton</td><td>2</td><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ISLocalRadioButton</td><td>Property</td><td>N</td><td/><td/><td>RadioButton</td><td>1</td><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>ISLocalRadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLocalRadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLockPermissions</td><td>Attributes</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permissions attributes mask, 1==Deny access; 2==No inherit, 4==Ignore apply failures, 8==Target object is 64-bit</td></row>
		<row><td>ISLockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set.</td></row>
		<row><td>ISLockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into CreateFolder, Registry, or File table</td></row>
		<row><td>ISLockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.</td></row>
		<row><td>ISLockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>ISLockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set. This can be a property, hardcoded named, or SID string</td></row>
		<row><td>ISLogicalDisk</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>ISLogicalDisk</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDisk</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDisk</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>ISLogicalDisk</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>ISLogicalDisk</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Feature_</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table,</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties, like Compressed, etc.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISLogicalDisk_</td><td>N</td><td>1</td><td>32767</td><td>ISLogicalDisk</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISLogicalDisk table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the file for this media.</td></row>
		<row><td>ISMergeModule</td><td>Destination</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Destination.</td></row>
		<row><td>ISMergeModule</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a merge module.</td></row>
		<row><td>ISMergeModule</td><td>ISMergeModule</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The GUID identifying the merge module.</td></row>
		<row><td>ISMergeModule</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModule</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the merge module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Attributes (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ContextData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ContextData  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DefaultValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DefaultValue  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DisplayName (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Format</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Format (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpKeyword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpKeyword (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpLocation (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>The module signature, a foreign key into the ISMergeModule table</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ModuleConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Identifier, foreign key into ModuleConfiguration table (ModuleConfiguration.Name)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Type (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for this item.</td></row>
		<row><td>ISObject</td><td>Language</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObject</td><td>ObjectName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>IncludeInBuild</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Boolean, 0 for false non 0 for true</td></row>
		<row><td>ISObjectProperty</td><td>ObjectName</td><td>Y</td><td/><td/><td>ISObject</td><td>1</td><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISPatchConfigImage</td><td>PatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfigurationTable</td></row>
		<row><td>ISPatchConfigImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISUpgradedImageTable</td></row>
		<row><td>ISPatchConfiguration</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>PatchConfiguration attributes</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPCDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether Product Codes may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPVDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether the Major Product Version may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>EnablePatchCache</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Enable Patch cacheing</td></row>
		<row><td>ISPatchConfiguration</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Patching API Flags</td></row>
		<row><td>ISPatchConfiguration</td><td>IncludeWholeFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to build a binary level patch</td></row>
		<row><td>ISPatchConfiguration</td><td>LeaveDecompressed</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to leave intermediate files devcompressed when finished</td></row>
		<row><td>ISPatchConfiguration</td><td>MinMsiVersion</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Minimum Required MSI Version</td></row>
		<row><td>ISPatchConfiguration</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfiguration</td><td>OptimizeForSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Optimize for large files</td></row>
		<row><td>ISPatchConfiguration</td><td>OutputPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Location</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchCacheDir</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to recieve the Patch Cache information</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuid</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Unique Patch Identifier</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuidsToReplace</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of Patch Guids to unregister</td></row>
		<row><td>ISPatchConfiguration</td><td>TargetProductCodes</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of target Product Codes</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>ISPatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration Property value</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the Patch Configuration Property</td></row>
		<row><td>ISPatchExternalFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filekey</td></row>
		<row><td>ISPatchExternalFile</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filepath</td></row>
		<row><td>ISPatchExternalFile</td><td>ISUpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the isupgraded image table</td></row>
		<row><td>ISPatchExternalFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Uniqu name to identify this record.</td></row>
		<row><td>ISPatchWholeFile</td><td>Component</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Component containing file key</td></row>
		<row><td>ISPatchWholeFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key of file to be included as whole</td></row>
		<row><td>ISPatchWholeFile</td><td>UpgradedImage</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to ISUpgradedImage Table</td></row>
		<row><td>ISPathVariable</td><td>ISPathVariable</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The name of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>TestValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The test value of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4;8</td><td>The type of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The value of the path variable.</td></row>
		<row><td>ISProductConfiguration</td><td>GeneratePackageCode</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td>0;1</td><td>Indicates whether or not to generate a package code.</td></row>
		<row><td>ISProductConfiguration</td><td>ISProductConfiguration</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the product configuration.</td></row>
		<row><td>ISProductConfiguration</td><td>ProductConfigurationFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Product configuration (release) flags.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>InstanceId</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Identifies the instance number of this instance. This value is stored in the Property InstanceId.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Property</td><td>N</td><td/><td/><td>Property</td><td>1</td><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property. Never null or empty.</td></row>
		<row><td>ISRelease</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISRelease</td><td>BuildLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build location.</td></row>
		<row><td>ISRelease</td><td>CDBrowser</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Demoshield browser location.</td></row>
		<row><td>ISRelease</td><td>DefaultLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Default language for setup.</td></row>
		<row><td>ISRelease</td><td>DigitalPVK</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing private key (.pvk) file.</td></row>
		<row><td>ISRelease</td><td>DigitalSPC</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing Software Publisher Certificate (.spc) file.</td></row>
		<row><td>ISRelease</td><td>DigitalURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing URL.</td></row>
		<row><td>ISRelease</td><td>DiskClusterSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Disk cluster size.</td></row>
		<row><td>ISRelease</td><td>DiskSize</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk size.</td></row>
		<row><td>ISRelease</td><td>DiskSizeUnit</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk size units (KB or MB).</td></row>
		<row><td>ISRelease</td><td>DiskSpanning</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk spanning (automatic, enforce size, etc.).</td></row>
		<row><td>ISRelease</td><td>DotNetBuildConfiguration</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Configuration for .NET solutions.</td></row>
		<row><td>ISRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISRelease</td><td>ISRelease</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISRelease</td><td>ISSetupPrerequisiteLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3</td><td>Location the Setup Prerequisites will be placed in</td></row>
		<row><td>ISRelease</td><td>MediaLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location on disk.</td></row>
		<row><td>ISRelease</td><td>MsiCommandLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command line passed to the msi package from setup.exe</td></row>
		<row><td>ISRelease</td><td>MsiSourceType</td><td>N</td><td>-1</td><td>4</td><td/><td/><td/><td/><td>MSI media source type.</td></row>
		<row><td>ISRelease</td><td>PackageName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Package name.</td></row>
		<row><td>ISRelease</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password.</td></row>
		<row><td>ISRelease</td><td>Platforms</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Platforms supported (Intel, Alpha, etc.).</td></row>
		<row><td>ISRelease</td><td>ReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Release flags.</td></row>
		<row><td>ISRelease</td><td>ReleaseType</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4</td><td>Release type (single, uncompressed, etc.).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Languages supported (for component filtering).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesUI</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>UI languages supported.</td></row>
		<row><td>ISRelease</td><td>SupportedOSs</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Indicate which operating systmes are supported.</td></row>
		<row><td>ISRelease</td><td>SynchMsi</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>MSI file to synchronize file keys and other data with (patch-like functionality).</td></row>
		<row><td>ISRelease</td><td>Type</td><td>N</td><td>0</td><td>6</td><td/><td/><td/><td/><td>Release type (CDROM, Network, etc.).</td></row>
		<row><td>ISRelease</td><td>URLLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location via URL.</td></row>
		<row><td>ISRelease</td><td>VersionCopyright</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Version stamp information.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property name</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property value</td></row>
		<row><td>ISReleaseExtended</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISReleaseExtended</td><td>CertPassword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital certificate password</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateDBaseNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate database for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateIdNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate ID for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificatePasswordNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetBaseLanguage</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Base Languge of .NET Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetFxCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to DotNetFx.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangPackCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to LangPack.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangaugePacks</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>.NET Redist language packs to include</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of .NET framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .NET framework Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetVersion</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Version of .NET framework Redist (1.0, 1.1)</td></row>
		<row><td>ISReleaseExtended</td><td>EngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of msi engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of ISScript  engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to InstallShield scripting engine</td></row>
		<row><td>ISReleaseExtended</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseExtended</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to vjredist.exe</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of J# framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>MsiEngineVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding selected MSI engine versions included in this release</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickCabName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickHtmlName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated html page</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickTargetBrowser</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Target browser (IE, Netscape, both...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebCabSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of the cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>WebLocalCachePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to cache downloaded package</td></row>
		<row><td>ISReleaseExtended</td><td>WebType</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Type of web install (One Executable, Downloader...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .msi package</td></row>
		<row><td>ISReleaseExtended</td><td>Win9xMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Ansi MSI engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinMsi30Url</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to MSI 3.0 engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinNTMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Unicode MSI engine</td></row>
		<row><td>ISReleaseProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISReleaseProperty</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISReleaseProperty</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISReleaseProperty</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISReleasePublishInfo</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item description</td></row>
		<row><td>ISReleasePublishInfo</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item display name</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding various attributes</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleasePublishInfo</td><td>Publisher</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item publisher</td></row>
		<row><td>ISReleasePublishInfo</td><td>Repository</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository which to  publish the built merge module</td></row>
		<row><td>ISSQLConnection</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>BatchSeparator</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>CmdTimeout</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Database</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ISSQLConnection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnection record.</td></row>
		<row><td>ISSQLConnection</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Password</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Server</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>UserName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnectionDBServer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnectionDBServer record.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLDBMetaData_</td><td>N</td><td/><td/><td>ISSQLDBMetaData</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLDBMetaData table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnAdditional</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDatabase</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDriver</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnNetLibrary</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPassword</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPort</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnServer</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnUserID</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnWindowsSecurity</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoDriverName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DsnODBCName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISSQLDBMetaData</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLDBMetaData record.</td></row>
		<row><td>ISSQLDBMetaData</td><td>InsertRecordCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>LocalInstanceNames</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>QueryDatabasesCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_ColumnType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SelectTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SwitchDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestDatabaseCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionBeginToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionEndToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionInfoCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>WinAuthentUserId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnectionDBServer_</td><td>Y</td><td/><td/><td>ISSQLConnectionDBServer</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnectionDBServer table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLRequirement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLRequirement record.</td></row>
		<row><td>ISSQLRequirement</td><td>MajorVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ServicePackLevel</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrNumber</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ISSQLScriptFile_</td><td>Y</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptError</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Custom end-user message. Reserved for future use.</td></row>
		<row><td>ISSQLScriptFile</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comments</td></row>
		<row><td>ISSQLScriptFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the SQL script.</td></row>
		<row><td>ISSQLScriptFile</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this script if the specified condition evaluates to the 'False' state. If a script is disabled, it will not be installed regardless of the 'Action' state associated with the component.</td></row>
		<row><td>ISSQLScriptFile</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Display name for the SQL script file.</td></row>
		<row><td>ISSQLScriptFile</td><td>ErrorHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISSQLScriptFile</td><td>ISSQLScriptFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptFile</td><td>InstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at install</td></row>
		<row><td>ISSQLScriptFile</td><td>Scheduling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>UninstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at Uninstall</td></row>
		<row><td>ISSQLScriptFile</td><td>Version</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Schema Version (#####.#####.#####.#####)</td></row>
		<row><td>ISSQLScriptImport</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Database</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ExcludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptImport</td><td>IncludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Server</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>UserName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptReplace</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLScriptReplace record.</td></row>
		<row><td>ISSQLScriptReplace</td><td>Replace</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Search</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISScriptFile</td><td>ISScriptFile</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the full path of the script file. The path portion may be expressed in path variable form.</td></row>
		<row><td>ISSelfReg</td><td>CmdLine</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>Cost</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>FileKey</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key to the file table</td></row>
		<row><td>ISSelfReg</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the file name to use when streaming the file to the support files location</td></row>
		<row><td>ISSetupFile</td><td>ISSetupFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSetupFile table</td></row>
		<row><td>ISSetupFile</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Four digit language identifier.  0 for Language Neutral</td></row>
		<row><td>ISSetupFile</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Link to the source file on the build machine</td></row>
		<row><td>ISSetupFile</td><td>Splash</td><td>Y</td><td/><td/><td/><td/><td>Short</td><td/><td>Boolean value indication whether his setup file entry belongs in the Splasc Screen section</td></row>
		<row><td>ISSetupFile</td><td>Stream</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The bits to stream to the support location</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this prereq  will be included in a particular release.</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupPrerequisites</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupType</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>ISSetupType</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>ISSetupType</td><td>Display</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>ISSetupType</td><td>Display_Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>ISSetupType</td><td>ISSetupType</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>ISSetupType_</td><td>N</td><td/><td/><td>ISSetupType</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSetupType table.</td></row>
		<row><td>ISStorages</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Path to the file to stream into sub-storage</td></row>
		<row><td>ISStorages</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Name of the sub-storage key</td></row>
		<row><td>ISString</td><td>Comment</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comment</td></row>
		<row><td>ISString</td><td>Encoded</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Encoding for multi-byte strings.</td></row>
		<row><td>ISString</td><td>ISLanguage_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISString</td><td>ISString</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String id.</td></row>
		<row><td>ISString</td><td>TimeStamp</td><td>Y</td><td/><td/><td/><td/><td>Time/Date</td><td/><td>Time Stamp. MSI's Time/Date column type is just an int, with bits packed in a certain order.</td></row>
		<row><td>ISString</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>real string value.</td></row>
		<row><td>ISSwidtagProperty</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISSwidtagProperty</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property value</td></row>
		<row><td>ISTargetImage</td><td>Flags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>IgnoreMissingFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>If true, ignore missing source files when creating patch</td></row>
		<row><td>ISTargetImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the target image</td></row>
		<row><td>ISTargetImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the TargetImage</td></row>
		<row><td>ISTargetImage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>foreign key to the upgraded Image table</td></row>
		<row><td>ISUpgradeMsiItem</td><td>ISAttributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1</td><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ObjectSetupPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The path to the setup you want to upgrade.</td></row>
		<row><td>ISUpgradeMsiItem</td><td>UpgradeItem</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the Upgrade Item.</td></row>
		<row><td>ISUpgradedImage</td><td>Family</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the image family</td></row>
		<row><td>ISUpgradedImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the upgraded image</td></row>
		<row><td>ISUpgradedImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the UpgradedImage</td></row>
		<row><td>ISVirtualDirectory</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISVirtualDirectory</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualDirectory</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File  table.</td></row>
		<row><td>ISVirtualFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualPackage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualPackage</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRegistry</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualRegistry</td><td>Registry_</td><td>N</td><td/><td/><td>Registry</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Registry table.</td></row>
		<row><td>ISVirtualRegistry</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISVirtualRelease</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISVirtualRelease</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISVirtualRelease</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualShortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualShortcut</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Shortcut table.</td></row>
		<row><td>ISVirtualShortcut</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISWSEWrap</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into CustomAction table</td></row>
		<row><td>ISWSEWrap</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property associated with this Action</td></row>
		<row><td>ISWSEWrap</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value associated with this Property</td></row>
		<row><td>ISXmlElement</td><td>Content</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Element contents</td></row>
		<row><td>ISXmlElement</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML element attributes</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement_Parent</td><td>Y</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElement</td><td>ISXmlFile_</td><td>N</td><td/><td/><td>ISXmlFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into XmlFile table.</td></row>
		<row><td>ISXmlElement</td><td>XPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XPath fragment including any operators</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML elementattib attributes</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElementAttrib</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element attribute</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElement_</td><td>N</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElementAttrib</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute name</td></row>
		<row><td>ISXmlElementAttrib</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute value</td></row>
		<row><td>ISXmlFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>ISXmlFile</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISXmlFile</td><td>Encoding</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XML File Encoding</td></row>
		<row><td>ISXmlFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized XML file name</td></row>
		<row><td>ISXmlFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML file attributes</td></row>
		<row><td>ISXmlFile</td><td>ISXmlFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized,internal token for Xml file</td></row>
		<row><td>ISXmlFile</td><td>SelectionNamespaces</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Selection namespaces</td></row>
		<row><td>ISXmlLocator</td><td>Attribute</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>The name of an attribute within the XML element.</td></row>
		<row><td>ISXmlLocator</td><td>Element</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>XPath query that will locate an element in an XML file.</td></row>
		<row><td>ISXmlLocator</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISXmlLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table.</td></row>
		<row><td>ISXmlLocator</td><td>Signature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, ISXmlLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>Icon</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Icon</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Icon</td><td>ISIconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index to be extracted.</td></row>
		<row><td>Icon</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key. Name of the icon file.</td></row>
		<row><td>IniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;3</td><td>The type of modification to be made, one of iifEnum</td></row>
		<row><td>IniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the .INI value.</td></row>
		<row><td>IniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>IniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to write the information</td></row>
		<row><td>IniFile</td><td>IniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>IniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>IniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>IniFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be written.</td></row>
		<row><td>IniLocator</td><td>Field</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The field in the .INI line. If Field is null or 0 the entire line is read.</td></row>
		<row><td>IniLocator</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name.</td></row>
		<row><td>IniLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key value (followed by an equals sign in INI file).</td></row>
		<row><td>IniLocator</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Section name within in file (within square brackets in INI file).</td></row>
		<row><td>IniLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>IniLocator</td><td>Type</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>An integer value that determines if the .INI value read is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>InstallExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>InstallShield</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>InstallShield</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>InstallUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>IsolatedComponent</td><td>Component_Application</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item for application</td></row>
		<row><td>IsolatedComponent</td><td>Component_Shared</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item to be isolated</td></row>
		<row><td>LaunchCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression which must evaluate to TRUE in order for install to commence.</td></row>
		<row><td>LaunchCondition</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable text to display when condition fails and install must abort.</td></row>
		<row><td>ListBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listbox.</td></row>
		<row><td>ListBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>ListView</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>The name of the icon to be displayed with the icon. The binary information is looked up from the Binary Table.</td></row>
		<row><td>ListView</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListView</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listview.</td></row>
		<row><td>ListView</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListView</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>LockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set. (usually a property)</td></row>
		<row><td>LockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry or File table</td></row>
		<row><td>LockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.  Full Control = 268435456 (GENERIC_ALL = 0x10000000)</td></row>
		<row><td>LockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>Directory;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>LockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set.  (usually a property)</td></row>
		<row><td>MIME</td><td>CLSID</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>Optional associated CLSID.</td></row>
		<row><td>MIME</td><td>ContentType</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Primary key. Context identifier, typically "type/format".</td></row>
		<row><td>MIME</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>Optional associated extension (without dot)</td></row>
		<row><td>Media</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>Media</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>Media</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>Media</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>Media</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>Media</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>MoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>If this component is not "selected" for installation or removal, no action will be taken on the associated MoveFile entry</td></row>
		<row><td>MoveFile</td><td>DestFolder</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>MoveFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name to be given to the original file after it is moved or copied.  If blank, the destination file will be given the same name as the source file</td></row>
		<row><td>MoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular MoveFile record</td></row>
		<row><td>MoveFile</td><td>Options</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Integer value specifying the MoveFile operating mode, one of imfoEnum</td></row>
		<row><td>MoveFile</td><td>SourceFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the source directory</td></row>
		<row><td>MoveFile</td><td>SourceName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the source file(s) to be moved or copied.  Can contain the '*' or '?' wildcards.</td></row>
		<row><td>MsiAssembly</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Assembly attributes</td></row>
		<row><td>MsiAssembly</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssembly</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>MsiAssembly</td><td>File_Application</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File table, denoting the application context for private assemblies. Null for global assemblies.</td></row>
		<row><td>MsiAssembly</td><td>File_Manifest</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the manifest file for the assembly.</td></row>
		<row><td>MsiAssemblyName</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiAssemblyName</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiDigitalCertificate</td><td>CertData</td><td>N</td><td/><td/><td/><td/><td>Binary</td><td/><td>A certificate context blob for a signer certificate</td></row>
		<row><td>MsiDigitalCertificate</td><td>DigitalCertificate</td><td>N</td><td/><td/><td>MsiPackageCertificate</td><td>2</td><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiDigitalSignature</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>Foreign key to MsiDigitalCertificate table identifying the signer certificate</td></row>
		<row><td>MsiDigitalSignature</td><td>Hash</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>The encoded hash blob from the digital signature</td></row>
		<row><td>MsiDigitalSignature</td><td>SignObject</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key to Media table</td></row>
		<row><td>MsiDigitalSignature</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Reference to another table name (only Media table is supported)</td></row>
		<row><td>MsiDriverPackages</td><td>Component</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>MsiDriverPackages</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Driver package flags</td></row>
		<row><td>MsiDriverPackages</td><td>ReferenceComponents</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiDriverPackages</td><td>Sequence</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Installation sequence number</td></row>
		<row><td>MsiEmbeddedChainer</td><td>CommandLine</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>MsiEmbeddedChainer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>CustomSource</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Integer</td><td>2;18;50</td><td/></row>
		<row><td>MsiEmbeddedUI</td><td>Attributes</td><td>N</td><td>0</td><td>3</td><td/><td/><td>Integer</td><td/><td>Information about the data in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>This column contains binary information.</td></row>
		<row><td>MsiEmbeddedUI</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>The name of the file that receives the binary information in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>MsiEmbeddedUI</td><td>MessageFilter</td><td>Y</td><td>0</td><td>234913791</td><td/><td/><td>Integer</td><td/><td>Specifies the types of messages that are sent to the user interface DLL. This column is only relevant for rows with the msidbEmbeddedUI attribute.</td></row>
		<row><td>MsiEmbeddedUI</td><td>MsiEmbeddedUI</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The primary key for the table.</td></row>
		<row><td>MsiFileHash</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing file with this hash</td></row>
		<row><td>MsiFileHash</td><td>HashPart1</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart2</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart3</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart4</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>Options</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Various options and attributes for this hash.</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Expression which must evaluate to TRUE in order for this set of permissions to be applied</td></row>
		<row><td>MsiLockPermissionsEx</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry, File, CreateFolder, or ServiceInstall table</td></row>
		<row><td>MsiLockPermissionsEx</td><td>MsiLockPermissionsEx</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiLockPermissionsEx</td><td>SDDLText</td><td>N</td><td/><td/><td/><td/><td>FormattedSDDLText</td><td/><td>String to indicate permissions to be applied to the LockObject</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry;ServiceInstall</td><td>Reference to another table name</td></row>
		<row><td>MsiPackageCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPackageCertificate</td><td>PackageCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPatchCertificate</td><td>PatchCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchMetadata</td><td>Company</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional company name</td></row>
		<row><td>MsiPatchMetadata</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfiguration table</td></row>
		<row><td>MsiPatchMetadata</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the metadata</td></row>
		<row><td>MsiPatchMetadata</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the metadata</td></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>Assembly_</td><td>Y</td><td/><td/><td>MsiPatchOldAssemblyName</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Assembly</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchSequence</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the patch configuration table</td></row>
		<row><td>MsiPatchSequence</td><td>PatchFamily</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the family to which this patch belongs</td></row>
		<row><td>MsiPatchSequence</td><td>Sequence</td><td>N</td><td/><td/><td/><td/><td>Version</td><td/><td>The version of this patch in this family</td></row>
		<row><td>MsiPatchSequence</td><td>Supersede</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td>Supersede</td></row>
		<row><td>MsiPatchSequence</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Target product codes for this patch family</td></row>
		<row><td>MsiServiceConfig</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Argument(s) for service configuration. Value depends on the content of the ConfigType field</td></row>
		<row><td>MsiServiceConfig</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfig</td><td>ConfigType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Service Configuration Option</td></row>
		<row><td>MsiServiceConfig</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfig</td><td>MsiServiceConfig</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>MsiServiceConfig</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Actions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of integer actions separated by [~] delimiters: 0 = SC_ACTION_NONE, 1 = SC_ACTION_RESTART, 2 = SC_ACTION_REBOOT, 3 = SC_ACTION_RUN_COMMAND. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Command line of the process to CreateProcess function to execute</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>DelayActions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of delays (time in milli-seconds), separated by [~] delmiters, to wait before taking the corresponding Action. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>MsiServiceConfigFailureActions</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>RebootMessage</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Message to be broadcast to server users before rebooting</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>ResetPeriod</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Time in seconds after which to reset the failure count to zero. Leave blank if it should never be reset</td></row>
		<row><td>MsiShortcutProperty</td><td>MsiShortcutProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiShortcutProperty</td><td>PropVariantValue</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>String representation of the value in the property</td></row>
		<row><td>MsiShortcutProperty</td><td>PropertyKey</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Canonical string representation of the Property Key being set</td></row>
		<row><td>MsiShortcutProperty</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Shortcut table</td></row>
		<row><td>ODBCAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC driver attribute</td></row>
		<row><td>ODBCAttribute</td><td>Driver_</td><td>N</td><td/><td/><td>ODBCDriver</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC driver in ODBCDriver table</td></row>
		<row><td>ODBCAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC driver attribute</td></row>
		<row><td>ODBCDataSource</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDataSource</td><td>DataSource</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for data source</td></row>
		<row><td>ODBCDataSource</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for data source</td></row>
		<row><td>ODBCDataSource</td><td>DriverDescription</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Reference to driver description, may be existing driver</td></row>
		<row><td>ODBCDataSource</td><td>Registration</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Registration option: 0=machine, 1=user, others t.b.d.</td></row>
		<row><td>ODBCDriver</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDriver</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for driver, non-localized</td></row>
		<row><td>ODBCDriver</td><td>Driver</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for driver</td></row>
		<row><td>ODBCDriver</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key driver file</td></row>
		<row><td>ODBCDriver</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key driver setup DLL</td></row>
		<row><td>ODBCSourceAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC data source attribute</td></row>
		<row><td>ODBCSourceAttribute</td><td>DataSource_</td><td>N</td><td/><td/><td>ODBCDataSource</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC data source in ODBCDataSource table</td></row>
		<row><td>ODBCSourceAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC data source attribute</td></row>
		<row><td>ODBCTranslator</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCTranslator</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for translator</td></row>
		<row><td>ODBCTranslator</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key translator file</td></row>
		<row><td>ODBCTranslator</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key translator setup DLL</td></row>
		<row><td>ODBCTranslator</td><td>Translator</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for translator</td></row>
		<row><td>Patch</td><td>Attributes</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing patch attributes</td></row>
		<row><td>Patch</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, non-localized token, foreign key to File table, must match identifier in cabinet.</td></row>
		<row><td>Patch</td><td>Header</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The patch header, used for patch validation.</td></row>
		<row><td>Patch</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to patch header.</td></row>
		<row><td>Patch</td><td>PatchSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of patch in bytes (long integer).</td></row>
		<row><td>Patch</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Primary key, sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>Patch</td><td>StreamRef_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>External key into the MsiPatchHeaders table specifying the row that contains the patch header stream.</td></row>
		<row><td>PatchPackage</td><td>Media_</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Foreign key to DiskId column of Media table. Indicates the disk containing the patch package.</td></row>
		<row><td>PatchPackage</td><td>PatchId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A unique string GUID representing this patch.</td></row>
		<row><td>ProgId</td><td>Class_</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>The CLSID of an OLE factory corresponding to the ProgId.</td></row>
		<row><td>ProgId</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Program identifier.</td></row>
		<row><td>ProgId</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component, like ExtractIcon, etc.</td></row>
		<row><td>ProgId</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>ProgId</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this ProgId. Will be written under the DefaultIcon key.</td></row>
		<row><td>ProgId</td><td>ProgId</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The Program Identifier. Primary key.</td></row>
		<row><td>ProgId</td><td>ProgId_Parent</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>The Parent Program Identifier. If specified, the ProgId column becomes a version independent prog id.</td></row>
		<row><td>Property</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Property</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>Property</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>PublishComponent</td><td>AppData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is localisable Application specific data that can be associated with a Qualified Component.</td></row>
		<row><td>PublishComponent</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID that represents the component id that will be requested by the alien product.</td></row>
		<row><td>PublishComponent</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>PublishComponent</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Feature table.</td></row>
		<row><td>PublishComponent</td><td>Qualifier</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is defined only when the ComponentId column is an Qualified Component Id. This is the Qualifier for ProvideComponentIndirect.</td></row>
		<row><td>RadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>RadioButton</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>RadioButton</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>RadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>RadioButton</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>RadioButton</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible title to be assigned to the radio button.</td></row>
		<row><td>RadioButton</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this button. Selecting the button will set the associated property to this value.</td></row>
		<row><td>RadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>RadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RegLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RegLocator</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RegLocator</td><td>Root</td><td>N</td><td>0</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>RegLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table. If the type is 0, the registry values refers a directory, and _Signature is not a foreign key.</td></row>
		<row><td>RegLocator</td><td>Type</td><td>Y</td><td>0</td><td>18</td><td/><td/><td/><td/><td>An integer value that determines if the registry value is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>Registry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the registry value.</td></row>
		<row><td>Registry</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a registry item.  Currently the only one is Automatic.</td></row>
		<row><td>Registry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>Registry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>Registry</td><td>Registry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Registry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>Registry</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The registry value.</td></row>
		<row><td>RemoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>DirProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to the folder of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>RemoveFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>InstallMode</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;3</td><td>Installation option, one of iimEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>2;4</td><td>The type of modification to be made, one of iifEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the .INI value.</td></row>
		<row><td>RemoveIniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>RemoveIniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to delete the information</td></row>
		<row><td>RemoveIniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>RemoveIniFile</td><td>RemoveIniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveIniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>RemoveIniFile</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be deleted. The value is required when Action is iifIniRemoveTag</td></row>
		<row><td>RemoveRegistry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RemoveRegistry</td><td>RemoveRegistry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveRegistry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum</td></row>
		<row><td>ReserveCost</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reserve a specified amount of space if this component is to be installed.</td></row>
		<row><td>ReserveCost</td><td>ReserveFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>ReserveCost</td><td>ReserveKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular ReserveCost record</td></row>
		<row><td>ReserveCost</td><td>ReserveLocal</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed locally.</td></row>
		<row><td>ReserveCost</td><td>ReserveSource</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed to run from the source location.</td></row>
		<row><td>SFPCatalog</td><td>Catalog</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>SFP Catalog</td></row>
		<row><td>SFPCatalog</td><td>Dependency</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Parent catalog - only used by SFP</td></row>
		<row><td>SFPCatalog</td><td>SFPCatalog</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>File name for the catalog.</td></row>
		<row><td>SelfReg</td><td>Cost</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The cost of registering the module.</td></row>
		<row><td>SelfReg</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the module that needs to be registered.</td></row>
		<row><td>ServiceControl</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments for the service.  Separate by [~].</td></row>
		<row><td>ServiceControl</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceControl</td><td>Event</td><td>N</td><td>0</td><td>187</td><td/><td/><td/><td/><td>Bit field:  Install:  0x1 = Start, 0x2 = Stop, 0x8 = Delete, Uninstall: 0x10 = Start, 0x20 = Stop, 0x80 = Delete</td></row>
		<row><td>ServiceControl</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>ServiceControl</td><td>ServiceControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceControl</td><td>Wait</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Boolean for whether to wait for the service to fully start</td></row>
		<row><td>ServiceInstall</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments to include in every start of the service, passed to WinMain</td></row>
		<row><td>ServiceInstall</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceInstall</td><td>Dependencies</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Other services this depends on to start.  Separate by [~], and end with [~][~]</td></row>
		<row><td>ServiceInstall</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of service.</td></row>
		<row><td>ServiceInstall</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>External Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>ErrorControl</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Severity of error if service fails to start</td></row>
		<row><td>ServiceInstall</td><td>LoadOrderGroup</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>LoadOrderGroup</td></row>
		<row><td>ServiceInstall</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Internal Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>password to run service with.  (with StartName)</td></row>
		<row><td>ServiceInstall</td><td>ServiceInstall</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceInstall</td><td>ServiceType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>ServiceInstall</td><td>StartName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>User or object name to run service as</td></row>
		<row><td>ServiceInstall</td><td>StartType</td><td>N</td><td>0</td><td>4</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>Shortcut</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command-line arguments for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table denoting the component whose selection gates the the shortcut creation/deletion.</td></row>
		<row><td>Shortcut</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The description for the shortcut.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The description name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the shortcut file is created.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The display name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Hotkey</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The hotkey for the shortcut. It has the virtual-key code for the key in the low-order byte, and the modifier flags in the high-order byte.</td></row>
		<row><td>Shortcut</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a shortcut.  Mainly used in pro project types.</td></row>
		<row><td>Shortcut</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Shortcut.</td></row>
		<row><td>Shortcut</td><td>ISShortcutName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A non-unique name for the shortcut.  Mainly used by pro pro project types.</td></row>
		<row><td>Shortcut</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>The icon index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the external icon file for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the shortcut to be created.</td></row>
		<row><td>Shortcut</td><td>Shortcut</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Shortcut</td><td>ShowCmd</td><td>Y</td><td/><td/><td/><td/><td/><td>1;3;7</td><td>The show command for the application window.The following values may be used.</td></row>
		<row><td>Shortcut</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Shortcut</td><td/><td>The shortcut target. This is usually a property that is expanded to a file or a folder that the shortcut points to.</td></row>
		<row><td>Shortcut</td><td>WkDir</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property defining location of working directory.</td></row>
		<row><td>Signature</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the file. This may contain a "short name|long name" pair.</td></row>
		<row><td>Signature</td><td>Languages</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>The languages supported by the file.</td></row>
		<row><td>Signature</td><td>MaxDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum creation date of the file.</td></row>
		<row><td>Signature</td><td>MaxSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum size of the file.</td></row>
		<row><td>Signature</td><td>MaxVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum version of the file.</td></row>
		<row><td>Signature</td><td>MinDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum creation date of the file.</td></row>
		<row><td>Signature</td><td>MinSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum size of the file.</td></row>
		<row><td>Signature</td><td>MinVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum version of the file.</td></row>
		<row><td>Signature</td><td>Signature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The table key. The Signature represents a unique file signature.</td></row>
		<row><td>TextStyle</td><td>Color</td><td>Y</td><td>0</td><td>16777215</td><td/><td/><td/><td/><td>A long integer indicating the color of the string in the RGB format (Red, Green, Blue each 0-255, RGB = R + 256*G + 256^2*B).</td></row>
		<row><td>TextStyle</td><td>FaceName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A string indicating the name of the font used. Required. The string must be at most 31 characters long.</td></row>
		<row><td>TextStyle</td><td>Size</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The size of the font used. This size is given in our units (1/12 of the system font height). Assuming that the system font is set to 12 point size, this is equivalent to the point size.</td></row>
		<row><td>TextStyle</td><td>StyleBits</td><td>Y</td><td>0</td><td>15</td><td/><td/><td/><td/><td>A combination of style bits.</td></row>
		<row><td>TextStyle</td><td>TextStyle</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the style. The primary key of this table. This name is embedded in the texts to indicate a style change.</td></row>
		<row><td>TypeLib</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>TypeLib</td><td>Cost</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The cost associated with the registration of the typelib. This column is currently optional.</td></row>
		<row><td>TypeLib</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>TypeLib</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Optional. The foreign key into the Directory table denoting the path to the help file for the type library.</td></row>
		<row><td>TypeLib</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the type library to be operational.</td></row>
		<row><td>TypeLib</td><td>Language</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The language of the library.</td></row>
		<row><td>TypeLib</td><td>LibID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The GUID that represents the library.</td></row>
		<row><td>TypeLib</td><td>Version</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The version of the library. The major version is in the upper 8 bits of the short integer. The minor version is in the lower 8 bits.</td></row>
		<row><td>UIText</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key that identifies the particular string.</td></row>
		<row><td>UIText</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The localized version of the string.</td></row>
		<row><td>Upgrade</td><td>ActionProperty</td><td>N</td><td/><td/><td/><td/><td>UpperCase</td><td/><td>The property to set when a product in this set is found.</td></row>
		<row><td>Upgrade</td><td>Attributes</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The attributes of this product set.</td></row>
		<row><td>Upgrade</td><td>ISDisplayName</td><td>Y</td><td/><td/><td>ISUpgradeMsiItem</td><td>1</td><td/><td/><td/></row>
		<row><td>Upgrade</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>A comma-separated list of languages for either products in this set or products not in this set.</td></row>
		<row><td>Upgrade</td><td>Remove</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The list of features to remove when uninstalling a product from this set.  The default is "ALL".</td></row>
		<row><td>Upgrade</td><td>UpgradeCode</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The UpgradeCode GUID belonging to the products in this set.</td></row>
		<row><td>Upgrade</td><td>VersionMax</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Upgrade</td><td>VersionMin</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Verb</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Optional value for the command arguments.</td></row>
		<row><td>Verb</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command text.</td></row>
		<row><td>Verb</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Verb</td><td>Sequence</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Order within the verbs for a particular extension. Also used simply to specify the default verb.</td></row>
		<row><td>Verb</td><td>Verb</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The verb for the command.</td></row>
		<row><td>_Validation</td><td>Category</td><td>Y</td><td/><td/><td/><td/><td/><td>"Text";"Formatted";"Template";"Condition";"Guid";"Path";"Version";"Language";"Identifier";"Binary";"UpperCase";"LowerCase";"Filename";"Paths";"AnyPath";"WildCardFilename";"RegPath";"KeyFormatted";"CustomSource";"Property";"Cabinet";"Shortcut";"URL";"DefaultDir"</td><td>String category</td></row>
		<row><td>_Validation</td><td>Column</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of column</td></row>
		<row><td>_Validation</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of column</td></row>
		<row><td>_Validation</td><td>KeyColumn</td><td>Y</td><td>1</td><td>32</td><td/><td/><td/><td/><td>Column to which foreign key connects</td></row>
		<row><td>_Validation</td><td>KeyTable</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>For foreign key, Name of table to which data must link</td></row>
		<row><td>_Validation</td><td>MaxValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Maximum value allowed</td></row>
		<row><td>_Validation</td><td>MinValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Minimum value allowed</td></row>
		<row><td>_Validation</td><td>Nullable</td><td>N</td><td/><td/><td/><td/><td/><td>Y;N;@</td><td>Whether the column is nullable</td></row>
		<row><td>_Validation</td><td>Set</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Set of values that are permitted</td></row>
		<row><td>_Validation</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of table</td></row>
	</table>
</msi>
