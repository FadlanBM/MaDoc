package com.example.applicationgi

import android.content.ClipData.Item
import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.ContactsContract
import android.util.Log
import android.view.LayoutInflater
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.recyclerview.widget.RecyclerView
import com.example.applicationgi.Adapter.MyHolder
import com.example.applicationgi.databinding.ActivityMainBinding
import com.example.applicationgi.util.SharePref

class MainActivity : AppCompatActivity() {
    private lateinit var binding:ActivityMainBinding;
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityMainBinding.inflate(layoutInflater);
        setContentView(binding.root)

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
        }
        binding.btnScanqr.setOnClickListener {
            startActivity(Intent(this,QRCodeScanActivity::class.java))
        }*/
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