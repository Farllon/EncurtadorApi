from pymongo import MongoClient
from itertools import permutations, combinations_with_replacement  

def Convert(string): 
    list1=[] 
    list1[:0]=string 
    return list1

client = MongoClient("mongodb://root:123@localhost:27017")

db = client.UrlshortnerDb

characters = Convert("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
hash_size = 5

for _ in range(pow(len(characters), hash_size)):
    for a in characters:
        for b in characters:
            for c in characters:
                for d in characters:
                    for e in characters:
                        hash = {
                            "HashCode": str(f"{a}{b}{c}{d}{e}"),
                            "IsAvaible": True
                        }

                        db.Hashes.insert_one(hash)
