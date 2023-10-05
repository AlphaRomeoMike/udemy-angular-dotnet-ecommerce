package com.example.client_mobile.interfaces;

import com.example.client_mobile.models.ProductList;

import retrofit2.Call;
import retrofit2.http.GET;

public interface API {
    String BASE_URL = "http://localhost:5001/api";
    @GET("products")
    Call<ProductList> getProducts();
}
