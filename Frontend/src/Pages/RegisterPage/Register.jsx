import React, { useState, useEffect } from 'react';
import { TextField, Button, Grid, MenuItem, InputLabel, Select, FormControl, FormHelperText } from '@mui/material';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useAuth } from '../../Context/useAuth';

// Validation schema using Yup
const validationSchema = Yup.object().shape({
    firstname: Yup.string().required("Firstname is required"),
    lastname: Yup.string().required("Lastname is required"),
    username: Yup.string().required("Username is required"),
    addressLine1: Yup.string().required("AddressLine1 is required"),
    organisationType: Yup.string().required("Organisation type is required"),
    email: Yup.string().required("Email is required").email("Enter a valid email"),
    password: Yup.string().required("Password is required"),
    contactNumber: Yup.string().required("Contact number is required")
        .matches(/^[0-9]{10}$/, "Contact number must be 10 digits"),
    about: Yup.string().required("About is required").min(10, "About should be at least 10 characters")
});

function Register() {
    const { registerOrganisation } = useAuth();
    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(validationSchema),
    });

    const [organisationTypes, setOrganisationTypes] = useState([]);  // State to hold organisation types
    const [loading, setLoading] = useState(true);  // State to manage loading state
    const [selectedOrganisation, setSelectedOrganisation] = useState({ id: '', name: '' }); // State for selected organisation

    useEffect(() => {
        const fetchOrganisationTypes = async () => {
            try {
                const response = await fetch('http://localhost:5186/api/Type/organisation-types');  
                const data = await response.json();
                setOrganisationTypes(data);  // Update the state with the fetched data
                setLoading(false);  // Set loading to false once data is fetched
            } catch (error) {
                console.error("Error fetching organisation types:", error);
                setLoading(false);
            }
        };

        fetchOrganisationTypes();
    }, []);

    const handleRegister = (form) => {
        registerOrganisation(form.firstname,form.lastname,form.username,form.email,form.contactNumber,form.addressLine1,form.addressLine2,form.about,form.password,selectedOrganisation.id);
    };

     // Handle organisation type change
     const handleOrganisationTypeChange = (event) => {
        const selectedType = event.target.value;
        const organisation = organisationTypes.find(item => item.organisationType === selectedType);
        console.log(organisation);
        setSelectedOrganisation({ id: organisation.id, name: selectedType });
    };

    return (
        <section style={{ backgroundColor: '#f5f5f5', padding: '30px' }}>
            <div style={{ maxWidth: '600px', margin: 'auto', backgroundColor: 'white', padding: '20px', borderRadius: '8px', boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)' }}>
                <h2 style={{ textAlign: 'center', marginBottom: '20px' }}>Create Your Account</h2>
                <form onSubmit={handleSubmit(handleRegister)}>

                    {/* First Name and Last Name in Single Row */}
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                fullWidth
                                label="Firstname"
                                variant="outlined"
                                id="firstname"
                                {...register("firstname")}
                                error={Boolean(errors.firstname)}
                                helperText={errors.firstname ? errors.firstname.message : ''}
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                fullWidth
                                label="Lastname"
                                variant="outlined"
                                id="lastname"
                                {...register("lastname")}
                                error={Boolean(errors.lastname)}
                                helperText={errors.lastname ? errors.lastname.message : ''}
                            />
                        </Grid>
                    </Grid>

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

                    {/* Email */}
                    <TextField
                        fullWidth
                        label="Email"
                        variant="outlined"
                        id="email"
                        {...register("email")}
                        error={Boolean(errors.email)}
                        helperText={errors.email ? errors.email.message : ''}
                        margin="normal"
                    />

                    {/* Address Line 1 */}
                    <TextField
                        fullWidth
                        label="Address Line 1"
                        variant="outlined"
                        id="addressLine1"
                        {...register("addressLine1")}
                        error={Boolean(errors.addressLine1)}
                        helperText={errors.addressLine1 ? errors.addressLine1.message : ''}
                        margin="normal"
                    />

                    {/* Address Line 2 */}
                    <TextField
                        fullWidth
                        label="Address Line 2"
                        variant="outlined"
                        id="addressLine2"
                        {...register("addressLine2")}
                        margin="normal"
                    />

                    {/* Organisation Type Dropdown */}
                    <FormControl fullWidth margin="normal" error={Boolean(errors.organisationType)}>
                        <InputLabel required>Organisation Type</InputLabel>
                        <Select
                            label="Organisation Type"
                            id="organisationType"
                            {...register("organisationType")}
                            value={selectedOrganisation.name}  // Controlled value
                            onChange={handleOrganisationTypeChange}  // Handle change
                            disabled={loading}  // Disable if loading
                        >
                            {loading ? (
                                <MenuItem value="">Loading...</MenuItem>  // Show loading text while fetching
                            ) : (
                                organisationTypes.map((type) => (
                                    <MenuItem key={type.id} value={type.organisationType}>
                                        {type.organisationType}
                                    </MenuItem>
                                ))
                            )}
                        </Select>
                        <FormHelperText>{errors.organisationType ? errors.organisationType.message : ''}</FormHelperText>
                    </FormControl>

                    {/* Contact Number */}
                    <TextField
                        fullWidth
                        label="Contact Number"
                        variant="outlined"
                        id="contactNumber"
                        {...register("contactNumber")}
                        error={Boolean(errors.contactNumber)}
                        helperText={errors.contactNumber ? errors.contactNumber.message : ''}
                        margin="normal"
                    />

                    {/* About */}
                    <TextField
                        fullWidth
                        label="About"
                        variant="outlined"
                        id="about"
                        {...register("about")}
                        error={Boolean(errors.about)}
                        helperText={errors.about ? errors.about.message : ''}
                        margin="normal"
                        multiline
                        rows={4}
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

                    {/* Register Button */}
                    <Button 
                        type="submit" 
                        variant="contained" 
                        color="primary" 
                        fullWidth
                        style={{ marginTop: '20px' }}
                    >
                        Register
                    </Button>

                    {/* Already have an account */}
                    <p style={{ textAlign: 'center', marginTop: '20px' }}>
                        Already have an account?{' '}
                        <a href="#" style={{ textDecoration: 'underline', color: '#1976d2' }}>
                            Sign In
                        </a>
                    </p>
                </form>
            </div>
        </section>
    );
}

export default Register;
