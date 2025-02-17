import { createBrowserRouter } from "react-router";
import Home from "../Pages/HomePage/Home";
import Company from "../Pages/CompanyPage/Company";
import App from "../App";

export const router = createBrowserRouter([
    {
        path:"/",
        element: <App/>,
        children:[
            { path:"",element:<Home/>},
            { path:"company/:ticker",element:<Company/>}
        ]
    }
]);