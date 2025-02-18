import { createBrowserRouter } from "react-router";
import Home from "../Pages/HomePage/Home";
import App from "../App";
import Login from "../Pages/LoginPage/Login";
import Register from "../Pages/RegisterPage/Register";
import OrganisationDashboard from "../Pages/OrganisationPage/OrganisationDashboard";
import ProtectedRoute from "./ProtectedRoute";

export const router = createBrowserRouter([
    {
        path:"/",
        element: <App/>,
        children:[
            { path:"",element:<Home/>},
            { path:"login",element:<Login/>},
            { path:"register",element:<Register/>},
            { path:"organisation-dashboard/:ticker",element:<ProtectedRoute><OrganisationDashboard/></ProtectedRoute>}
        ]
    }
]);