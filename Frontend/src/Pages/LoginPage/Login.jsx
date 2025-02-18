import React from 'react';
import { TextField, Button, Grid, Link } from '@mui/material';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useAuth } from '../../Context/useAuth';

// Validation schema using Yup
const validationSchema = Yup.object().shape({
  username: Yup.string().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

function Login() {
  const { loginUser } = useAuth();
  const { register, handleSubmit, formState: { errors } } = useForm({
    resolver: yupResolver(validationSchema),
  });

  const handleLogin = (form) => {
    loginUser(form.username, form.password);
  };

  return (
    <section style={{ backgroundColor: '#f5f5f5', padding: '30px' }}>
      <div style={{ maxWidth: '600px', margin: 'auto', backgroundColor: 'white', padding: '20px', borderRadius: '8px', boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)' }}>
        <h2 style={{ textAlign: 'center', marginBottom: '20px' }}>Sign in to your account</h2>
        <form onSubmit={handleSubmit(handleLogin)}>

          {/* Username */}
          <TextField
            fullWidth
            label="Username"
            variant="outlined"
            id="username"
            {...register("username")}
            error={Boolean(errors.username)}
            helperText={errors.username ? errors.username.message : ''}
            margin="normal"
          />

          {/* Password */}
          <TextField
            fullWidth
            label="Password"
            variant="outlined"
            type="password"
            id="password"
            {...register("password")}
            error={Boolean(errors.password)}
            helperText={errors.password ? errors.password.message : ''}
            margin="normal"
          />

          {/* Forgot Password Link */}
          <div style={{ textAlign: 'right', marginTop: '10px' }}>
            <Link href="#" variant="body2" color="primary">
              Forgot password?
            </Link>
          </div>

          {/* Login Button */}
          <Button 
            type="submit" 
            variant="contained" 
            color="primary" 
            fullWidth
            style={{ marginTop: '20px' }}
          >
            Sign In
          </Button>

          {/* Sign-up Link */}
          <p style={{ textAlign: 'center', marginTop: '20px' }}>
            Don’t have an account?{' '}
            <Link href="#" variant="body2" color="primary">
              Sign up
            </Link>
          </p>
        </form>
      </div>
    </section>
  );
}

export default Login;
