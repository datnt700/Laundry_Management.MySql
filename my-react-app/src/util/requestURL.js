import axios from "axios";
import getCookie from "../hooks/getCookie";
import removeCookie from "../hooks/removeCookie";


 const instance = axios.create({
    baseURL: process.env.REACT_APP_URL_API,
    timeout: 300000,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    }
});

instance.interceptors.request.use(req=>{

    req.headers.Authorization = `${getCookie("token")}`;
    return req;
})

instance.interceptors.response.use(
    
    (response) => {
        console.log("interceptors",response);
        return response.data
    },
    (error)=>{
        console.log("e_errr",error);
    }
);


// instance.interceptors.request.use(
//     config => {
//         config.headers = { 
//             'Authorization': `${getCookie("token")}`,
//             'Accept': 'application/json',
//             'Content-Type': 'application/x-www-form-urlencoded'
//           }
//         return config;
//     },
//     error => {
//         return Promise.reject(error);
//     }
// );

const CheckStatus = (response) =>{
    
    if(response && response.status === 200)
        return response.data;

    return null;
}



export const AxiosGet =async (path,params) =>{
    return await instance
    .get(path,{params})
    .then(CheckStatus)
    .catch(err=>{
        console.log(err)
    })
}                                                                                              

export const AxiosPost = async(path,body) =>{
    return await instance
    .post(path,body)
    .then(CheckStatus).catch(err=>{
        console.log(err)
    })
}

export const AxiosPut = async(path,body) =>{
    return await instance
    .put(path,body)
    .then(CheckStatus).catch(err=>{
        console.log(err)
    })
}

export const AxiosDelete =async(path,body) =>{
    return await instance
    .delete(path,body)
    .then(CheckStatus).catch(err=>{
        console.log(err)
    })
}


export function saveTokenInLocalStorage(tokentDetails) {
    localStorage.setItem('userDetails', JSON.stringify(tokentDetails));
}


export default instance;