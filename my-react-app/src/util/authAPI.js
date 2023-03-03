import instance from "./requestURL";
import { AxiosDelete } from "./requestURL";


const autAPI ={
    login : (phone,password) => {
        const url ='Auth/login';
        return instance.post(url,{phone,password});
    },

    register : (username,phone,password) => {
        const url ='Auth/register';
        return instance.post(url,{username,phone,password});
    },

    machine : () => {
        
        const url = 'Machine/GetList';
        return instance.get(url);
    },
    
    machineId : (id) => {
        
        const url = `Machine/${id}`;
        return instance.get(url,id);
    },

    machineAdd : (MachineName, MachineType,Branch, Size, Status) => {
        const url = 'Machine/AddMachine';
        return instance.post(url,{MachineName, MachineType,Branch, Size, Status});
    },

    machineUpdate : (id,MachineName, MachineType,Branch, Size, Status) => {
        const url = 'Machine/UpdateMachine';
        return instance.put(url,{id,MachineName, MachineType,Branch, Size, Status});
    },

    machineDelete : (id) => { 
        const url = `Machine/${id}`;
        return instance.delete(url,id);
    },



}

export default autAPI;