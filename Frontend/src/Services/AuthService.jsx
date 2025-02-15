import { handleError } from "../Helpers/ErrorHandler";
import axios from "axios";

const api = "http://localhost:5186/api"

export  const loginApi = async(username,password)=>{
    try{
        const data = await axios.post<UserProfileToken>(api+"account/login",{
            username:username,
            password:password,
        })
        return data;
    }catch(error){
        handleError(error)
    }
}

export  const registerApi = async(email,username,password)=>{
    try{
        const data = await axios.post<UserProfileToken>(api+"account/register",{
            email:email,
            username:username,
            password:password,
        })
        return data;
    }catch(error){
        handleError(error)
    }
}