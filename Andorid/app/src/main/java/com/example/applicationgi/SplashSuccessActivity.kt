package com.example.applicationgi

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle

class SplashSuccessActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash_success)

        supportActionBar?.hide()
    }
}