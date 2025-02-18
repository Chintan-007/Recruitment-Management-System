import React from 'react'
import { Navigate, useLocation } from 'react-router'

function ProtectedRoute({children}) {
    const location = useLocation();
    const {isLoggedIn} = useAuth();
  return isLoggedIn ? (
    <>{children}</>
  ):(
    <Navigate to="/login" state={{from:location}} replace/>
  )
}

export default ProtectedRoute