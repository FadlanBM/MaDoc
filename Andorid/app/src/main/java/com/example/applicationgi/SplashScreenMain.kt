package com.example.applicationgi

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler

class SplashScreenMain : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash_screen_main)
        supportActionBar?.hide()

        Handler().postDelayed({
            startActivity(Intent(this,LoginActivity::class.java))
            finish()
        },2400)
    }
}