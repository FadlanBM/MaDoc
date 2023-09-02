package com.example.applicationgi.Adapter

import android.content.Context
import android.provider.ContactsContract.RawContacts.Data
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.activity.R
import androidx.appcompat.view.menu.ActionMenuItemView
import androidx.recyclerview.widget.RecyclerView
import com.example.applicationgi.ListDataActivity

class DocAdapter(private val items: List<MyHolder>):RecyclerView.Adapter<DocAdapter.ViewHolder>(){

    class ViewHolder(itemView: View):RecyclerView.ViewHolder(itemView) {
        val nameTextView:TextView=itemView.findViewById(com.example.applicationgi.R.id.lb_nameDoc)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view=LayoutInflater.from(parent.context).inflate(com.example.applicationgi.R.layout.adapter_doc,parent,false)
        return ViewHolder(view)
    }

    override fun getItemCount(): Int {
        return items.size
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item=items[position]
        holder.nameTextView.text=item.nameDoc
    }

}