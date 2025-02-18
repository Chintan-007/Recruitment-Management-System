import { handleError } from "../Helpers/ErrorHandler";
import axios from "axios";

const api = "http://localhost:5186/api/";

export const loginApi = async (username, password) => {
  try {
    const data = await axios.post(api + "Account/login", {
      name : username,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerOrgnisationApi = async (firstname,lastname,username,email,contact,addressLine1,addressLine2,about,password,organisationTypeId) => {
  try {
    const data = await axios.post(api + "Account/register-organisation", {
        firstName:firstname ,
        lastName:lastname,
        userName: username,
        email: email,
        contact:contact,
        addressLine1:addressLine1 ,
        addressLine2:addressLine2 ,
        about: about,
        password:password ,
        organisationTypeId: organisationTypeId
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};
