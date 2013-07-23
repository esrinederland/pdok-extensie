using System;
using Microsoft.Win32;

namespace pdok4arcgis
{
    public enum SpecialFolder
    {
        Application,
        ConfigurationFiles,
        Data,
        ExecutingAssembly,
        Help,
        Temp,
        TransformationFiles,
        LogFiles
    }

    public class Utils
    {
        private static String _logFile = "";

        #region "File/Folder Path"
        /// <summary>
        /// Get folder path for a system environment special folder
        /// </summary>
        /// <param name="folderName">enumeration of the system special folder</param>
        /// <returns>full path to the special folder</returns>
        public static string GetSpecialFolderPath(System.Environment.SpecialFolder folder)
        {
            return System.Environment.GetFolderPath(folder);
        }

        public static void setLogFile(String fileName)
        {
            _logFile = fileName;
        }

        /// <summary>
        /// Get folder path for CSW dll related folder, such as the data folder, transformation file folder, etc.
        /// </summary>
        /// <param name="folderName">enumeration of the CSW special folder</param>
        /// <returns>full path to the special folder</returns>
        public static string GetSpecialFolderPath(pdok4arcgis.SpecialFolder folder)
        {
            string folderPath = "";
            string configFileDir = null;
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\PDOK\\Applications\\PDOK4ArcGIS");
            if (regKey != null) configFileDir = (String)regKey.GetValue("ConfigFileDir");
            if (configFileDir == null) configFileDir = ExecutingAssemblyPath();

            switch (folder)
            {
                case SpecialFolder.ConfigurationFiles:

                    try
                    {
                        System.IO.StreamReader fStream = System.IO.File.OpenText(configFileDir + "\\CswConfigPath.properties");
                        folderPath = fStream.ReadLine();
                    }
                    catch (Exception e)
                    {
                        // throw e;
                    }
                    if (string.IsNullOrEmpty(folderPath)) folderPath = System.IO.Path.Combine(ExecutingAssemblyPath(), "Data");
                    //folderPath = GetSpecialFolderPath(Environment.SpecialFolder.ProgramFiles);
                    //folderPath = System.IO.Path.Combine(folderPath, "ESRI\\Portal\\CswClients\\Data");
                    break;
                case SpecialFolder.LogFiles:

                    try
                    {
                        System.IO.StreamReader fStream = System.IO.File.OpenText(configFileDir + "\\CswConfigPath.properties");
                        fStream.ReadLine();
                        if (fStream != null)
                        {
                            String debugFlag = fStream.ReadLine();
                            if (debugFlag != null && debugFlag.Trim().Equals("debug=on", StringComparison.CurrentCultureIgnoreCase))
                            {
                                folderPath = (String)regKey.GetValue("LogDir");
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        // throw e;
                    }
                    break;
                case SpecialFolder.TransformationFiles:
                    // "Documents And Settings\<user>\Application Data\ESRI\PortalFindServices"
                    folderPath = GetSpecialFolderPath(Environment.SpecialFolder.ApplicationData);
                    folderPath = System.IO.Path.Combine(folderPath, "ESRI");
                    break;
                case SpecialFolder.Data:
                    folderPath = System.IO.Path.Combine(ExecutingAssemblyPath(), "Data");
                    break;
                case SpecialFolder.Help:
                    folderPath = System.IO.Path.Combine(ExecutingAssemblyPath(), "Help");
                    break;
                case SpecialFolder.ExecutingAssembly:
                    folderPath = ExecutingAssemblyPath();
                    break;
                case SpecialFolder.Application:
                    folderPath = System.Windows.Forms.Application.ExecutablePath;
                    break;
                case SpecialFolder.Temp:
                    // Returns the path of the current system's temporary folder
                    folderPath = System.IO.Path.GetTempPath();
                    break;
            }

            return folderPath;
        }

        public static void writeFile(String logMessage)
        {

            System.IO.FileStream fileStream = null;
            System.IO.StreamWriter sr = null;
            try
            {
                DateTime dt = DateTime.Now;
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(_logFile);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Open(System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    sr = new System.IO.StreamWriter(fileStream);
                }
                else
                {
                    sr = fileInfo.AppendText();
                }


                sr.Write(logMessage);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (fileStream != null)
                    fileStream.Close();

            }
        }

        private static string ExecutingAssemblyPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        #endregion

        #region "URL"
        public static string EnsureTrailingQuestionOrAmpersandInURL(string url)
        {
            if (url == null) return null;

            // clean up
            string newUrl = url.Trim();
            if (newUrl.Length == 0) return newUrl;
            if (newUrl.EndsWith("/")) { newUrl = newUrl.Substring(0, newUrl.Length - 1); }

            // check if has "?" in the url
            if (!newUrl.Contains("?"))
            {
                newUrl += "?";
            }
            else
            {
                switch (newUrl.Substring(newUrl.Length - 1, 1))
                {
                    case "?":
                    case "&":
                        break;

                    default:
                        newUrl += "&";
                        break;
                }
            }

            return newUrl;
        }
        #endregion

        #region "GUI GetNextTabStopControl"
        /// <summary>
        /// Get next visible tabstop control. Used for tab key handling.
        /// </summary>
        /// <param name="form">the form that contains those controls</param>
        /// <returns>a control; or null if not found</returns>
        public static System.Windows.Forms.Control GetNextTabStopControl(System.Windows.Forms.Form form)
        {
            System.Windows.Forms.Control ctrl = form.GetNextControl(form.ActiveControl, true);
            bool found = false, exhausted = false, marked = false;
            while (!found && !exhausted)
            {
                if (ctrl != null)
                {
                    if (ctrl.TabStop && ctrl.CanFocus) { found = true; break; }  // found a control
                    else { ctrl = form.GetNextControl(ctrl, true); }
                }
                else
                {
                    if (marked) { exhausted = true; break; }// exhausted all the controls
                    else
                    {
                        ctrl = form.GetNextControl(null, true);
                        marked = true;
                    }
                }
            }

            return ctrl;
        }
        #endregion
    }
}
