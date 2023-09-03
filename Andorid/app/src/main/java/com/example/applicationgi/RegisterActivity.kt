package com.example.applicationgi

import android.content.Context
import android.content.Intent
import android.os.AsyncTask
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Parcel
import android.renderscript.ScriptGroup.Binding
import android.util.Log
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import android.widget.Toast
import com.example.applicationgi.databinding.ActivityRegisterBinding
import com.example.applicationgi.util.BaseApi
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import org.json.JSONObject
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.net.HttpURLConnection
import java.net.URL

class RegisterActivity : AppCompatActivity() {
    private  lateinit var binding:ActivityRegisterBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding=ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        GetIdentitas(binding,this).execute()

        binding.btnRegister.setOnClickListener {
            if (binding.tbNameLogin.length()==0)
                binding.tbNameLogin.setError("Form Name belum terisi")
            if (binding.tbUsernameLogin.length()==0)
                binding.tbUsernameLogin.setError("Form Username belum terisi")
            if (binding.tbPasswordLogin.length()==0)
                binding.tbPasswordLogin.setError("Form Password belum terisi")
            if (binding.tbNoIdentitas.length()==0)
                binding.tbNoIdentitas.setError("Form No Identitas belum terisi")
            else {
                postAccunt(binding,this).execute()
                startActivity(Intent(this,LoginActivity::class.java))
            }

        }
        binding.tbToLogin.setOnClickListener {
            startActivity(Intent(this,LoginActivity::class.java))
        }
    }

    @Suppress("DEPRECATION")
    class postAccunt(private val binding: ActivityRegisterBinding,private val context: Context):AsyncTask<String,String,String>(){
        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg params: String?): String {
            var result=""
            try {
                var jsonObject=JSONObject()
                jsonObject.put("name",binding.tbNameLogin.text)
                jsonObject.put("username",binding.tbUsernameLogin.text)
                jsonObject.put("password",binding.tbPasswordLogin.text)
                jsonObject.put("noIdentitas",binding.tbNoIdentitas.text)
                jsonObject.put("idIdentitas",binding.comboBoxListIdent.text)
                var jsonObjectString=jsonObject.toString()
                var con:HttpURLConnection?=null
                try {
                    var url=URL(BaseApi.BASEAPI+"api/Register/")
                    con=url.openConnection() as HttpURLConnection
                    con.requestMethod="POST"
                    con.setRequestProperty("Content-Type","application/json")
                    con.setRequestProperty("Accept","text/plain")

                    var outputStream=con.outputStream
                    var outputStreamWriter=OutputStreamWriter(outputStream)
                    outputStreamWriter.write(jsonObjectString)
                    outputStreamWriter.flush()

                    if (con.responseCode==HttpURLConnection.HTTP_OK){
                        Log.e("Success","Berhasil Input data")
                    }
                }catch (ex:Exception){
                    Log.e("Error Http","Error : $ex")

                }
            }catch (e:Exception){
                Log.e("Error Http","Error : $e")
            }
            Log.e("Success","Berhasil adsjahidhshdsk")
            return "Ok"
        }

        override fun onPostExecute(result: String?) {
            super.onPostExecute(result)
        }
    }

    @Suppress("DEPRECATION")
    class GetIdentitas(private val binding: ActivityRegisterBinding,private val  context: Context):AsyncTask<String,String,String>(){
        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg params: String?): String {
            var resould=""
            var httpURLConnection:HttpURLConnection?=null
                var url=URL(BaseApi.BASEAPI+"api/Identitas/")
                httpURLConnection=url.openConnection() as HttpURLConnection
                var inputStream=httpURLConnection!!.inputStream
                var inputStreamReader=InputStreamReader(inputStream)
                var data=inputStreamReader.read()

                while (data !=-1){
                    resould+=data.toChar()
                    data=inputStreamReader.read()
                }
                if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){
                    var jsonArray=JSONArray(resould)
                    Log.e("List Data",jsonArray.toString())
                }
            return  resould
        }
        fun jsonArrayToArrayList(jsonArray: JSONArray): ArrayList<Any> {
            val gson = Gson()
            val itemType = object : TypeToken<ArrayList<Any>>() {}.type
            return gson.fromJson(jsonArray.toString(), itemType)
        }

        fun getJSONObjectById(jsonArray: JSONArray, targetId: Int): JSONObject? {
            for (i in 0 until jsonArray.length()) {
                val item = jsonArray.getJSONObject(i)
                if (item.getInt("id") == targetId) {
                    return item
                }
            }
            return null
        }
        override fun onPostExecute(result: String?) {

            var nilai=JSONArray(result)
            val item =ArrayList<String>()
            for (i in 0 until nilai.length()){
                val jsonObject=nilai.getJSONObject(i)
                val name=jsonObject.getString("nama")
                item.add(name)
            }
            Log.e("Show Data",nilai.toString())
//            val item= jsonArrayToArrayList(JSONArray(nilai.toString().replace("{","").replace("}","").replace("nama","").replace("=","").replace(":","").replace("\"","")))
            Log.e("Show Data",item.toString())
            var autoComplite:AutoCompleteTextView=binding.comboBoxListIdent
            var adapter=ArrayAdapter(context,R.layout.item_listiden,item)

            autoComplite.setAdapter(adapter)
            autoComplite.onItemClickListener=AdapterView.OnItemClickListener { adapterView, view,i,l ->
                val itemSelected=adapterView.getItemAtPosition(i)
            }
        }
    }
}