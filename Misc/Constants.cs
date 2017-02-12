
using System;
using System.IO;

namespace Ykse.Partner
{
	public class Constants
	{
		public const string _ = "_";
		public const string HexCode = "0123456789ABCDEF";
		public const string UpperAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string LowerAlphabet = "abcdefghijklmnopqrstuvwxyz";
		public const string Default = "Default";
		public const string Empty = "Empty";
		public const string All = "ALL";
		public const string Root = "Root";
		public const string Password = "Password";
		public const int BufferSize = 1024;
		public const int BigBufferSize = 102400;
		public const uint DaVinci = 3284624;

		public static string[] SkipAssemblyPrefixes = new[] {
			"System",
			"Microsoft",
			"mscorlib",
			"Newtonsoft",
			"Oracle",
			"MySql",
			"AntiXssLibrary",
			"HtmlSanitizationLibrary",
			"log4net",
			"hadoop",
			"Antlr",
			"SimpleInjector",
			"WebActivatorEx",
			"WebGrease",
			"FluentScheduler",
			"xunit",
			"MockJockey",
			"FluentAssertions",
		};

		public class EventLog
		{
			public const string Source = "Yuanta";
		}

		public class Platform
		{
			public const string x64 = "x64";
			public const string x86 = "x86";
		}

		public class Date
		{
			public static readonly DateTime NullTW =
				new DateTime(2910, 12, 31);
			public static readonly DateTime Max = DateTime.MaxValue;
			public static readonly DateTime Min = DateTime.MinValue;
			public static readonly DateTime MinMySql = new DateTime(1000, 1, 1);
			public static readonly DateTime MinSql = new DateTime(1753, 1, 1);
		}

		public class Xml
		{
			public const string Namespace = "http://yuanta.com/";
		}

		public class DbProvider
		{
			public static string[] Names = new[] {
				OracleClient,
				OracleDataAccess,
			};

			public const string Odbc = "System.Data.Odbc";
			public const string OleDb = "System.Data.OleDb";
			public const string OracleClient = "System.Data.OracleClient";
			public const string SqlClient = "System.Data.SqlClient";
			public const string OracleDataAccess = "Oracle.DataAccess.Client";
			public const string MySqlClient = "MySql.Data.MySqlClient";
			public const string SQLite = "System.Data.SQLite";
		}


		public class Builtin
		{
			public const string Log = "Log";
			public const string Data = "Data";
		}

		public class Encoding
		{
			public static System.Text.Encoding Big5 =
				System.Text.Encoding.GetEncoding(950);
			public static System.Text.Encoding UTF8 =
				System.Text.Encoding.UTF8;
			public static System.Text.Encoding Utf8NoBom =
				new System.Text.UTF8Encoding(false, true);
			public static System.Text.Encoding ASCII =
				System.Text.Encoding.ASCII;
			public static System.Text.Encoding Default =
				System.Text.Encoding.Default;
		}

		public class Encode
		{
			public const string MD5 = "MD5";
			public const string SHA512 = "SHA512";
		}

		public class Unicode
		{
			public const string Prefix = "&#x";
			public const string Suffix = ";";
		}


		public class Pattern
		{
			public const string Email = @"^\w+([-+.']\w*)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
			public const string EmailSeaperateByComma = @"^(\w+([-+.']\w*)*@\w+([-.]\w+)*\.\w+([-.]\w+)*,\s*)*\w+([-+.']\w*)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

			public const string Ip = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

			public const string Alphabet = @"^[a-zA-Z]*$";
			public const string Numeric = @"^-?\d*(\.\d+)?$";
			public const string AlphaNumeric = @"^[a-zA-Z0-9]*$";
			public const string Uri = @"^(?:http|https|ftp)://[a-zA-Z0-9\.\-]+(?:\:\d{1,5})?(?:[A-Za-z0-9\.\;\:\@\&\=\+\$\,\?/]|%u[0-9A-Fa-f]{4}|%[0-9A-Fa-f]{2})*$";
			public const string Html = @"<\/?[^>]+>";
			public const string HtmlBr = @"<(br|BR)\s*\/*>";
			public const string HtmlColor = @"^#(?:[0-9a-fA-F]{3}){1,2}$";

			public const string Uuid = @"^[A-F0-9]{32}$";
		}


		public class Json
		{
			public class Null
			{
				public const string Array =
					Symbol.OpenBracket + Symbol.CloseBracket;
				public const string Object =
					Symbol.OpenBrace + Symbol.CloseBrace;
			}
		}


		public class JavaScript
		{
			public const string Undefined = "undefined";
		}

		public class UriScheme
		{
			public static readonly string Delimiter = Uri.SchemeDelimiter;
			public static readonly string File = Uri.UriSchemeFile;
			public static readonly string Ftp = Uri.UriSchemeFtp;
			public static readonly string Http = Uri.UriSchemeHttp;
			public static readonly string Https = Uri.UriSchemeHttps;
			public static readonly string Mailto = Uri.UriSchemeMailto;
			public static readonly string Tcp = "tcp";
		}

