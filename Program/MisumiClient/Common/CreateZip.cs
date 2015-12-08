using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Misumi_Client.Common
{
    public class ClientCreateZip
    {
        private string _sZipDirectory = string.Empty;
        private string _sUnZipDirectory = string.Empty;
        private string _sZipPath = string.Empty;
        private int _iZipLevel = 6;                     // 0 - store only to 9 - means best compression
        private string _sZipPassword = string.Empty;
        private bool _isFile = false;

        /// <summary>
        /// 需压缩文件夹
        /// </summary>
        public string ZipDirectory { set { _sZipDirectory = value; } }
        /// <summary>
        /// 是否是文件，默认为否
        /// </summary>
        public bool IsFile { set { _isFile = value; } }
        /// <summary>
        /// 解压到文件夹
        /// </summary>
        public string UnZipDirectory { set { _sUnZipDirectory = value; } }
        /// <summary>
        /// 压缩文件路径
        /// </summary>
        public string ZipPath { set { _sZipPath = value; } }
        /// <summary>
        /// 压缩率 0～9 默认为6
        /// </summary>
        public int ZipLevel { get { return _iZipLevel; } set { _iZipLevel = value; } }
        /// <summary>
        /// 压缩密码
        /// </summary>
        public string ZipPassword { set { _sZipPassword = value; } }
        /// <summary>
        /// 执行压缩
        /// </summary>
        /// <returns></returns>
        public bool StartZip()
        {
            if (_sZipDirectory == string.Empty || _sZipPath == string.Empty)
                return false;
            if (!Directory.Exists(_sZipDirectory) & !File.Exists(_sZipDirectory))//如果既不存在文件夹，也不存在文件
                return false;
            FileInfo fi = new FileInfo(_sZipPath);
            if (!fi.Directory.Exists)
                fi.Directory.Create();
            ZipOutputStream s = new ZipOutputStream(File.Create(_sZipPath));
            s.Password = _sZipPassword;
            s.SetLevel(_iZipLevel);
            ZipCompression(_sZipDirectory, s);
            s.Finish();
            s.Close();
            return true;
        }
        private void ZipCompression(string sDirectory, ZipOutputStream zipstream)
        {
            if ((sDirectory[sDirectory.Length - 1] == Path.DirectorySeparatorChar))
            {
                string[] filenames = Directory.GetFileSystemEntries(sDirectory);
                foreach (string file in filenames)
                {
                    if (Directory.Exists(file))
                    {
                        ZipCompression(file, zipstream);
                    }
                    else
                    {
                        //打开压缩文件
                        FileStream fs = File.OpenRead(file);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string tempfile = file.Substring(_sZipDirectory.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(tempfile);
                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;
                        fs.Close();
                        zipstream.PutNextEntry(entry);
                        zipstream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            else
            {
                //打开压缩文件
                FileStream fs = File.OpenRead(sDirectory);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                string tempfile = sDirectory.Substring(sDirectory.LastIndexOf("\\") + 1);
                ZipEntry entry = new ZipEntry(tempfile);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                zipstream.PutNextEntry(entry);
                zipstream.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 执行解压
        /// </summary>
        /// <returns></returns>
        public void StartUnZip()
        {
            if (_sUnZipDirectory[_sUnZipDirectory.Length - 1] != Path.DirectorySeparatorChar)
                _sUnZipDirectory += Path.DirectorySeparatorChar;
            try
            {
                ZipInputStream s = new ZipInputStream(File.OpenRead(_sZipPath.Trim()));
                s.Password = _sZipPassword;
                ZipEntry entry;
                while ((entry = s.GetNextEntry()) != null)
                {
                    string sPath = _sUnZipDirectory + entry.Name;
                    FileInfo fi = new FileInfo(sPath);
                    if (!fi.Directory.Exists)
                        fi.Directory.Create();
                    if (File.Exists(sPath))
                        File.Delete(sPath);
                    FileStream streamWriter = File.Create(sPath);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                            streamWriter.Write(data, 0, size);
                        else
                            break;
                    }
                    streamWriter.Close();
                }
                s.Close();
            }
            catch
            {
            }
        }
    }
}
