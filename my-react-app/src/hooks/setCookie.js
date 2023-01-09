import Cookies from "js-cookie";

const setCookie = (cookiename, ursin)=>{
    Cookies.set(cookiename,ursin,{
        expires:1,
        secure:true,
        sameSite: 'strict',
        path:'/'
    });
};
export default setCookie;