		public class Protocol
		{
			public const string Ldap = "LDAP://";
			public const string WinNt = "WinNT://";
			public static readonly string Http =
				Uri.UriSchemeHttp + "://";
			public static readonly string Https =
				Uri.UriSchemeHttps + "://";
			public static readonly string Ftp =
				Uri.UriSchemeFtp + "://";
			public static readonly string File =
				Uri.UriSchemeFile + ":///";
		}

		public class HttpMethod
		{
			public const string Get = "GET";
			public const string Head = "HEAD";
			public const string Post = "POST";
			public const string Put = "PUT";
			public const string Delete = "DELETE";
			public const string Trace = "TRACE";
			public const string Options = "OPTIONS";
		}

		public class Directory
		{
			public static readonly string Separator =
				Path.DirectorySeparatorChar.ToString();
		}

		public class File
		{
			public class Extension
			{
				public const string LOG = ".log";
				public const string XML = ".xml";
				public const string DLL = ".dll";
				public const string CONFIG = ".config";
				public const string DOC = ".doc";
				public const string EXE = ".exe";
				public const string PDF = ".pdf";
				public const string AI = ".ai";
				public const string EPS = ".eps";
				public const string PS = ".ps";
				public const string XLS = ".xls";
				public const string PPT = ".ppt";
				public const string JS = ".js";
				public const string JSON = ".json";
				public const string SWF = ".swf";
				public const string XHTML = ".xhtml";
				public const string XHT = ".xht";
				public const string ZIP = ".zip";
				public const string MID = ".mid";
				public const string MIDI = ".midi";
				public const string MPGA = ".mpga";
				public const string MP2 = ".mp2";
				public const string MP3 = ".mp3";
				public const string M3U = ".m3u";
				public const string RAM = ".ram";
				public const string RM = ".rm";
				public const string RA = ".ra";
				public const string RDL = ".rdl";
				public const string RDLC = ".rdlc";
				public const string WAV = ".wav";
				public const string BMP = ".bmp";
				public const string GIF = ".gif";
				public const string JPEG = ".jpeg";
				public const string JPG = ".jpg";
				public const string JPE = ".jpe";
				public const string PNG = ".png";
				public const string TIFF = ".tiff";
				public const string TIF = ".tif";
				public const string CSS = ".css";
				public const string HTML = ".html";
				public const string HTM = ".htm";
				public const string TXT = ".txt";
				public const string RTX = ".rtx";
				public const string WML = ".wml";
				public const string WMLS = ".wmls";
				public const string XSL = ".xsl";
				public const string MPEG = ".mpeg";
				public const string MPG = ".mpg";
				public const string MPE = ".mpe";
				public const string QT = ".qt";
				public const string MOV = ".mov";
				public const string AVI = ".avi";
				public const string ASP = ".asp";
				public const string ASPX = ".aspx";
				public const string ASHX = ".ashx";
				public const string ASMX = ".asmx";
				public const string SVC = ".svc";
				public const string VSHOST = ".vshost.exe";
				public const string CK = ".ck";
			}
		}


		public class Interval
		{
			public const int Minute = 60 * 1;
			public const int Hour = 60 * 60;
			public const int Day = 60 * 60 * 24;
			public const int Week = 60 * 60 * 24 * 7;
			public const int Month = 60 * 60 * 24 * 30;
			public const int Year = 60 * 60 * 24 * 365;
			public const int Forever = -1;
		}

		public class Html
		{
			public const string br = "<br />";

			public class Entity
			{
				public const string nbsp = " ";
				public const string amp = "&";
				public const string gt = ">";
				public const string lt = "<";
			}

			public class Escape
			{
				public const string nbsp = "&nbsp;";
				public const string amp = "&amp;";
				public const string gt = "&gt;";
				public const string lt = "&lt;";
			}
			public class Enctype
			{
				public static string MultiPart =
					"multipart/form-data";
			}
		}

		public class Ip
		{
			public const string LoopBack = "127.0.0.1";
			public const string LoopBackIPv6 = "::1";
			public const long LoopBackAsLong = 2130706433;

			public const string Max = "255.255.255.255";
			public const string Min = "0.0.0.0";

			public const long MaxAsLong = 4294967295;
			public const long MinAsLong = 0;

			public class Private
			{
				public class ClassA
				{
					public const string Start = "10.0.0.0";
					public const string End = "10.255.255.255";
					public const long StartAsLong = 167772160;
					public const long EndAsLong = 184549375;
				}

				public class ClassB
				{
					public const string Start = "172.16.0.0";
					public const string End = "172.31.255.255";
					public const long StartAsLong = 2886729728;
					public const long EndAsLong = 2887778303;
				}

