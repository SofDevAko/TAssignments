# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.core.serializers import serialize
from django.http import HttpResponse
from django.shortcuts import render, redirect
import re, bcrypt, json
from .models import User
from django.contrib import messages
from time import gmtime, strftime
from datetime import datetime

def index(request): 
    return render(request, 'index.html') 

def get(request):
    page = int(request.body[5])
    first = (-1+page)*5
    last = page*5
    users = []
    all_users = User.objects.order_by('id')
    # print(all_users)
    if len(all_users) > last:
        for i in range(first,last):
            users.append({
                'id' : all_users[i].id,
                'first_name' : all_users[i].first_name,
                'last_name' : all_users[i].last_name,
                'created_at' :  all_users[i].created_at.strftime('%Y-%m-%d'),
                'email' : all_users[i].email
            })
    else:
        for i in range(first,len(all_users)):
            users.append({
                'id' : all_users[i].id,
                'first_name' : all_users[i].first_name,
                'last_name' : all_users[i].last_name,
                'created_at' :  all_users[i].created_at.strftime('%Y-%m-%d'),
                'email' : all_users[i].email
            })
    # print(users)
    json_users = json.dumps(users)
    return HttpResponse(json_users)

def create(request):
    errors = User.objects.register_validator(request.POST)
    if len(errors):
        return HttpResponse(json.dumps({'response': errors}))
    else:
        time = strftime("%H:%M %p %Y-%m-%d", gmtime())
        User.objects.create(
            first_name = request.POST['first_name'],
            last_name = request.POST['last_name'],
            email = request.POST['email'],
            created_at = time,
            updated_at = time,
        )
        return HttpResponse(json.dumps({'response':"You have successfully registered!"}))