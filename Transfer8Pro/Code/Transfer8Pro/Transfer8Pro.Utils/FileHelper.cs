﻿using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer8Pro.Utils
{
    /*
      大文件拷贝之所以用文件流来进行拷贝，主要是由于如果用File静态类来执行拷贝就是将整个文件整体传输，对于一个好几个G的大文件会造成内存占用大，运行慢，
      效率不高。所以用到文件流拷贝。文件流拷贝可以设置拷贝的二进制流缓冲区的大小，然后根据缓冲区的大小来一点一点拷贝，就类似与U盘拷贝文件到电脑似的。

     */

    /// <summary>
    /// 文件操作类
    /// https://blog.csdn.net/IstarI/article/details/51356667
    /// https://www.cnblogs.com/lyd2016/p/6599550.html
    /// https://www.cnblogs.com/vevi/p/5705105.html
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 返回指示文件是否已被其它程序使用
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static bool CheckFileInUse(string fileFullName)
        {
            bool result = false;
            if (!File.Exists(fileFullName))
            {
                result = false;
            }
            else
            {
                try
                {
                    using (FileStream fs = File.Open(fileFullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        result = false;
                    }
                }
                catch (IOException ioeEx)
                {
                    result = true;
                }
                catch(Exception ex)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public static bool MoveFile(string sourcePath, string destPath)
        {
            try
            {
                if (!File.Exists(sourcePath))
                {
                    LogUtil.WriteLog("FileHelper.MoveFile()源文件不存在");
                    return false;
                }

                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                File.Move(sourcePath, destPath);
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public static bool MoveFolder(string sourcePath, string destPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                LogUtil.WriteLog("FileHelper.MoveFolder()源目录不存在");
                return false;
            }

            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    #region 创建目录
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteLog(ex);
                        return false;
                    }
                    #endregion
                }
            }

            //获得源文件下所有文件
            List<string> files = new List<string>(Directory.GetFiles(sourcePath));
            files.ForEach(sourceFile =>
            {
                string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(sourceFile) });
                //覆盖模式
                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                }
                File.Move(sourceFile, destFile);
            });

            //获得源文件下所有目录文件
            List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
            folders.ForEach(sourceDir =>
            {
                string destDir = Path.Combine(destPath, Path.GetFileName(sourceDir));
                //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                //判断源和目标是否在一个盘符下
                if (Path.GetPathRoot(sourcePath).ToUpper() == Path.GetPathRoot(destPath).ToUpper())
                {
                    Directory.Move(sourceDir, destDir);
                }
                else
                {
                    //不在一个盘符下 采用递归方法实现 
                    MoveFolder(sourceDir, destDir);
                }
            });

            //当前源移动完成，尝试删除当前目录 
            if (Directory.GetFileSystemEntries(sourcePath).Length == 0)
            {
                try
                {
                    Directory.Delete(sourcePath);
                }
                catch (Exception ex)
                {
                    LogUtil.WriteLog(ex);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 复制文件夹包含子文件及子目录
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        private bool CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    #region 创建目标目录
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteLog(ex);
                        return false;
                    }
                    #endregion
                }

                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(sourceFile =>
                {
                    string destFile = Path.Combine(destPath, Path.GetFileName(sourceFile));
                    File.Copy(sourceFile, destFile, true); //目标文件有相同的覆盖
                });

                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(sourceDir =>
                {
                    string destDir = Path.Combine(destPath, Path.GetFileName(sourceDir));
                    CopyFolder(sourceDir, destDir);
                });
            }
            else
            {
                LogUtil.WriteLog("FileHelper.CopyFolder()源目录不存在");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="sourefile"></param>
        /// <param name="destfile"></param>       
        /// <param name="bufferSize">缓冲区大小 默认为10M</param>
        /// <returns></returns>
        public static void CopyFile(string sourefile, string destfile, int bufferSize = 1024 * 1024 * 10)
        {
            /*
             当用文件流FileStream来读取文本文档的时候，由于汉字是用2个字节编码，而字母是1个字节。
             对于一个固定的二进制字节流缓冲区，不能很好的区分汉字和字母，这样有可能缓冲区完毕之后最后一个汉字读到1个字节，
             这就是“半个汉字“这就出现了信息不完整的现象。所以这时读取文本文档就要用StreamWriter，StreamReader。
             */

            if (!File.Exists(sourefile))
            {
                throw new Exception($"CopyFile()方法，参数sourefile[{sourefile}]源文件不存在");
            }

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //1、创建一个读取源文件的文件流
            using (FileStream fsRead = new FileStream(sourefile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //2、创建一个写入目标文件的文件流
                using (FileStream fsWrite = new FileStream(destfile, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    //拷贝文件的时候，创建一个中间缓冲区
                    byte[] bytes = new byte[bufferSize];
                    //返回值表示本次实际读到的字节个数
                    int r = fsRead.Read(bytes, 0, bytes.Length);
                    while (r > 0)
                    {
                        //将读取到的内容写入到新文件
                        //第三个参数应该是实际读取到的字节数，而不是数组的长度
                        fsWrite.Write(bytes, 0, r);
                        //Console.Write(".");
                        r = fsRead.Read(bytes, 0, bytes.Length);
                    }
                }
            }
            //sw.Stop();
            //Console.WriteLine($"CopyFile(),用时：{sw.ElapsedMilliseconds}");                
        }


        /// <summary>
        ///// 文件复制
        ///// </summary>
        ///// <param name="sourefile"></param>
        ///// <param name="destfile"></param>     
        ///// <param name="bufferSize">缓冲区大小 默认为10M</param>
        ///// <returns></returns>
        //public static async Task<bool> CopyFileAsync(string sourefile, string destfile, int bufferSize = 1024 * 1024 * 10)
        //{
        //    /*
        //     当用文件流FileStream来读取文本文档的时候，由于汉字是用2个字节编码，而字母是1个字节。
        //     对于一个固定的二进制字节流缓冲区，不能很好的区分汉字和字母，这样有可能缓冲区完毕之后最后一个汉字读到1个字节，
        //     这就是“半个汉字“这就出现了信息不完整的现象。所以这时读取文本文档就要用StreamWriter，StreamReader。

        //     */
        //    if (!File.Exists(sourefile))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        Stopwatch sw = new Stopwatch();
        //        sw.Start();
        //        //1、创建一个读取源文件的文件流
        //        using (FileStream fsRead = new FileStream(sourefile, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            //2、创建一个写入目标文件的文件流
        //            using (FileStream fsWrite = new FileStream(destfile, FileMode.Create, FileAccess.Write, FileShare.Read))
        //            {
        //                //拷贝文件的时候，创建一个中间缓冲区
        //                byte[] bytes = new byte[bufferSize];
        //                //返回值表示本次实际读到的字节个数
        //                int r = await fsRead.ReadAsync(bytes, 0, bytes.Length);
        //                while (r > 0)
        //                {
        //                    //将读取到的内容写入到新文件
        //                    //第三个参数应该是实际读取到的字节数，而不是数组的长度
        //                    await fsWrite.WriteAsync(bytes, 0, r);
        //                    Console.Write(".");
        //                    r = await fsRead.ReadAsync(bytes, 0, bytes.Length);
        //                }
        //            }
        //        }
        //        sw.Stop();
        //        Console.WriteLine($"CopyFileAsync(),用时：{sw.ElapsedMilliseconds}");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw new Exception(ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="sourefile"></param>
        /// <param name="destfile"></param>
        /// <param name="msg"></param>
        /// <param name="bufferSize">缓冲区大小 默认为5M</param>
        /// <returns></returns>
        public static bool CopyFilePlus(string sourefile, string destfile, int bufferSize = 1024 * 1024 * 5)
        {
            /*
             当用文件流FileStream来读取文本文档的时候，由于汉字是用2个字节编码，而字母是1个字节。
             对于一个固定的二进制字节流缓冲区，不能很好的区分汉字和字母，这样有可能缓冲区完毕之后最后一个汉字读到1个字节，
             这就是“半个汉字“这就出现了信息不完整的现象。所以这时读取文本文档就要用StreamWriter，StreamReader。

             */

            if (!File.Exists(sourefile))
            {
                LogUtil.WriteLog($"FileHelper.CopyFilePlus()源文件:{sourefile}不存在");
                return false;
            }
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //1、创建一个读取源文件的文件流
                using (FileStream readstream = new FileStream(sourefile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    #region 目标文件 中文出现乱码 原因及解决
                    //目标文件 中文出现乱码
                    //原因是自Windows 2000之后的操作系统在文件处理时默认编码采用Unicode
                    //所以.NET文件的默认编码也是Unicode。除非另外指定，StreamReader的默认编码为Unicode，
                    //而不是当前系统的ANSI代码页。但是文档大部分还是以ANSI编码存储，中文文本使用的是GB2312，所以才造成中文乱码
                    //所以在读取文本的时候要指定编码格式。使用System.Text.Encoding.Defaul告诉StreamReader采用目前操作系统的编码即可
                    #endregion
                    using (StreamReader srReader = new StreamReader(readstream, Encoding.Default)) //读一定要加编码，否则容易出现乱码
                    {
                        //2、创建一个写入目标文件的文件流
                        using (FileStream writestrem = new FileStream(destfile, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            using (StreamWriter swWriter = new StreamWriter(writestrem, Encoding.Default)) //从源文件中读取时，一定要保证源StreamReader的编码，否则中文可能会出乱码
                            {
                                //拷贝文件的时候，创建一个中间缓冲区
                                char[] charBuffer = new char[bufferSize];
                                //返回值表示本次实际读到的字节个数                               
                                int r = srReader.Read(charBuffer, 0, charBuffer.Length);
                                while (r > 0)
                                {
                                    //将读取到的内容写入到新文件
                                    //第三个参数应该是实际读取到的字节数，而不是数组的长度
                                    //fsWrite.Write(bytes, 0, r);
                                    swWriter.Write(charBuffer, 0, r);
                                    Console.Write(".");
                                    r = srReader.Read(charBuffer, 0, charBuffer.Length);
                                }

                                //一行一行读大文件不合适
                                //while (read.Peek() != -1)
                                //{
                                //    str = read.ReadLine();
                                //    Console.WriteLine(str);
                                //}                                
                            }
                        }
                    }
                }
                sw.Stop();
                Console.WriteLine($"CopyFilePlus(),用时：{sw.ElapsedMilliseconds}");
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                //throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourcePath">源文件全路径</param>
        /// <param name="destPath">目的文件全路径</param>      
        public static void ZipFile(string sourcePath, string destPath,string password = "")
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"FileHelper.ZipFile()源文件:{sourcePath}不存在!");
            }
            string zipedPath = FileHelper.GetDirectoryName(destPath);
            string zipedFileName = FileHelper.GetFileName(destPath);

            FileHelper.ZipFile(sourcePath, zipedPath, zipedFileName, password);
        }

        /// <summary>
        /// ZIP:压缩单个文件
        /// add yuangang by 2016-06-13
        /// </summary>
        /// <param name="FileToZip">需要压缩的文件（绝对路径）</param>
        /// <param name="ZipedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="ZipedFileName">压缩后的文件名称（文件名，默认 同源文件同名）</param>
        /// <param name="CompressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="BlockSize">缓存大小（每次写入文件大小，默认 5M）</param>
        /// <param name="IsEncrypt">是否加密（默认 不加密）</param>
        public static void ZipFile(string FileToZip, string ZipedPath, string ZipedFileName,string password,int BlockSize = 1024 * 1024 * 5, int CompressionLevel = 5)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException($"FileHelper.ZipFile()源文件:{FileToZip} 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string ZipFileName = !string.IsNullOrEmpty(ZipedFileName) && ZipedFileName.Contains(".zip") ? ZipedPath + "\\" + ZipedFileName : ZipedPath + "\\" + GetExtensionName(ZipedPath) + ".zip";

            using (FileStream ZipFile = File.Create(ZipFileName))
            {
                using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))
                {
                    using (FileStream StreamToZip = new FileStream(FileToZip, FileMode.Open, FileAccess.Read))
                    {
                        string fileName = FileToZip.Substring(FileToZip.LastIndexOf("\\") + 1);

                        ZipEntry ZipEntry = new ZipEntry(fileName);

                        if (!string.IsNullOrEmpty(password))
                        {
                            //压缩文件加密
                            ZipStream.Password = password;
                        }

                        ZipStream.PutNextEntry(ZipEntry);

                        //设置压缩级别
                        ZipStream.SetLevel(CompressionLevel);

                        //缓存大小
                        byte[] buffer = new byte[BlockSize];

                        int sizeRead = 0;

                        try
                        {
                            do
                            {
                                sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                                ZipStream.Write(buffer, 0, sizeRead);
                            }
                            while (sizeRead > 0);
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }

                        StreamToZip.Close();
                    }

                    ZipStream.Finish();
                    ZipStream.Close();
                }

                ZipFile.Close();
            }
        }

        /// <summary>
        /// ZIP：压缩文件夹
        /// add yuangang by 2016-06-13
        /// </summary>
        /// <param name="DirectoryToZip">需要压缩的文件夹（绝对路径）</param>
        /// <param name="ZipedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="ZipedFileName">压缩后的文件名称（文件名，默认 同源文件夹同名）</param>
        /// <param name="IsEncrypt">是否加密（默认 加密）</param>
        public static void ZipDirectory(string DirectoryToZip, string ZipedPath, string ZipedFileName = "", bool IsEncrypt = true)
        {
            //如果目录不存在，则报错
            if (!Directory.Exists(DirectoryToZip))
            {
                throw new FileNotFoundException("指定的目录: " + DirectoryToZip + " 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string ZipFileName = string.IsNullOrEmpty(ZipedFileName) ? ZipedPath + "\\" + new DirectoryInfo(DirectoryToZip).Name + ".zip" : ZipedPath + "\\" + ZipedFileName + ".zip";

            using (FileStream ZipFile = File.Create(ZipFileName))
            {
                using (ZipOutputStream s = new ZipOutputStream(ZipFile))
                {
                    if (IsEncrypt)
                    {
                        //压缩文件加密
                        s.Password = "123";
                    }
                    ZipSetp(DirectoryToZip, s, "");
                }
            }
        }

        /// <summary>
        /// ZIP:解压一个zip文件
        /// add yuangang by 2016-06-13
        /// </summary>
        /// <param name="ZipFile">需要解压的Zip文件（绝对路径）</param>
        /// <param name="TargetDirectory">解压到的目录</param>
        /// <param name="Password">解压密码</param>
        /// <param name="OverWrite">是否覆盖已存在的文件</param>
        public static void UnZip(string ZipFile, string TargetDirectory, string Password = "", bool OverWrite = true)
        {
            //如果解压到的目录不存在，则报错
            if (!Directory.Exists(TargetDirectory))
            {
                try
                {
                    Directory.CreateDirectory(TargetDirectory);
                }
                catch (Exception ex)
                {
                    LogUtil.WriteLog(ex);
                    throw new FileNotFoundException("指定的目录: " + TargetDirectory + $" 不存在!{ex.Message}");
                }
            }
            //目录结尾
            if (!TargetDirectory.EndsWith("\\")) { TargetDirectory = TargetDirectory + "\\"; }

            using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(ZipFile)))
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    zipfiles.Password = Password;
                }
               
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(TargetDirectory + directoryName);

                    if (fileName != "")
                    {
                        if ((File.Exists(TargetDirectory + directoryName + fileName) && OverWrite) || (!File.Exists(TargetDirectory + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(TargetDirectory + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipfiles.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }

                zipfiles.Close();
            }
        }

        #region 文件及文件夹路径操作
        /// <summary>
        /// 返回文件或文件夹所在目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// 返回扩展名 如 .txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetExtensionName(string path)
        {
            return Path.GetExtension(path);
        }

        /// <summary>
        /// 返回文件名 如 a.txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 返回不具有扩展名文件名 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// 更改文件扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extensionName">扩展名 .jpeg</param>
        /// <returns></returns>
        public static string ChangeExtension(string path, string extensionName)
        {
            return Path.ChangeExtension(path, extensionName);
        }

        /// <summary>
        /// 获取文件根据路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }
        #endregion

        /// <summary>
        /// 递归遍历目录
        /// add yuangang by 2016-06-13
        /// </summary>
        private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory += Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();

            string[] filenames = Directory.GetFileSystemEntries(strDirectory);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {

                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\") + 1);
                    pPath += "\\";
                    ZipSetp(file, s, pPath);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    using (FileStream fs = File.OpenRead(file))
                    {

                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(fileName);

                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;

                        fs.Close();

                        crc.Reset();
                        crc.Update(buffer);

                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);

                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
