package com.example.applicationgi

import android.content.Context
import android.content.Intent
import android.os.AsyncTask
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AlertDialog
import com.example.applicationgi.databinding.ActivityListPenerimaBinding
import com.example.applicationgi.databinding.ActivityListdataBinding
import com.example.applicationgi.util.BaseApi
import com.example.applicationgi.util.GetData
import com.example.applicationgi.util.SharePref
import org.json.JSONObject
import java.io.InputStreamReader
import java.io.OutputStream
import java.io.OutputStreamWriter
import java.net.HttpURLConnection
import java.net.URL

class ListDataActivity : AppCompatActivity() {
    private lateinit var binding:ActivityListdataBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityListdataBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        Log.e("Data",GetData.setId)
        GetDataDoc(this,binding).execute()

        binding.btnBack.setOnClickListener {
           var alert=AlertDialog.Builder(this)
            alert.setTitle("Warning")
            alert.setMessage("Apakah anda ingin keluar ?")
            alert.setPositiveButton("Ya"){_,_->
                startActivity(Intent(this,MainActivity::class.java))
            }
            alert.setNegativeButton("No",null)
            alert.show()
        }
        binding.btnKonfirmasi.setOnClickListener {
            ValidDocUser(this).execute()
        }
    }

    @Suppress("DEPRECATION")
    class ValidDocUser(private var context: Context):AsyncTask<String,String,String>(){
        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg params: String?): String {
            var result=""
            var token="bearer "+SharePref.getInstance(context).getToken()
            var idDoc=GetData.setId
            try {
                var httpURLConnection:HttpURLConnection?=null
                var url=URL(BaseApi.BASEAPI+"api/QrValidation/$idDoc")
                httpURLConnection=url.openConnection() as HttpURLConnection
                httpURLConnection.requestMethod="POST"
                httpURLConnection.setRequestProperty("Authorization",token)

                if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){
                    context.startActivity(Intent(context,SplashSuccessActivity::class.java))
                }
            }catch (e:Exception){
                Log.e("ErrorHttp","Error : $e")
            }
            return result
        }

        override fun onPostExecute(result: String?) {
            super.onPostExecute(result)
        }
    }

    @Suppress("DEPRECATION")
    class GetDataDoc(private val context: Context,private val binding: ActivityListdataBinding):AsyncTask<String,String,String>(){

        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg params: String?): String {
            var result=""
            var id=GetData.setId
            var token="bearer "+SharePref.getInstance(context).getToken()
            Log.e("token",token)
            try {
                var httpURLConnection:HttpURLConnection?=null
                var url=URL(BaseApi.BASEAPI+"api/QrValidation/$id")
                httpURLConnection=url.openConnection() as HttpURLConnection
                httpURLConnection.requestMethod="GET"
                httpURLConnection.setRequestProperty("Authorization",token)
                httpURLConnection.setRequestProperty("Content-Type","application/json")
                httpURLConnection.setRequestProperty("Accep","text/plain")

                var inputStream=httpURLConnection.inputStream
                var inputStreamReader=InputStreamReader(inputStream)
                var data=inputStreamReader.read()

                while (data !=-1){
                    result +=data.toChar()
                    data=inputStreamReader.read()
                }

                if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){

                }
            }catch (e:Exception){
                Log.e("Error Http","Error : $e")
            }
            return  result
        }

        override fun onPostExecute(result: String?) {
            super.onPostExecute(result)
            var jsonObject=JSONObject(result)
            binding.lbNameDoc.text=jsonObject.getString("nameDokumen")
            binding.lbAgendaDoc.text=jsonObject.getString("agendaDokumen")
            binding.lbPerihal.text=jsonObject.getString("perihalDokumen")
            binding.lbNamaPengirim.text=jsonObject.getString("pengirimDokumen")
            binding.lbTglDoc.text=jsonObject.getString("tglDokumen")
            binding.lbTglDiterima.text=jsonObject.getString("tglDiterima")
            binding.lbTglawal.text=jsonObject.getString("tglAgendaAwal")
            binding.tbTglAkhir.text=jsonObject.getString("tglAgendaAkhir")
            binding.lbUraianDoc.text=jsonObject.getString("uraianDokumen")
        }
    }
}