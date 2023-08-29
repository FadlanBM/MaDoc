package com.example.applicationgi

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.appcompat.app.AlertDialog
import com.example.applicationgi.databinding.ActivityListPenerimaBinding
import com.example.applicationgi.databinding.ActivityListdataBinding

class ListDataActivity : AppCompatActivity() {
    private lateinit var binding:ActivityListdataBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityListdataBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()



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
}