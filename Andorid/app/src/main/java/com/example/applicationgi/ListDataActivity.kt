package com.example.applicationgi

import android.content.Context
import android.content.Intent
import android.os.AsyncTask
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.MutableLiveData
import com.example.applicationgi.databinding.ActivityListdataBinding
import com.example.applicationgi.util.BaseApi
import com.example.applicationgi.util.GetId
import com.google.gson.JsonObject
import org.json.JSONObject
import java.io.InputStreamReader
import java.net.HttpURLConnection
import java.net.URL


class ListDataActivity : AppCompatActivity() {
    private lateinit var binding:ActivityListdataBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityListdataBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        Toast.makeText(this,"Data ${GetId.getId}",Toast.LENGTH_SHORT).show()
        getData(GetId.getId,this,binding).execute()

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
            startActivity(Intent(this,MainActivity::class.java))
        }
    }

    @Suppress("DEPRECATION")
    class getData(private val id:String,private var context: Context,private  var binding: ActivityListdataBinding):AsyncTask<String,String,String>(){
        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg p0: String?): String {
            var result=""
            try {
                var httpURLConnection:HttpURLConnection?=null
                try {
                    var url=URL(BaseApi.BASEAPI+"api/QrValidation/$id")
                    httpURLConnection=url.openConnection() as HttpURLConnection
                    httpURLConnection.requestMethod="GET"

                    var inputStream=httpURLConnection.inputStream
                    var inputStreamReader=InputStreamReader(inputStream)
                    var data=inputStreamReader.read()

                    while (data!=-1){
                        result+=data.toChar()
                        data=inputStreamReader.read()
                    }
                    if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){
                        var jsonObject=JSONObject(result)
                        binding.lbNamaDoc.text=jsonObject.getString("nameDokumen")
                        binding.lbPengirim.text=jsonObject.getString("pengirimDokumen")
                        binding.lbPerihalDoc.text=jsonObject.getString("perihalDokumen")
                        binding.lbUraian.text=jsonObject.getString("uraianDokumen")
                        binding.lbTglDoc.text=jsonObject.getString("tglDokumen")
                        binding.lbAgendaAwal.text=jsonObject.getString("tglAgendaAwal")
                        binding.lbAgendaAkhir.text=jsonObject.getString("tglAgendaAkhir")
                        binding.lbTglDiterima.text=jsonObject.getString("tglDiterima")
                        binding.lbAgendaDoc.text=jsonObject.getString("agendaDokumen")
                    }
                }catch (e:Exception){
                    Log.e("Error Http","Error $e")
                }
            }catch (ex:Exception){
                Log.e("Error Http","Error $ex")
            }
            return result
        }

        override fun onPostExecute(result: String?) {
            super.onPostExecute(result)

        }
    }
}