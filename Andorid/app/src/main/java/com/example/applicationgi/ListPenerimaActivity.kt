package com.example.applicationgi

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.applicationgi.databinding.ActivityListPenerimaBinding

class ListPenerimaActivity : AppCompatActivity() {
    private lateinit var binding:ActivityListPenerimaBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityListPenerimaBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        binding.btnBack.setOnClickListener {
            startActivity(Intent(this,MainActivity::class.java))
        }
    }
}