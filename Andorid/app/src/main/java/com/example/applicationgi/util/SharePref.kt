package com.example.applicationgi.util

import android.content.Context
import android.content.SharedPreferences

class SharePref constructor(context: Context){
    companion object{
        private const val PreftName="MyApp"
        private const val PreftToken="token"

        @Volatile
        private var  instance:SharePref?=null
        fun getInstance(context: Context):SharePref=
            instance?: synchronized(this){
                instance?: SharePref(context.applicationContext).also { instance=it }
            }
    }

    private val sharePref:SharedPreferences=
        context.getSharedPreferences(PreftName,Context.MODE_PRIVATE)

    fun setToken(token:String){
        sharePref.edit().putString(PreftToken,token).apply()
    }
    fun getToken():String?{
        return sharePref.getString(PreftToken,null)
    }
}