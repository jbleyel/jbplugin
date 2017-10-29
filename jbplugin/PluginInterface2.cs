using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Invelos.DVDProfilerPlugin
{
	class PluginConstants
	{
		public const int API_VERSION = 2;

		public const int FORMID_Main        = 0;
		public const int FORMID_AddDVDs     = 1;
		public const int FORMID_Reports     = 2;
		public const int FORMID_Welcome     = 3;
		public const int FORMID_Options     = 4;
		public const int FORMID_EditProfile = 5;
		public const int FORMID_Personalize = 6;
		public const int FORMID_LoadLayout  = 7;

		// Action IDs
		// FORMID_AddDVDs
		public const int ACTIONID_AddDVDs_GetPendingList = 0;

		// Standard Action result error codes
		public const int ACTIONResult_Unknown       = -1;
		public const int ACTIONResult_FormNotLoaded = -2;

		// Menu IDs
		public const int MENUID_Form = 0;
		// Main form popup menu IDs
		public const int MENUID_Main_Collection = 1;
		// Add DVDs form popup menu IDs
		public const int MENUID_AddDVDs_TitleListing = 1;
		public const int MENUID_AddDVDs_PendingListing = 2;

		// Event IDs
		// General Events
		public const int EVENTID_Exception              = 0;   // EventData is error description
		public const int EVENTID_PluginUnloading        = 1;   // EventData is null
		// Custom Plugin Events
		public const int EVENTID_CustomMenuClick        = 100; // EventData is Custom Event ID
		public const int EVENTID_CustomPluginOptions    = 101; // EventData is null
		// Program Events
		public const int EVENTID_ProgramLoaded          = 200; // EventData is null
		public const int EVENTID_ProgramMinimized       = 201; // EventData is null
		public const int EVENTID_ProgramRestored        = 202; // EventData is null
		public const int EVENTID_ProgramGotFocus        = 203; // EventData is null
		public const int EVENTID_ProgramLostFocus       = 204; // EventData is null
		public const int EVENTID_ProgramLayoutSaving    = 205; // EventData is null
		public const int EVENTID_ProgramLayoutLoaded    = 206; // EventData is Layout file name
		// Form Events
		public const int EVENTID_FormCreated            = 300; // EventData is Form ID
		public const int EVENTID_FormDestroyed          = 301; // EventData is Form ID
		public const int EVENTID_HostWindowClosed       = 302; // EventData is Host window's WindowHandle
		// Profile Events
		public const int EVENTID_DVDSelected            = 400; // EventData is Profile ID
		public const int EVENTID_DVDEditStart           = 401; // EventData is Profile ID
		public const int EVENTID_DVDEditSave            = 402; // EventData is Profile ID
		public const int EVENTID_DVDEditCancel          = 403; // EventData is Profile ID
		public const int EVENTID_DVDMovedToOwned        = 404; // EventData is Profile ID
		public const int EVENTID_DVDMovedToOrdered      = 405; // EventData is Profile ID
		public const int EVENTID_DVDMovedToWishList     = 406; // EventData is Profile ID
		public const int EVENTID_DVDImagesChanged       = 407; // EventData is image file name
		public const int EVENTID_DVDWatched             = 408; // EventData is Profile ID
		public const int EVENTID_DVDLoaned              = 409; // EventData is Profile ID
		public const int EVENTID_DVDDueDateChanged      = 410; // EventData is Profile ID
		public const int EVENTID_DVDReturned            = 411; // EventData is Profile ID
		public const int EVENTID_DVDHistoryEdited       = 412; // EventData is Profile ID
		public const int EVENTID_DVDTagsChanged         = 413; // EventData is Profile ID
		public const int EVENTID_DVDAdded               = 414; // EventData is Profile ID
		public const int EVENTID_DVDRemoved             = 415; // EventData is Profile ID
		public const int EVENTID_DVDRefreshed           = 416; // EventData is Profile ID
		public const int EVENTID_DVDImagesRefreshed     = 417; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom0      = 418; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom1      = 419; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom2      = 420; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom3      = 421; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom4      = 422; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom5      = 423; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom6      = 424; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom7      = 425; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom8      = 426; // EventData is Profile ID
		public const int EVENTID_DVDMovedToCustom9      = 427; // EventData is Profile ID
		// Flag Events
		public const int EVENTID_DVDFlagged             = 500; // EventData is Profile ID
		public const int EVENTID_DVDUnflagged           = 501; // EventData is Profile ID
		public const int EVENTID_BulkFlagOperationStart = 502; // EventData is null
		public const int EVENTID_BulkFlagOperationEnd   = 503; // EventData is null
		// Collection Display Events
		public const int EVENTID_SortOrderChanged       = 600; // EventData is Sort Order Type
		public const int EVENTID_CollectionListChanged  = 601; // EventData is null
		// Interface Events
		public const int EVENTID_ThemeChanged           = 700; // EventData is name of theme
		// Collection Events
		public const int EVENTID_BackupStarting         = 800; // EventData is file path and name
		public const int EVENTID_BackupFinished         = 801; // EventData is file path and name
		public const int EVENTID_BackupCancelled        = 802; // EventData is file path and name
		public const int EVENTID_RestoreStarting        = 803; // EventData is file path and name
		public const int EVENTID_RestoreFinished        = 804; // EventData is file path and name
		public const int EVENTID_RestoreCancelled       = 805; // EventData is file path and name
		public const int EVENTID_DatabaseOpened         = 806; // EventData is path
		public const int EVENTID_DatabaseClosed	        = 807; // EventData is path
		
		// Field-specific IDs
		public const int GENRE_None             = 0;
		public const int GENRE_Accessories      = 1;
		public const int GENRE_Action           = 2;
		public const int GENRE_Adult            = 3;
		public const int GENRE_Animation        = 4;
		public const int GENRE_Classic          = 5;
		public const int GENRE_Comedy           = 6;
		public const int GENRE_Drama            = 7;
		public const int GENRE_Family           = 8;
		public const int GENRE_Horror           = 10;
		public const int GENRE_Music            = 11;
		public const int GENRE_ScienceFiction   = 12;
		public const int GENRE_SpecialInterest  = 13;
		public const int GENRE_SuspenseThriller = 14;
		public const int GENRE_Western          = 15;
		public const int GENRE_Adventure        = 16;
		public const int GENRE_Romance          = 17;
		public const int GENRE_Fantasy          = 18;
		public const int GENRE_Anime            = 19;
		public const int GENRE_Documentary      = 20;
		public const int GENRE_Musical          = 21;
		public const int GENRE_Television       = 22;
		public const int GENRE_Sports           = 23;
		public const int GENRE_War              = 24;
		public const int GENRE_MartialArts      = 25;
		public const int GENRE_Crime            = 26;
		public const int GENRE_Disaster         = 27;
		public const int GENRE_FilmNoir         = 28;
		public const int GENRE_Childrens        = 29;
		public const int CASETYPE_None      = 0;
		public const int CASETYPE_KeepCase  = 1;
		public const int CASETYPE_Snapper   = 2;
		public const int CASETYPE_Jewel     = 3;
		public const int CASETYPE_Clamshell = 4;
		public const int CASETYPE_Drawer    = 5;
		public const int CASETYPE_Digipak   = 6;
		public const int CASETYPE_Custom    = 7;
		public const int CASETYPE_BoxSet    = 8; // Slip Case
		public const int CASETYPE_Envelope  = 9;
		public const int CASETYPE_SteelBook = 10;
		public const int CASETYPE_Elite     = 11;
		public const int CASETYPE_THINpak   = 12;
		public const int VIDSTD_NTSC        = 0;
		public const int VIDSTD_PAL         = 1;
		public const int FEATURE_SceneAccess     = 0;
		public const int FEATURE_Commentary      = 1;
		public const int FEATURE_Trailer         = 2;
		public const int FEATURE_Gallery         = 3;
		public const int FEATURE_DeletedScenes   = 4;
		public const int FEATURE_Documentary     = 5;
		public const int FEATURE_ProductionNotes = 6;
		public const int FEATURE_InteractiveGame = 7;
		public const int FEATURE_DVDROMContent   = 8;
		public const int FEATURE_MultiAngle      = 9;
		public const int FEATURE_MusicVideos     = 10;
		public const int FEATURE_ClosedCaptioned = 11;
		public const int FEATURE_THX             = 12;
		public const int FEATURE_Interviews      = 13;
		public const int FEATURE_StoryboardComps = 14;
		public const int FEATURE_Bloopers        = 15;
		public const int FEATURE_PIP             = 16;
		public const int FEATURE_BDLive          = 17;
		public const int FEATURE_BonusTrailers   = 18;
		public const int FEATURE_DigitalCopy     = 19;
		public const int SUBTITLE_Afrikaans  = 0;
		public const int SUBTITLE_Arabic     = 1;
		public const int SUBTITLE_Bulgarian  = 2;
		public const int SUBTITLE_Chinese    = 3;
		public const int SUBTITLE_Croatian   = 4;
		public const int SUBTITLE_Czech      = 5;
		public const int SUBTITLE_Danish     = 6;
		public const int SUBTITLE_Dutch      = 7;
		public const int SUBTITLE_English    = 8;
		public const int SUBTITLE_Farsi      = 9;
		public const int SUBTITLE_Finnish    = 10;
		public const int SUBTITLE_French     = 11;
		public const int SUBTITLE_German     = 12;
		public const int SUBTITLE_Greek      = 13;
		public const int SUBTITLE_Hebrew     = 14;
		public const int SUBTITLE_Hindi      = 15;
		public const int SUBTITLE_Hungarian  = 16;
		public const int SUBTITLE_Icelandic  = 17;
		public const int SUBTITLE_Italian    = 18;
		public const int SUBTITLE_Japanese   = 19;
		public const int SUBTITLE_Korean     = 20;
		public const int SUBTITLE_Norwegian  = 21;
		public const int SUBTITLE_Polish     = 22;
		public const int SUBTITLE_Portuguese = 23;
		public const int SUBTITLE_Romanian   = 24;
		public const int SUBTITLE_Russian    = 25;
		public const int SUBTITLE_Spanish    = 26;
		public const int SUBTITLE_Slovakian  = 27;
		public const int SUBTITLE_Slovenian  = 28;
		public const int SUBTITLE_Swedish    = 29;
		public const int SUBTITLE_Tagalog    = 30;
		public const int SUBTITLE_Thai       = 31;
		public const int SUBTITLE_Turkish    = 32;
		public const int SUBTITLE_Xhosa      = 33;
		public const int SUBTITLE_Zulu       = 34;
		public const int SUBTITLE_Commentary = 35;
		public const int SUBTITLE_Trivia     = 36;
		public const int SUBTITLE_Other      = 37;
		public const int LOCK_Entire          = 0;
		public const int LOCK_Scans           = 1;
		public const int LOCK_Title           = 2;
		public const int LOCK_Overview        = 3;
		public const int LOCK_Regions         = 4;
		public const int LOCK_Genres          = 5;
		public const int LOCK_PurchasePrice             = 6;
		public const int LOCK_Studios         = 7;
		public const int LOCK_DiscInformation = 8;
		public const int LOCK_Cast            = 9;
		public const int LOCK_Crew            = 10;
		public const int LOCK_Features        = 11;
		public const int LOCK_AudioTracks     = 12;
		public const int LOCK_Subtitles       = 13;
		public const int LOCK_EasterEggs      = 14;
		public const int LOCK_RunningTime     = 15;
		public const int LOCK_ReleaseDate     = 16;
		public const int LOCK_ProductionYear  = 17;
		public const int LOCK_CaseType        = 18;
		public const int LOCK_VideoFormats    = 19;
		public const int LOCK_BoxSetContents  = 20;
		public const int LOCK_Rating          = 21;
		public const int LOCK_MediaTypes      = 22;
		public const int COLLTYPE_Owned    = 1;
		public const int COLLTYPE_Ordered  = 2;
		public const int COLLTYPE_WishList = 3;
		public const int COLLTYPE_Custom0  = 4;
		public const int COLLTYPE_Custom1  = 5;
		public const int COLLTYPE_Custom2  = 6;
		public const int COLLTYPE_Custom3  = 7;
		public const int COLLTYPE_Custom4  = 8;
		public const int COLLTYPE_Custom5  = 9;
		public const int COLLTYPE_Custom6  = 10;
		public const int COLLTYPE_Custom7  = 11;
		public const int COLLTYPE_Custom8  = 12;
		public const int COLLTYPE_Custom9  = 13;
		public const int CREDIT_Direction        = 0;
		public const int CREDITSUB_Director               = 0;
		public const int CREDIT_Writing          = 1;
		public const int CREDITSUB_OriginalMaterialBy     = 0;
		public const int CREDITSUB_Screenwriter           = 1;
		public const int CREDITSUB_Writer                 = 2;
		public const int CREDITSUB_OriginalCharactersBy   = 3;
		public const int CREDIT_Production       = 2;
		public const int CREDITSUB_Producer               = 0;
		public const int CREDITSUB_ExecutiveProducer      = 1;
		public const int CREDIT_Cinematography   = 3;
		public const int CREDITSUB_Cinematographer        = 0;
		public const int CREDITSUB_DirectorOfPhotography  = 1;
		public const int CREDIT_FilmEditing      = 4;
		public const int CREDITSUB_FilmEditor             = 0;
		public const int CREDIT_Music            = 5;
		public const int CREDITSUB_Composer               = 0;
		public const int CREDITSUB_SongWriter             = 1;
		public const int CREDIT_Sound            = 6;
		public const int CREDITSUB_SoundEditor            = 0;
		public const int CREDITSUB_SoundReRecordingMixer  = 1;
		public const int CREDITSUB_SoundDesigner          = 2;
		public const int CREDITSUB_Sound                  = 3;
		public const int CREDITSUB_SupervisingSoundEditor = 4;
		public const int CREDITSUB_ProductionSoundMixer   = 5;
		public const int CREDIT_Art              = 7;
		public const int CREDITSUB_ProductionDesigner     = 0;
		public const int CREDITSUB_ArtDirector            = 1;

		public const int DIVIDER_Episode = 1;
		public const int DIVIDER_Group   = 2;
		public const int DIVIDER_Team    = 3;
		public const int DIVIDER_EndDiv  = 4;

		public const int AUDIOCONT_None       = 0;
		public const int AUDIOCONT_Arabic     = 22;
		public const int AUDIOCONT_Bulgarian  = 23;
		public const int AUDIOCONT_Cantonese  = 18;
		public const int AUDIOCONT_Czech      = 24;
		public const int AUDIOCONT_Danish     = 15;
		public const int AUDIOCONT_Dutch      = 12;
		public const int AUDIOCONT_English    = 1;
		public const int AUDIOCONT_Farsi      = 31;
		public const int AUDIOCONT_Finnish    = 16;
		public const int AUDIOCONT_French     = 2;
		public const int AUDIOCONT_German     = 9;
		public const int AUDIOCONT_Greek      = 25;
		public const int AUDIOCONT_Hebrew     = 26;
		public const int AUDIOCONT_Hindi      = 20;
		public const int AUDIOCONT_Hungarian  = 27;
		public const int AUDIOCONT_Icelandic  = 30;
		public const int AUDIOCONT_Italian    = 10;
		public const int AUDIOCONT_Japanese   = 4;
		public const int AUDIOCONT_Korean     = 19;
		public const int AUDIOCONT_Mandarin   = 5;
		public const int AUDIOCONT_Norwegian  = 14;
		public const int AUDIOCONT_Polish     = 21;
		public const int AUDIOCONT_Portuguese = 17;
		public const int AUDIOCONT_Romanian   = 29;
		public const int AUDIOCONT_Russian    = 11;
		public const int AUDIOCONT_Spanish    = 3;
		public const int AUDIOCONT_Swedish    = 13;
		public const int AUDIOCONT_Tagalog    = 28;
		public const int AUDIOCONT_Turkish    = 32;
		public const int AUDIOCONT_Commentary = 6;
		public const int AUDIOCONT_MusicOnly  = 7;
		public const int AUDIOCONT_Other      = 8;

		public const int AUDIOFORMAT_None             = 0;
		public const int AUDIOFORMAT_DolbyDigital     = 3;
		public const int AUDIOFORMAT_DTS              = 5;
		public const int AUDIOFORMAT_DolbyDigitalEX   = 4;
		public const int AUDIOFORMAT_DTSES            = 6;
		public const int AUDIOFORMAT_DolbyDigitalPlus = 7;
		public const int AUDIOFORMAT_DolbyTrueHD      = 8;
		public const int AUDIOFORMAT_DTSHD_HR         = 9;
		public const int AUDIOFORMAT_DTSHD_MA         = 10;
		public const int AUDIOFORMAT_PCM              = 1;
		public const int AUDIOFORMAT_MP2              = 2;

		public const int AUDIOCHANNELS_None         = 0;
		public const int AUDIOCHANNELS_Mono         = 1;
		public const int AUDIOCHANNELS_2Channel     = 2;
		public const int AUDIOCHANNELS_Surround     = 3;
		public const int AUDIOCHANNELS_4_0          = 4;
		public const int AUDIOCHANNELS_4_1          = 5;
		public const int AUDIOCHANNELS_5_0          = 6;
		public const int AUDIOCHANNELS_5_1          = 7;
		public const int AUDIOCHANNELS_6_1_Matrixed = 8;
		public const int AUDIOCHANNELS_6_1_Discrete = 9;
		public const int AUDIOCHANNELS_7_1          = 10;
		public const int AUDIOCHANNELS_3_1          = 11;


		public const int CURRENCY_USD = 0;
		public const int CURRENCY_ARP = 1;
		public const int CURRENCY_AUD = 2;
		public const int CURRENCY_BRL = 3;
		public const int CURRENCY_CAD = 4;
		public const int CURRENCY_CLP = 5;
		public const int CURRENCY_CNY = 6;
		public const int CURRENCY_DKK = 7;
		public const int CURRENCY_EUR = 8;
		public const int CURRENCY_GBP = 9;
		public const int CURRENCY_HKD = 10;
		public const int CURRENCY_ISK = 11;
		public const int CURRENCY_INR = 12;
		public const int CURRENCY_IDR = 13;
		public const int CURRENCY_ILS = 14;
		public const int CURRENCY_JPY = 15;
		public const int CURRENCY_MXP = 16;
		public const int CURRENCY_NZD = 17;
		public const int CURRENCY_NOK = 18;
		public const int CURRENCY_PHP = 19;
		public const int CURRENCY_RUR = 20;
		public const int CURRENCY_SGD = 21;
		public const int CURRENCY_ZAR = 22;
		public const int CURRENCY_KRW = 23;
		public const int CURRENCY_SEK = 24;
		public const int CURRENCY_CHF = 25;
		public const int CURRENCY_TWD = 26;
		public const int CURRENCY_THB = 27;
		public const int CURRENCY_TRL = 28;

		public const int EXCLUSIONTYPE_MoviePick  = 1;
		public const int EXCLUSIONTYPE_Mobile     = 2;
		public const int EXCLUSIONTYPE_iPhone     = 3;
		public const int EXCLUSIONTYPE_Remote     = 4;
		public const int EXCLUSIONTYPE_DPOPublic  = 5;
		public const int EXCLUSIONTYPE_DPOPrivate = 6;

		public const int EVENTTYPE_Borrowed = 0;
		public const int EVENTTYPE_Returned = 1;
		public const int EVENTTYPE_Watched  = 2;

		// ICollectionFilter Constants
		// ConditionTypes
		// IDVD Data load sections
		public const int DATASEC_Cast       = 1;
		public const int DATASEC_Crew       = 2;
		public const int DATASEC_Studios    = 4;
		public const int DATASEC_Retailers  = 8;
		public const int DATASEC_Audio      = 16;
		public const int DATASEC_BoxSet     = 32;
		public const int DATASEC_EasterEggs = 64;
		public const int DATASEC_Genres     = 128;
		public const int DATASEC_Features   = 256;
		public const int DATASEC_Regions    = 512;
		public const int DATASEC_Formats    = 1024;
		public const int DATASEC_Events     = 2048;
		public const int DATASEC_Review     = 4096;
		public const int DATASEC_Tags       = 8192;
		public const int DATASEC_Discs      = 16384;

		// ICollectionFilter Constants
		// ConditionTypes
		public const int CONDITION_Equal          = 0;
		public const int CONDITION_NotEqual       = 1;
		public const int CONDITION_GreaterThan    = 2;
		public const int CONDITION_LessThan       = 3;
		public const int CONDITION_GreaterOrEqual = 4;
		public const int CONDITION_LessOrEqual    = 5;
		public const int CONDITION_Contain        = 6;
		public const int CONDITION_NotContain     = 7;
		// Fields
		public const int FIELD_CollectionType = 0;
		public const int FIELD_IsLoaned       = 1;
		public const int FIELD_LoanDue        = 2;
		public const int FIELD_Genres         = 3;
		public const int FIELD_EverWatched    = 4;
		public const int FIELD_LastWatched    = 5;
		public const int FIELD_Studios        = 6;
		public const int FIELD_BoxSet         = 7;
		// Sort orders
		public const int SORT_Displayed            = 0;
		public const int SORT_Random               = 1;
		public const int SORT_TitleAsc             = 2;
		public const int SORT_TitleDesc            = 3;
		public const int SORT_CollectionNumberAsc  = 4;
		public const int SORT_CollectionNumberDesc = 5;
		public const int SORT_ProductionYearAsc    = 6;
		public const int SORT_ProductionYearDesc   = 7;
		public const int SORT_DVDReleaseAsc        = 8;
		public const int SORT_DVDReleaseDesc       = 9;
		public const int SORT_PurchaseDateAsc      = 10;
		public const int SORT_PurchaseDateDesc     = 11;
		public const int SORT_LastEditedAsc        = 12;
		public const int SORT_LastEditedDesc       = 13;
		public const int SORT_WishPriorityAsc      = 14;
		public const int SORT_WishPriorityDesc     = 15;
		public const int SORT_FirstGenreAsc        = 16;
		public const int SORT_FirstGenreDesc       = 17;
		public const int SORT_ReviewAsc            = 18;
		public const int SORT_ReviewDesc           = 19;
		public const int SORT_RunTimeAsc           = 20;
		public const int SORT_RunTimeDesc          = 21;
		public const int SORT_RatingAsc            = 22;
		public const int SORT_RatingDesc           = 23;
		public const int SORT_DiscLocationAsc      = 24;
		public const int SORT_DiscLocationDesc     = 25;
		public const int SORT_UPCAsc               = 26;
		public const int SORT_UPCDesc              = 27;
		public const int SORT_LastWatchedAsc       = 28;
		public const int SORT_LastWatchedDesc      = 29;
		// Interface color IDs
		public const int COLOR_BackgroundSolid     = 0;
		public const int COLOR_PanelGradTop        = 1;
		public const int COLOR_PanelGradBottom     = 2;
		public const int COLOR_PanelSolid          = 3;
		public const int COLOR_PanelBevel          = 4;
		public const int COLOR_MessageGradTop      = 5;
		public const int COLOR_MessageGradBottom   = 6;
		public const int COLOR_Highlight           = 7;
		public const int COLOR_HighlightText       = 8;
		public const int COLOR_ListLinesOdd        = 9;
		public const int COLOR_ListLinesEven       = 10;
		public const int COLOR_TabGradientTop      = 11;
		public const int COLOR_TabGradientBottom   = 12;
		public const int COLOR_TabBevel            = 13;

		public const int SHORTCUT_MOD_Shift = 0x2000;
		public const int SHORTCUT_MOD_Ctrl  = 0x4000;
		public const int SHORTCUT_MOD_Alt   = 0x8000;
		public const int SHORTCUT_MOD_None  = 0;

		public const int SHORTCUT_KEY_F1 = 112;
		public const int SHORTCUT_KEY_F2 = 113;
		public const int SHORTCUT_KEY_F3 = 114;
		public const int SHORTCUT_KEY_F4 = 115;
		public const int SHORTCUT_KEY_F5 = 116;
		public const int SHORTCUT_KEY_F6 = 117;
		public const int SHORTCUT_KEY_F7 = 118;
		public const int SHORTCUT_KEY_F8 = 119;
		public const int SHORTCUT_KEY_F9 = 120;
		public const int SHORTCUT_KEY_F10 = 121;
		public const int SHORTCUT_KEY_F11 = 122;
		public const int SHORTCUT_KEY_F12 = 123;

		public const int SHORTCUT_KEY_A = 65;
		// Additional shortCut Keys use ASCII values

		// Used for GetConstantByDescription and GetDescriptionByConstant
		public const int CONSTANT_TYPE_SUBTITLE      = 0;
		public const int CONSTANT_TYPE_AUDIOCONT     = 1;
		public const int CONSTANT_TYPE_AUDIOFORMAT   = 2;
		public const int CONSTANT_TYPE_AUDIOCHANNELS = 3;
		public const int CONSTANT_TYPE_GENRE         = 4;
		public const int CONSTANT_TYPE_CREDIT        = 5;
		public const int CONSTANT_TYPE_CREDITSUB     = 6;
		public const int CONSTANT_TYPE_CASETYPE      = 7;
		public const int CONSTANT_TYPE_CURRENCY      = 8;
		public const int CONSTANT_TYPE_LOCALITY      = 9;

	}
	
	class CategoryRegistrar
	{
		[ComImport(), Guid("0002E012-0000-0000-C000-000000000046"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			internal interface ICatRegister
		{
			void RegisterCategories(
				int cCategories,
				IntPtr rgCategoryInfo);

			void UnRegisterCategories(
				int cCategories,
				IntPtr rgcatid);

			void RegisterClassImplCategories(
				[In()] ref Guid rclsid,
				int cCategories,
				[In(), MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

			void UnRegisterClassImplCategories(
				[In()] ref Guid rclsid,
				int cCategories,
				[In(), MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

			void RegisterClassReqCategories(
				[In()] ref Guid rclsid,
				int cCategories,
				[In(), MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

			void UnRegisterClassReqCategories(
				[In()] ref Guid rclsid,
				int cCategories,
				[In(), MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);
		}
	}

	[Guid("8599D499-8C7E-431A-83BD-17A89825931F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDVDInfo
	{
		// Internal use
		string GetID();
		string GetCollectionNumber();
		void SetCollectionNumber(string Value);
		string GetTitle();
		void SetTitle(string Value);
		string GetSortTitle();
		void SetSortTitle(string Value);
		string GetFormattedProfileID();
		string GetFormattedRating();
		int GetProductionYear();
		void SetProductionYear(int Value);
		DateTime GetDVDReleaseDate();
		void SetDVDReleaseDate(DateTime Value);
		DateTime GetPurchaseDate();
		void SetPurchaseDate(DateTime Value);
		int GetRuntime();
		void SetRuntime(int Value);
		string GetFormattedGenres();
		bool GetFormatWidescreen();
		void SetFormatWidescreen(bool Value);
		bool GetFormatAnamorphic();
		void SetFormatAnamorphic(bool Value);
		bool GetFormatPanScan();
		void SetFormatPanScan(bool Value);
		bool GetFormatFullFrame();
		void SetFormatFullFrame(bool Value);
		string GetAspectRatio();
		void SetAspectRatio(string Value);
		string GetProfileID();
		void SetProfileID(string Value);
		int GetCaseID();
		void SetCaseID(int Value);
		int GetVideoStandard();
		void SetVideoStandard(int Value);
		string GetOverview();
		void SetOverview(string Value);
		string GetEasterEggs();
		void SetEasterEggs(string Value);
		string GetNotes();
		void SetNotes(string Value);
		string GetEdition();
		void SetEdition(string Value);
		string GetOriginalTitle();
		void SetOriginalTitle(string Value);
		int GetWishPriority();
		void SetWishPriority(int Value);
		bool GetFeatureByID(int FeatureID);
		void SetFeatureByID(int FeatureID, bool Value);
		bool GetLockByID(int LockID);
		void SetLockByID(int LockID, bool Value);
		DateTime GetLastEdited();
		void SetLastEdited(DateTime Value);
		DateTime GetLoanDue();
		void SetLoanDue(DateTime Value);
		int GetCountryOfOrigin();
		void SetCountryOfOrigin(int Value);
		string GetPurchasePlace();
		void SetPurchasePlace(string Value);
		string GetOtherFeatures();
		void SetOtherFeatures(string Value);
		int GetCollectionType();
		void SetCollectionType(int Value);
		bool GetRegionByID(int RegionID);
		void SetRegionByID(int RegionID, bool Value);
		string GetStudio(int StudioIndex);
		void SetStudio(int StudioIndex, string Value);
		int GetGenre(int GenreIndex);
		void SetGenre(int GenreIndex, int Value);
		// SRP Value is passed as a fixed-point data type with four implicit decimal places: e.g. 29.95 is passed as 299500.
		long GetSRPValue();
		void SetSRPValue(long Value);
		int GetSRPCurrency();
		void SetSRPCurrency(int Value);
		// Purchase Price Value is passed as a fixed-point data type with four implicit decimal places: e.g. 29.95 is passed as 299500.
		long GetPurchasePriceValue();
		void SetPurchasePriceValue(long Value);
		int GetPurchasePriceCurrency();
		void SetPurchasePriceCurrency(int Value);
		bool PurchasePriceIsEmpty();
		void ClearPurchasePrice();
		void GetReview(out int Film, out int Video, out int Audio, out int Extras);
		void SetReview(int Film, int Video, int Audio, int Extras);
		// Cast
		int GetCastCount();
		void ClearCast();
		void GetCastByIndex(int CastIndex, out string FirstName, out string MiddleName,
		  out string LastName, out int BirthYear, out string Part, out string CreditedAs,
		  out bool Voice, out bool Uncredited);
		void SetCastByIndex(int CastIndex, string FirstName, string MiddleName,
			string LastName, int BirthYear, string Part, string CreditedAs,
			bool Voice, bool Uncredited);
		void AddCast(string FirstName, string MiddleName,
			string LastName, int BirthYear, string Part, string CreditedAs,
			bool Voice, bool Uncredited);
		// Crew
		int GetCrewCount();
		void ClearCrew();
		void GetCrewByIndex(int CrewIndex, out string FirstName, out string MiddleName,
			out string LastName, out int BirthYear, out int CreditType, out int CreditSubtype, 
			out string CreditedAs);
		void SetCrewByIndex(int CrewIndex, string FirstName, string MiddleName,
			string LastName, int BirthYear, int CreditType, int CreditSubtype, string CreditedAs);
		void AddCrew(string FirstName, string MiddleName,
			string LastName, int BirthYear, int CreditType, int CreditSubtype, string CreditedAs);
		void GetMediaTypes(out bool DVD, out bool HDDVD, out bool BluRay);
		void SetMediaTypes(bool DVD, bool HDDVD, bool BluRay);
		void GetCustomMediaType(out string MediaTypeName);
		void SetCustomMediaType(string MediaTypeName);
		// Box set
		int GetBoxSetContentCount();
		void ClearBoxSetContents();
		string GetBoxSetContentByIndex(int BoxIndex);
		void SetBoxSetContentByIndex(int BoxIndex, string Value);
		void AddBoxSetContent(string Value);
		// Subtitles
		int GetSubtitleCount();
		void ClearSubtitles();
		int GetSubtitleByIndex(int SubIndex);
		void SetSubtitleByIndex(int SubIndex, int Value);
		void AddSubtitle(int Value);
		// Audio Tracks
		void GetAudioTrack(int TrackIndex, out int ContentID, out int FormatID, out int ChannelsID);
		void SetAudioTrack(int TrackIndex, int ContentID, int FormatID, int ChannelsID);
		// Discs
		int GetDiscCount();
		void ClearDiscs();
		void GetDiscByIndex(int DiscIndex, out string DescriptionSideA, out string DescriptionSideB,
			out string LabelSideA, out string LabelSideB, out string DiscIDSideA, out string DiscIDSideB,
			out bool DualLayeredSideA, out bool DualLayeredSideB, out string Location, out string Slot);
		void SetDiscByIndex(int DiscIndex, string DescriptionSideA, string DescriptionSideB,
			string LabelSideA, string LabelSideB, string DiscIDSideA, string DiscIDSideB,
			bool DualLayeredSideA, bool DualLayeredSideB, string Location, string Slot);
		void AddDisc(string DescriptionSideA, string DescriptionSideB,
			string LabelSideA, string LabelSideB, string DiscIDSideA, string DiscIDSideB,
			bool DualLayeredSideA, bool DualLayeredSideB, string Location, string Slot);
		// Tags
		int GetTagCount();
		void ClearTags();
		string GetTagByIndex(int TagIndex);
		void SetTagByIndex(int TagIndex, string Value);
		void AddTag(string Value);
		// Events (History)
		int GetEventCount();
		void ClearEvents();
		void GetEventByIndex(int EventIndex, out string UserFirstName, out string UserLastName,
			out int EventType, out DateTime TimeStamp, out string Note);
		void SetEventByIndex(int EventIndex, string UserFirstName, string UserLastName,
			int EventType, DateTime TimeStamp, string Note);
		void AddEvent(string UserFirstName, string UserLastName,
			int EventType, DateTime TimeStamp, string Note);
		// Other functions
		string GetCoverImageFilename(bool Front, bool Thumbnail);
		string GetXML(bool IncludePersonalInfo);
        bool GetChangesMadeIndicator();
		void SetChangesMadeIndicator(bool Value);
		bool GetPurchasePlaceIsOnline();
		void SetPurchasePlaceIsOnline(bool Value);
		string GetPurchasePlaceWebsite();
		void SetPurchasePlaceWebsite(string Value);
		bool GetPurchasePriceIsEmpty();
		void SetProfileTimestamp(DateTime Value);
		DateTime GetProfileTimestamp();
		string GetParentProfileID();
		void GetCastDividerByIndex(int CastIndex, out string Caption, out int DividerType);
		void SetCastDividerByIndex(int CastIndex, out int DividerType);
		void AddCastDivider(string Caption, int DividerType);
		void GetCrewDividerByIndex(int CrewIndex, out string Caption, out int DividerType, out int CreditType);
		void SetCrewDividerByIndex(int CrewIndex, string Caption, int DividerType, int CreditType);
		void AddCrewDivider(string Caption, int DividerType, int CreditType);
		string GetMediaCompany(int MediaCompanyIndex);
		void SetMediaCompany(int MediaCompanyIndex, string Value);
		int GetCountAs();
		void SetCountAs(int Value);
		void GetRating(out int RatingSystem, out int RatingAge, out int RatingVariant);
		void SetRating(int RatingSystem, int RatingAge, int RatingVariant);
		string GetRatingDescription();
		void SetRatingDescription(string Value);
		void GetBDRegions(out bool RegA, out bool RegB, out bool RegC);
		bool GetExclusion(int ExclusionType);
		void SetExclusion(int ExclusionType, bool ExclusionOn);
	}
	
	[Guid("8AFA0FCD-91B4-47F5-8663-CF72CA05B360"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDVDProfilerAPI
	{
		void SelectDVDByProfileID(string ProfileID);
		object GetFlaggedProfileIDs();
		void FlagDVDByProfileID(string ProfileID, bool FlagOn);
		void  FlagAllDVDs(bool FlagOn);
		void FlagAllDisplayedDVDs(bool FlagOn);
		bool DVDIsFlaggedByProfileID(string ProfileID);
		void RegisterForEvent(int EventType);
		string RegisterMenuItem(int FormID, int MenuID, string MenuPath, string Caption, int EventID);
		void SetMenuGlyph(string MenuToken, object LargeImage, object SmallImage, string HintTitle, string HintContent);
		void AssignMenuItemToToolbar(string MenuToken, string ToolbarCaption, bool CreateAsNeeded);
		void RemoveMenuItemFromToolbar(string MenuToken, string ToolbarCaption);
		void UnregisterMenuItem(string MenuToken);
		bool GetRegisteredMenuItemChecked(string MenuToken); 
		void SetRegisteredMenuItemChecked(string MenuToken, bool Checked);
		int GetSortIndexByProfileID(string ProfileID);
		IDVDInfo GetDisplayedDVD();
		// Pass -1 for DataSections for all data; Pass -1 for MaxCast for all cast entries
		// Odd paramater convention due to conflicts with backwards compatibility 
		void DVDByProfileID(out IDVDInfo DVD, string ProfileID, int DataSections, int MaxCast);
		IDVDInfo CreateDVD();
		ICollectionFilter GetCollectionFilter();
		object GetFilteredProfileIDs(ICollectionFilter CollectionFilter);
		object GetDisplayedProfileIDs(int SortOrder);
		object GetAllProfileIDs();
		void SetDisplayedProfileIDs(object ProfileIDList);
		IDVDProfilerPluginHostWindow CreateHostWindow(string PanelCaption);
		
		// Parental control
		bool ParentalControlIsEnabled();
		bool ParentalControlHideInCollection();
		bool ParentalControlHideInOnlineListing();

		// Program Actions
		void FocusMainForm();
		void SetMainFormVisible(bool Visible);
		void ClearAllFilters();
		void ReloadCurrentDVD();
		void RequeryDatabase();
		void UnloadMe();
		void PerformPendingDownloads();
		void ClearPendingDownloads();
		bool DisplayDVDPersonalize(IDVDInfo DVD);
		bool DisplayDVDEdit(IDVDInfo DVD, bool IncludeCoverScanPage, bool IncludePersonalizePage);
		void DisplayCoverScanEdit(string ProfileID);
		void RemoveDVDFromCollection(string ProfileID, bool RemoveCoverScans);
		void SaveDVDToCollection(IDVDInfo DVD);
		void UpdateProfileInListDisplay(string ProfileID);
		
		// Interface queries
		int GetInterfaceColor(int ColorID);

		// Call an action for the specific FormID, which must be loaded
		// ActionID is one of public const int ACTIONID_FormID_XXX
		// ActionData A, B, C and the return value vary by the ActionID
		// Returns public const int ACTIONResult_Unknown for an unknown FormID/ActionID combination
		// Returns public const int ACTIONResult_FormNotLoaded if the form isn't loaded
		object ExecuteAction(int FormID, int ActionID, object ActionDataA, object ActionDataB, object ActionDataC);

		string GetDVDProfilerVersion();
		int GetDVDProfilerBuildNumber();
		void SaveDVDToCollectionPartial(IDVDInfo DVD, int DataSections);
		void LoadFlagSet(string PathFileName);
		void SaveFlagSet(string PathFileName);
		void LoadFilterSetFromFile(string PathFileName, bool LoadTypeAndSort);
		void LoadFilterSetFromString(string FilterString, bool LoadTypeAndSort);
		void SaveFilterSetToFile(string PathFileName, bool SaveTypeAndSort);
		string GetFilterSetAsString(bool SaveTypeAndSort);
		void ShowNotification(string MessageText, string TitleText);
		string GetDefaultOpticalDrive();
		string GetDiscIDFromDrive(string RootPath, out bool IsBluRay, out bool IsDualLayered);
		void SetRegisteredMenuItemEnabled(string MenuToken, bool Enabled);
		string RegisterMenuItemA(int FormID, int MenuID, string MenuPath, string Caption, int EventID,
			string AfterMenuItem, int ShortCutKey, int ShortCutModifier, bool BeginGroup);
		void TriggerMenuItem(int FormID, int MenuID, string MenuPath, string MenuItem);
		string GetDescriptionByConstant(int ConstantType, int ConstantValue, int ConstantSubValue);
		int GetConstantByDescription(int ConstantType, string Description);
		string TranslateString(string InStr, string Key);
		void GetCustomCollectionCategoryInfo(int CollectionType, out string Name, out bool IncludeInOwned,
			out bool TrackCollectionNums, out bool TrackPurchaseInfo);
	}

    [Guid("6598EA88-8FAD-4875-A2F0-39BA225B8233"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDVDProfilerPluginHostWindow
	{
		int GetWindowHandle();
		void CloseWindow();
		void ShowWindow();
		void HideWindow();
		void SetCaption(string Caption);
		void SetInitialSize(int Width, int Height);
	}

	[Guid("2E7E8A3D-FAB8-4DFD-955A-D9B1F607A58C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ICollectionFilter
	{
		void SetJoinWithOr(bool Value);
		bool GetJoinWithOr();
		void SetSortOrder(int Value);
		int GetSortOrder();
		void SetIgnoreParentalControl(bool Value);
		bool GetIgnoreParentalControl();
		
		// Other functions
		void Clear();
		void AddCondition(int ConditionField, int ConditionType, object ConditionValue);
		// Internal use
		string GetID();
	}

	[Guid("38D6312C-4B22-4620-A04F-2CC80C873118"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDVDProfilerPluginInfo
	{
		string GetName();
		string GetDescription();
		string GetAuthorName();
		string GetAuthorWebsite();
		int GetPluginAPIVersion();
		int GetVersionMajor();
		int GetVersionMinor();
	}

	[Guid("356390A8-A862-43CF-81BF-5ACC8F1F4D82"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDVDProfilerPlugin
	{
		void Load(IDVDProfilerAPI DVDProAPI);
		void Unload();
		void HandleEvent(int EventType, object EventData);
	}
}
