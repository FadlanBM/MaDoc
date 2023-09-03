package com.example.applicationgi

import android.content.Context
import android.content.Intent
import android.os.AsyncTask
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import android.widget.LinearLayout
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.applicationgi.Adapter.ItemAdapter
import com.example.applicationgi.Adapter.Item
import com.example.applicationgi.databinding.ActivityMainBinding
import com.example.applicationgi.util.BaseApi
import com.example.applicationgi.util.SharePref
import org.json.JSONArray
import java.io.BufferedReader
import java.io.InputStreamReader
import java.lang.Exception
import java.lang.StringBuilder
import java.net.HttpURLConnection
import java.net.URL

class MainActivity : AppCompatActivity() {
    private lateinit var binding:ActivityMainBinding;
    private lateinit var itemAdapter: ItemAdapter
    private lateinit var recyclerView: RecyclerView
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityMainBinding.inflate(layoutInflater);
        setContentView(binding.root)

        recyclerView=findViewById(R.id.recyclerView)
        recyclerView.layoutManager=LinearLayoutManager(this)
        itemAdapter= ItemAdapter(emptyList())
        recyclerView.adapter=itemAdapter
        FetchDataTask().execute()

        var item= arrayListOf("Show All","Selesai","Belum Selesai","Belum Selesai","Belum Selesai")
        var autoComplete:AutoCompleteTextView=findViewById(R.id.cb_ordelist)
        var adapter=ArrayAdapter(this,R.layout.item_text,item)

        var sf=SharePref(this)
        if (sf.getToken()==null){
            startActivity(Intent(this,LoginActivity::class.java))
        }
        autoComplete.setAdapter(adapter)
        autoComplete.onItemClickListener=AdapterView.OnItemClickListener { adapterView, view, i, l ->
            val itemselected =adapterView.getItemAtPosition(i)
            Toast.makeText(this,"Item : $itemselected",Toast.LENGTH_SHORT).show()
        }
        /*binding.btnDetailDoc.setOnClickListener {
            startActivity(Intent(this,ListDataActivity::class.java))
        }
        binding.btnListPenerima.setOnClickListener {
            startActivity(Intent(this,ListPenerimaActivity::class.java))
        }*/
        binding.btnScanqr.setOnClickListener {
            startActivity(Intent(this,QRCodeScanActivity::class.java))
        }
    }


    private inner class FetchDataTask:AsyncTask<Void,Void,List<Item>>(){
        override fun doInBackground(vararg p0: Void?): List<Item> {
            var data= mutableListOf<Item>()
            try {
                val url=URL(BaseApi.BASEAPI+"api/DocList")
                val httpURLConnection=url.openConnection() as HttpURLConnection
                httpURLConnection.requestMethod="GET"
                var token="bearer ${SharePref.getInstance(this@MainActivity).getToken()}"
                httpURLConnection.setRequestProperty("Authorization",token)
                val inputStream=httpURLConnection.inputStream
                val reader=BufferedReader(InputStreamReader(inputStream))
                val response= StringBuilder()
                var line:String?
                while (reader.readLine().also { line=it }!=null){
                    response.append(line)
                }
                reader.close()
                httpURLConnection.disconnect()

                val jsonArray=JSONArray(response.toString())
                for (i in 0 until jsonArray.length()){
                    val jsonObject=jsonArray.getJSONObject(i)
                    val nameDoc=jsonObject.getString("nameDoc")
                    data.add(Item(nameDoc))
                }
                    Log.e("ListData",data.toString())

            }catch (e:Exception){
                Log.e("Error Connect Task","Error $e")
            }
            return data
        }

        override fun onPostExecute(result: List<Item>?) {
            super.onPostExecute(result)


        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.cusotm_menu,menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when(item.itemId){
            R.id.tb_settings->{
                Toast.makeText(this, "Click Settigns", Toast.LENGTH_SHORT).show()
                return true
            }
            R.id.tb_logout->{
                var message=AlertDialog.Builder(this)
                message.setTitle("Warning")
                message.setMessage("Apakah anda yakin ingin logout ?")
                message.setPositiveButton("Yes"){_,_->
                Toast.makeText(this,"Logout",Toast.LENGTH_SHORT).show()
                }
                message.setNegativeButton("No",null)
                message.show()
                return true
            }
            else->super.onOptionsItemSelected(item)
        }
    }
}