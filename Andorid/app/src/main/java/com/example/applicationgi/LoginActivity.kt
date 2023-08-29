package com.example.applicationgi

import android.app.Activity
import android.content.AsyncTaskLoader
import android.content.Context
import android.content.Intent
import android.os.AsyncTask
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.util.Log
import android.widget.Toast
import com.example.applicationgi.databinding.ActivityLoginBinding
import org.json.JSONObject
import org.json.JSONStringer
import java.io.InputStreamReader
import java.io.OutputStream
import java.io.OutputStreamWriter
import java.lang.Exception
import java.net.HttpURLConnection
import java.net.URL

class LoginActivity : AppCompatActivity() {
    private lateinit var binding: ActivityLoginBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        binding= ActivityLoginBinding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(binding.root)
        supportActionBar?.hide()

        binding.btnToregister.setOnClickListener{
            startActivity(Intent(this,RegisterActivity::class.java))
        }
        binding.btnLogin.setOnClickListener {
            if (binding.tbUsername.length()==0){
                binding.tbUsername.setError("Username masih kosong")
            }
            if (binding.tbPassword.length()==0){
                binding.tbPassword.setError("Password masih kosong")
            }
            Auth(binding,this).execute()
        }
    }
   @Suppress("DEPRECATION")
   class Auth(private val binding: ActivityLoginBinding,private val context: Context):AsyncTask<String,String,String>(){
       override fun onPreExecute() {
           super.onPreExecute()
       }

       override fun doInBackground(vararg p0: String?): String {
                var result=""
           try {
               var jsonObject=JSONObject()
               jsonObject.put("name",binding.tbUsername.text)
               jsonObject.put("password",binding.tbPassword.text)
               var jsonObjectString=jsonObject.toString()
               var httpURLConnection:HttpURLConnection?=null
               try {
                   var url=URL("http://192.168.2.129:7105/Api/Auth/")
                   httpURLConnection=url.openConnection() as HttpURLConnection
                   httpURLConnection.requestMethod="POST"
                   httpURLConnection.setRequestProperty("Content-Type","application/json")
                   httpURLConnection.setRequestProperty("Accept","text/plain")

                   var outputStream=httpURLConnection.outputStream
                   var outputStreamWriter=OutputStreamWriter(outputStream)
                   outputStreamWriter.write(jsonObjectString)
                   outputStreamWriter.flush()
                    if (httpURLConnection.responseCode==HttpURLConnection.HTTP_NOT_FOUND){
                        var hendle=Handler(context.mainLooper)
                        hendle.post(Runnable {
                            Toast.makeText(context,"Username tidak di temukan",Toast.LENGTH_SHORT).show()
                        })
                    }else if (httpURLConnection.responseCode==HttpURLConnection.HTTP_BAD_REQUEST){
                        var handler=Handler(context.mainLooper)
                        handler.post(Runnable {
                            Toast.makeText(context,"Password yang anda masukkan salah",Toast.LENGTH_SHORT).show()
                        })
                    }
                   var inputStream=httpURLConnection.inputStream
                   var inputStreamReader=InputStreamReader(`inputStream`)
                   var data=inputStreamReader.read()

                   while (data!=-1){
                        result+=data.toChar()
                       data=inputStreamReader.read()
                   }
                   if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){
                       var jsonObject=JSONObject(result)
                       var token=jsonObject.getString("token")
                       Log.e("Token",token)
                   }
               }catch (ex:Exception){
                   Log.d("ErrorConnection",ex.toString())
               }
           }catch (e:Exception){
               Log.d("ErrorConnection",e.toString())
           }
           return result
       }

       override fun onPostExecute(result: String?) {
           super.onPostExecute(result)
       }
   }


}