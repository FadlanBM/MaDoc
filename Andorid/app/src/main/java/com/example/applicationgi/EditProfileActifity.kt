package com.example.applicationgi

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Adapter
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import com.example.applicationgi.databinding.ActivityEditProfileActifityBinding

class EditProfileActifity : AppCompatActivity() {
    private lateinit var binding:ActivityEditProfileActifityBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding=ActivityEditProfileActifityBinding.inflate(layoutInflater)
        setContentView(binding.root)

        var item= arrayListOf("Nama","Lisy")
        var autocom:AutoCompleteTextView=binding.comboBoxListIden
        var adapater=ArrayAdapter(this,R.layout.item_listiden,item)
        autocom.setAdapter(adapater)
        autocom.onItemClickListener=AdapterView.OnItemClickListener{adapterview, view, i, l ->
            val itemSelected=adapterview.getItemAtPosition(i)
        }
            binding.btnBack.setOnClickListener{
                startActivity(Intent(this,SettingsActivity::class.java))
            }
    }
}