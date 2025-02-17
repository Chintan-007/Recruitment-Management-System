import React, { createContext, use, useEffect, useState } from "react";
import { UserProfile } from "../Models/User";
import { useNavigate } from "react-router-dom";
import { loginApi, registerApi } from "../Services/AuthService";
import { toast } from "react-toastify";
import axios from "axios";

let UserContextType = {
    user:UserProfile | null,
    token:String|null,
    registerUser:(email,username,password) => {},
    loginUser:(username,password)=> {},
    logOut: ()=>{},
    isLoggedIn: ()=>{}
}

let Props = {children : React.ReactNode};

const UserContext = createContext<UserContextType>({});

export const UserProvider = ({children}) =>{

    const navigate  = useNavigate();
    const [token,setToken] =  useState<String|null>(null);
    const [user,setUser] = useState<UserProfile|null>(null);
    const [isReady,setIsReady] = useState(false);

    useEffect(()=>{
        const user = localStorage.getItem("user");
        const token =  localStorage.getItem("token");
        if(user && token){
            setUser(JSON.parse(use));
            setToken(JSON.parse(token));
            axios.defaults.headers.common["Authorization"] = "Bearer"+token;
        }
        setIsReady(true);
    },[]);

    const registerUser = async(email,username,password)=>{
        await registerApi(email,username,password).then((res)=>{
            if(res){
                localStorage.setItem("token",res?.data.token);
                const userObj = {
                    username:res?.data.username,
                    email:res?.data.email,
                }
                localStorage.setItem("user",JSON.stringify(userObj));
                setToken(res?.data.token)
                setUser(userObj)
                toast.success("Login sucessfull!");
                navigate("/search");
            }
        }).catch(e=>toast.warning("Server error occured"));
    }

    const loginUser = async(username,password)=>{
        await loginApi(username,password).then((res)=>{
            if(res){
                localStorage.setItem("token",res?.data.token);
                const userObj = {
                    username:res?.data.username,
                    email:res?.data.email,
                }
                localStorage.setItem("user",JSON.stringify(userObj));
                setToken(res?.data.token)
                setUser(userObj)
                toast.success("Login sucessfull!");
                navigate("/search");
            }
        }).catch(e=>toast.warning("Server error occured"));
    }

    const isLoggedIn = ()=>{
        return !!user;
    }

    const logout = ()=>{
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        setUser(null);
        setToken("");
        navigate("/");
    }

    return(
        <UserContext.Provider value = {{loginUser,user,token,logout,isLoggedIn, registerUser}}>
            {isReady ? children:null}
        </UserContext.Provider>
    )
}

export const useAuth = () => React.useContext(UserContext);