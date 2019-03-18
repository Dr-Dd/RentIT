from werkzeug.security import generate_password_hash, check_password_hash

password = "canevolante"
salt = "1234567890Abc"
pass_hash = generate_password_hash(password)
print(pass_hash)
print(check_password_hash(password,pass_hash))