import React, { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginApi, registerOrgnisationApi } from "../Services/AuthService";
import { toast } from "react-toastify";
import axios from "axios";

// Create context with default values
const UserContext = createContext({
  user: null,
  token: null,
  registerOrgnisation: (firstname,lastname,username,addressLine1, addressLine2,orgnaisationType,email, password) => {},
  loginUser: (username, password) => {},
  logOut: () => {},
  isLoggedIn: () => {},
});

export const UserProvider = ({ children }) => {
  const navigate = useNavigate();
  const [token, setToken] = useState(null);
  const [user, setUser] = useState(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    const storedToken = localStorage.getItem("token");
    if (storedUser && storedToken) {
      setUser(JSON.parse(storedUser));
      setToken(storedToken);
      axios.defaults.headers.common["Authorization"] = "Bearer " + storedToken;
    }
    setIsReady(true);
  }, []);

  const registerOrganisation = async (firstname,lastname,username,email,contact,addressLine1,addressLine2,about,password,organisationTypeId) => {
    try {
      const res = await registerOrgnisationApi(firstname,lastname,username,email,contact,addressLine1,addressLine2,about,password,organisationTypeId);
      if (res?.data?.token) {
        localStorage.setItem("token", res?.data.token);
        const userObj = {
          username: res?.data.username,
          email: res?.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res?.data.token);
        setUser(userObj);
        toast.success("Registration successful!");
        navigate(`/organisation-dashboard/${organisationTypeId}`);
      }
    } catch (e) {
      toast.warning("Server error occurred");
    }
  };

  const loginUser = async (username, password) => {
    try {
      const res = await loginApi(username, password);
      if (res?.data?.token) {
        localStorage.setItem("token", res?.data.token);
        const userObj = {
          username: res?.data.username,
          email: res?.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res?.data.token);
        setUser(userObj);
        toast.success("Login successful!");
        navigate("/dashboard");
      }
    } catch (e) {
      toast.warning("Server error occurred");
    }
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken(null);
    navigate("/");
  };

  return (
    <UserContext.Provider value={{ loginUser, user, token, logout, isLoggedIn, registerOrganisation }}>
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

// Custom hook for consuming the UserContext
export const useAuth = () => {
  const context = React.useContext(UserContext);
  if (!context) {
    throw new Error("useAuth must be used within a UserProvider");
  }
  return context;
};
