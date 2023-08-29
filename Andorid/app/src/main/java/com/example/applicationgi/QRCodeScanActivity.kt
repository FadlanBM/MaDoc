package com.example.applicationgi

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.activity.result.contract.ActivityResultContracts
import com.example.applicationgi.databinding.ActivityQrcodeScanBinding
import com.github.drjacky.imagepicker.ImagePicker
import com.github.drjacky.imagepicker.constant.ImageProvider
import com.squareup.picasso.Picasso
import java.io.File

class QRCodeScanActivity : AppCompatActivity() {
    private lateinit var binding: ActivityQrcodeScanBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityQrcodeScanBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        binding.imageProfile.setOnClickListener{
            picimage()
        }
        binding.btnBackMenu.setOnClickListener {
            startActivity(Intent(this,MainActivity::class.java))
        }
        binding.btnScanQr.setOnClickListener {
            startActivity(Intent(this,ListDataActivity::class.java))
        }
    }

    private fun picimage(){
        ImagePicker.with(this)
            //...
            .cropSquare()    //Crop square image, its same as crop(1f, 1f)
            .maxResultSize(1080, 1080, true) //true: Keep Ratio
            .provider(ImageProvider.BOTH) //Or bothCameraGallery()
            .createIntentFromDialog { launcher.launch(it) }

    }
    private val launcher =
        registerForActivityResult(ActivityResultContracts.StartActivityForResult()) {
            if (it.resultCode == Activity.RESULT_OK) {
                val uri = it.data?.data!!
                Picasso.get().load(uri).into(binding.imageProfile)
            }
        }
}