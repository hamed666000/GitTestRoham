using System;
using System.IO;
using System.Net;
using System.Text;





namespace restClient_0
{

    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }



    class RESTClient
    {


        public string endPoint { get; set; }
        public string sData { get; set; }

        public httpVerb httpMethod { get; set; }

    

        public string makeRequest()
        {
            cPost Post = new cPost();
            Post.title = "Felix";
            Post.body = "Felix2";
            Post.userId = "Felix3";

            //sData = JsonConvert.SerializeObject(Post);
            sData = Post.getDataSet();


            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();


            request.ContentType = "application/json";

            request.Accept = "application/json";

            request.ContentLength = sData.Length;

           

            //request.X-API-Key = api_key;

            HttpWebResponse response = null;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);

            requestWriter.Write(sData);

            requestWriter.Close();


            try
            {
                response = (HttpWebResponse)request.GetResponse();


                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
        public string makeRequestMir( int iValue)
        {


            sData = "{ \"value\": xx}";
            sData = sData.Replace("xx", iValue.ToString());
            
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            httpMethod = httpVerb.PUT;
            request.Method = httpMethod.ToString();

            request.Headers["authorization"] = "Basic RmVsaXg6OGM2OTc2ZTViNTQxMDQxNWJkZTkwOGJkNGRlZTE1ZGZiMTY3YTljODczZmM0YmI4YTgxZjZmMmFiNDQ4YTkxOA==";

            request.Headers["Accept-Language"] = "de_DE";

            //request.Headers["accept"] = "application/json";

            request.ContentType = "application/json";

            request.Accept = "application/json";

            request.ContentLength = sData.Length;

          


            // request.X-API-Key = api_key;

            HttpWebResponse response = null;
            try
            {
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(sData);

                requestWriter.Close();
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }



            try
            {
                response = (HttpWebResponse)request.GetResponse();


                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
               if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
        public string makeRequestMiRReg()
        {

            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            request.Headers["authorization"] = "Basic RmVsaXg6OGM2OTc2ZTViNTQxMDQxNWJkZTkwOGJkNGRlZTE1ZGZiMTY3YTljODczZmM0YmI4YTgxZjZmMmFiNDQ4YTkxOA==";
                                                
            request.Headers["Accept-Language"] = "de_DE";

            //request.Headers["accept"] = "application/json";

            request.ContentType = "application/json";

            request.Accept = "application/json";

            //request.ContentLength = sData.Length;

            HttpWebResponse response = null;
            try
            {
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(sData);

                requestWriter.Close();
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }



            try
            {
                response = (HttpWebResponse)request.GetResponse();


                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
        public string makeRequestMiRRegWrite( int iValue)
        {

            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpVerb.PUT.ToString();

            request.Headers["authorization"] = "Basic RmVsaXg6OGM2OTc2ZTViNTQxMDQxNWJkZTkwOGJkNGRlZTE1ZGZiMTY3YTljODczZmM0YmI4YTgxZjZmMmFiNDQ4YTkxOA==";

            request.Headers["Accept-Language"] = "de_DE";

            //request.Headers["accept"] = "application/json";

            request.ContentType = "application/json";

            request.Accept = "application/json";

            //request.ContentLength = sData.Length;

            HttpWebResponse response = null;
            try
            {
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(sData);

                requestWriter.Close();
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }



            try
            {
                response = (HttpWebResponse)request.GetResponse();


                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;





        }


    }
}