				public class ClassC
				{
					public const string Start = "192.168.0.0";
					public const string End = "192.168.255.255";
					public const long StartAsLong = 3232235520;
					public const long EndAsLong = 3232301055;
				}

				public class Loopback
				{
					public const string Start = "127.0.0.0";
					public const string End = "127.255.255.255";
					public const long StartAsLong = 2130706432;
					public const long EndAsLong = 2147483647;
				}
			}
		}

		public class Folder
		{
			public const string Bin = "bin";
			public const string Log = "log";
			public const string Config = "config";
		}

		public class ContentType
		{
			public const string FORM =
				"application/x-www-form-urlencoded";
			public const string MULTIPART =
				"multipart/form-data";
			public const string DOC = "application/msword";
			public const string EXE = "application/octet-stream";
			public const string DLL = "application/octet-stream";
			public const string PDF = "application/pdf";
			public const string AI = "application/postscript";
			public const string EPS = "application/postscript";
			public const string PS = "application/postscript";
			public const string XLS = "application/vnd.ms-excel";
			public const string PPT = "application/vnd.ms-powerpoint";
			public const string JS = "application/x-javascript";
			public const string SWF = "application/x-shockwave-flash";
			public const string XHTML = "application/xhtml+xml";
			public const string XHT = "application/xhtml+xml";
			public const string ZIP = "application/zip";
			public const string MID = "audio/midi";
			public const string MIDI = "audio/midi";
			public const string MPGA = "audio/mpeg";
			public const string MP2 = "audio/mpeg";
			public const string MP3 = "audio/mpeg";
			public const string M3U = "audio/x-mpegurl";
			public const string RAM = "audio/x-pn-realaudio";
			public const string RM = "audio/x-pn-realaudio";
			public const string RA = "audio/x-realaudio";
			public const string WAV = "audio/x-wav";
			public const string BMP = "image/bmp";
			public const string GIF = "image/gif";
			public const string JPEG = "image/jpeg";
			public const string JPG = "image/jpeg";
			public const string JPE = "image/jpeg";
			public const string PNG = "image/png";
			public const string TIFF = "image/tiff";
			public const string TIF = "image/tiff";
			public const string CSS = "text/css";
			public const string HTML = "text/html";
			public const string HTM = "text/html";
			public const string TXT = "text/plain";
			public const string RTX = "text/richtext";
			public const string WML = "text/vnd.wap.wml";
			public const string WMLS = "text/vnd.wap.wmlscript";
			public const string XSL = "text/xml";
			public const string XML = "text/xml";
			public const string MPEG = "video/mpeg";
			public const string MPG = "video/mpeg";
			public const string MPE = "video/mpeg";
			public const string QT = "video/quicktime";
			public const string MOV = "video/quicktime";
			public const string AVI = "video/x-msvideo";
			public const string JSON = "application/json";
		}

		public class Boolean
		{
			public const string _1 = "1";
			public const string _0 = "0";
			public const string Y = "Y";
			public const string N = "N";
			public const string Yes = "Yes";
			public const string No = "No";
			public const string T = "T";
			public const string F = "F";
			public const string True = "True";
			public const string False = "False";
			public const string On = "On";
			public const string Off = "Off";
		}

		public class WebServerName
		{
			public const string VisualStudioDefaultWebServer = "Request";
			public const string VisualStudio2012 = "IIS7WorkerRequest";
			public const string IIS6 = "ISAPIWorkerRequestInProcForIIS6";
			public const string IIS7 = "ISAPIWorkerRequestInProcForIIS7";
		}

		public class Socket
		{
			public const string EOF = "<EOF>";
		}

		public class HexString
		{
			public const string r = "0x0D";
			public const string n = "0x0A";
			public const string rn = "0x0D0A";
		}

		public class Token
		{
			public const string AppID = "Token-AppID";
			public const string UserID = "Token-UserID";
			public const string ScopeID = "Token-ScopeID";
		}

		public class Metric
		{
			public const string _10p15 = "Peta";
			public const string _10p12 = "Tera";
			public const string _10p9 = "Giga";
			public const string _10p6 = "Mega";
			public const string _10p3 = "Kilo";
			public const string _10p_3 = "Milli";
			public const string _10p_6 = "Micro";
			public const string _10p_9 = "Nano";
			public const string _10p_12 = "Pico";
			public const string _10p_15 = "Femto";

			public class Symbol
			{
				public const string _10p15 = "P";
				public const string _10p12 = "T";
				public const string _10p9 = "G";
				public const string _10p6 = "M";
				public const string _10p3 = "K";
				public const string _10p_3 = "m";
				public const string _10p_6 = "μ";
				public const string _10p_9 = "n";
				public const string _10p_12 = "p";
				public const string _10p_15 = "f";
			}
		}
	}
}
