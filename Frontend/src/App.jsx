import { useState } from 'react'
import "react-toastify/dist/ReactToastify.css"
import './App.css'
import { ToastContainer } from 'react-toastify'
import { UserProvider } from './Context/useAuth'
import { Outlet } from 'react-router'
import Navbar from './Componants/Navbar/Navbar'

function App() {

  return (
    <>
      {/* <UserProvider>
        <ToastContainer/>
      </UserProvider> */}
      <Navbar/>
      <Outlet/>
    </>
  )
}

export default App;
