package com.example.client_mobile;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.ListView;
import android.widget.ScrollView;

import com.example.client_mobile.adapters.RetrofitClient;
import com.example.client_mobile.models.ProductList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    ScrollView products;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        products = findViewById(R.id.products);
    }

    private void getProducts() {
        Call<ProductList> call = RetrofitClient.getInstance().getMyApi().getProducts();
        call.enqueue(new Callback<ProductList>() {
            @Override
            public void onResponse(@NonNull Call<ProductList> call, @NonNull Response<ProductList> response) {
                ProductList products_response = response.body();
            }

            @Override
            public void onFailure(Call<ProductList> call, Throwable t) {

            }
        });
    }
}