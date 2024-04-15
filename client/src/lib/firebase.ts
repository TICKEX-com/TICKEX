// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";


const firebaseConfig = {
  apiKey: "AIzaSyAbMWWFUpsP-f2gUQiTHah2SwKf3WDTIW4",
  authDomain: "tickex-20fa7.firebaseapp.com",
  projectId: "tickex-20fa7",
  storageBucket: "tickex-20fa7.appspot.com",
  messagingSenderId: "1007750788594",
  appId: "1:1007750788594:web:3dd2ce1111c1925abf676b",
  measurementId: "G-YRYFTCK3NT"
};


const app = initializeApp(firebaseConfig);
export const storage = getStorage(app);