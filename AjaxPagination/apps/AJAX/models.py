# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models

import re, bcrypt
EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
NAME_REGEX = re.compile(r'^[a-zA-Z]+$')
USER_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+$')

class UserManager(models.Manager):

    def register_validator(self, data):
        errors = []
        if User.objects.filter(email=data['email']):
            errors.append('This email address is already registered')
        if len(data['email']) < 1  or len(data['first_name']) < 1 or len(data['last_name']) < 1:
            errors.append('Please fill all the fields!')
        if len(data['email']) > 1 and not EMAIL_REGEX.match(data['email']):
            errors.append("Invalid Email Address!")
        if len(data['first_name']) > 1 and not NAME_REGEX.match(data["first_name"]):
            errors.append("Your first name should contain letters only!")
        if len(data['last_name']) > 1 and not NAME_REGEX.match(data["last_name"]):
            errors.append("Your last name should contain letters only!")
        return errors

class User(models.Model):
    first_name = models.CharField(max_length=255)
    last_name = models.CharField(max_length=255)
    email = models.CharField(max_length=255)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    
    objects = UserManager()
