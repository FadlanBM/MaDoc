package com.example.applicationgi

import android.app.Activity
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.os.AsyncTask
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.budiyev.android.codescanner.AutoFocusMode
import com.budiyev.android.codescanner.CodeScanner
import com.budiyev.android.codescanner.CodeScannerView
import com.budiyev.android.codescanner.DecodeCallback
import com.budiyev.android.codescanner.ErrorCallback
import com.budiyev.android.codescanner.ScanMode
import com.example.applicationgi.databinding.ActivityQrcodeScanBinding
import com.example.applicationgi.util.BaseApi
import com.example.applicationgi.util.GetId
import com.github.drjacky.imagepicker.ImagePicker
import com.github.drjacky.imagepicker.constant.ImageProvider
import com.squareup.picasso.Picasso
import org.json.JSONObject
import java.io.File
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.lang.Exception
import java.net.HttpURLConnection
import java.net.URL
import java.util.jar.Manifest

class QRCodeScanActivity : AppCompatActivity() {
    private lateinit var binding: ActivityQrcodeScanBinding

    private  lateinit var codeScanner:CodeScanner
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding= ActivityQrcodeScanBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.hide()

        if(ContextCompat.checkSelfPermission(this,android.Manifest.permission.CAMERA)==
                PackageManager.PERMISSION_DENIED){
            ActivityCompat.requestPermissions(this, arrayOf(android.Manifest.permission.CAMERA),123)
        }else{
            startScan()
        }
    }

    private  fun startScan(){
        val scannerview:CodeScannerView=binding.scannerView
        codeScanner= CodeScanner(this,scannerview)
        codeScanner.camera=CodeScanner.CAMERA_BACK
        codeScanner.formats=CodeScanner.ALL_FORMATS

        codeScanner.autoFocusMode=AutoFocusMode.SAFE
        codeScanner.scanMode=ScanMode.SINGLE
        codeScanner.isAutoFocusEnabled=false
        codeScanner.isFlashEnabled=false

        codeScanner.decodeCallback= DecodeCallback {
            runOnUiThread{
                postTokenDoc(this,it.text).execute()
                Toast.makeText(this,"Scan resoult ${it.text}",Toast.LENGTH_SHORT).show()
            }
        }
        codeScanner.errorCallback= ErrorCallback {
            runOnUiThread{
                Toast.makeText(this,"Error Scann ${it.message}",Toast.LENGTH_SHORT).show()
            }
        }
        scannerview.setOnClickListener{
            codeScanner.startPreview()
        }
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
        if (requestCode==123){
            if (grantResults[0]==PackageManager.PERMISSION_GRANTED){
                Toast.makeText(this,"Camera Permition Grade",Toast.LENGTH_SHORT).show()
                startScan()
        }else{
            Toast.makeText(this,"Camera Permition Denied",Toast.LENGTH_SHORT).show()
        }
      }
    }

    override fun onResume() {
        super.onResume()
        if (:: codeScanner.isInitialized){
            codeScanner?.startPreview()
        }
    }

    override fun onPause() {
        if (:: codeScanner.isInitialized){
            codeScanner?.releaseResources()
        }
        super.onPause()
    }

    @Suppress("DEPRECATION")
    class postTokenDoc(private val context: Context,private val token:String):AsyncTask<String,String,String>(){
        override fun onPreExecute() {
            super.onPreExecute()
        }

        override fun doInBackground(vararg params: String?): String {
            var resoult=""
            try {
                var jsonObject=JSONObject()
                jsonObject.put("tokenDokumen",token)
                var jsonObjectString=jsonObject.toString()
                var httpURLConnection:HttpURLConnection?=null
                try {
                    var url=URL(BaseApi.BASEAPI+"api/QrValidation/")
                    httpURLConnection=url.openConnection() as HttpURLConnection
                    httpURLConnection.requestMethod="POST"
                    httpURLConnection.setRequestProperty("Content-Type","application/json")
                    httpURLConnection.setRequestProperty("Accept","text/plain")

                    var outputStream=httpURLConnection.outputStream
                    var outputStreamWriter=OutputStreamWriter(outputStream)
                    outputStreamWriter.write(jsonObjectString)
                    outputStreamWriter.flush()

                    var inputStream=httpURLConnection.inputStream
                    var inputStreamReader=InputStreamReader(`inputStream`)
                    var data=inputStreamReader.read()
                    Log.e("idDoc",data.toString())

                    while (data!=-1){
                        resoult +=data.toChar()
                        data=inputStreamReader.read()
                    }

                    if (httpURLConnection.responseCode==HttpURLConnection.HTTP_OK){
                        var jsonObject=JSONObject(resoult)
                        var id=jsonObject.getString("token")
                        GetId.getId=id
                        Log.e("idDoc",jsonObject.toString())
                        context.startActivity(Intent(context,ListDataActivity::class.java))
                    }
                }catch (ex:Exception){
                    Log.e("Error","Error $ex")
                }
            }catch (e:Exception){
                Log.e("Error","Error Http : $e")
            }
            return resoult
        }

        override fun onPostExecute(result: String?) {
            super.onPostExecute(result)
        }
    }
}