import React, { useState, useEffect } from "react";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLoginState } from "../../_store/Login";
import { LoginFromProps } from "../../_types/Login";
import InputFieldComponent, { InputMode } from "../../components/common/InputField";
import Spinner from "../../components/spinners/Spinner";
import { useAuth } from "../../context/UseAuth";
import { useHelper } from "../../context/Helper";
import { Input } from 'reactstrap';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import { useNavigate, Link } from "react-router-dom";
import { toast } from "react-toastify";
import { BsEyeFill } from "react-icons/bs";
import { BsEyeSlashFill } from "react-icons/bs";


import JsonViewerComponent from "../../components/common/JsonViewerComponent";

const MySwal = withReactContent(Swal);

const Login = (props: any) => {
    const { loginUser } = useAuth();
    const navigate = useNavigate();
    const { getMessage } = useHelper();
    const isLoading = useLoginState((state) => state.isLoading);
    const setIsLoading = useLoginState((state) => state.setIsLoading);
    const validationData = useLoginState((state) => state.validationData);
    const clientLogin = useLoginState((state) => state.clientLogin);
    const [showPassword, setShowPassword] = useState(false);

    const methods = useForm<LoginFromProps>({
        mode: "onChange",
        resolver: yupResolver(validationData()),
        defaultValues: undefined,
    });
    const { formState: { errors }, getValues, watch, trigger } = methods;

    const onSubmit: SubmitHandler<LoginFromProps> = async (data) => {
        setIsLoading(true);
        await clientLogin(data.userName, data.password).then(async (res: ResponseProps) => {
            if (res.data?.code === 200) {
                if (res.data?.status === true) {
                    await loginUser(res);
                } else {
                    MySwal.fire({
                        icon: 'error',
                        text: getMessage(res),
                        confirmButtonText: 'ตกลง',
                        confirmButtonColor: '#3085d6',
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then((result) => {
                        if (result.isConfirmed) {
                        } else if (result.dismiss === Swal.DismissReason.cancel) {

                        }
                    });
                }
            } else {
                toast.error(getMessage(res));
            }
            setIsLoading(false);
        }).catch(error => {
            toast.error(JSON.stringify(error));
            setIsLoading(false);
        });
    };

    useEffect(() => {
        setIsLoading(true);
        var gettingData = true;
        if (gettingData) {
            setIsLoading(false);
        }
        return () => { gettingData = false };
    }, []);

    return (

        <FormProvider {...methods}>
            {isLoading ? <Spinner /> : (
                <section className="p-3 p-md-4 p-xl-5">
                    <div className="container">
                        <div className="card border-light-subtle shadow-sm">
                            <div className="row g-0">
                                <div className="col-12 col-md-6">
                                    <img
                                        className="img-fluid rounded-start w-100 h-100 object-fit-cover"
                                        loading="lazy"
                                        src="https://bootstrapbrain.com/demo/components/logins/login-4/assets/img/logo-img-1.webp"
                                        alt="BootstrapBrain Logo"
                                    />
                                </div>
                                <div className="col-12 col-md-6">
                                    <div className="card-body p-3 p-md-4 p-xl-5">
                                        <div className="row">
                                            <div className="col-12">
                                                <center >
                                                    <img src="https://klongtun-hospital.com/wp-content/uploads/thegem-logos/logo_176408cef83204b130ac281cb44bd1b2_2x.webp" width={200}></img>

                                                    <div className="mb-5 pt-5">
                                                        <h3>เข้าสู่ระบบ</h3>
                                                    </div></center>
                                            </div>
                                        </div>
                                        <form action="#!">
                                            <div className="row gy-3 gy-md-4 overflow-hidden">
                                                <div className="col-12">
                                                    <InputFieldComponent
                                                        name="userName"
                                                        label={'บัญชีผู้ใช้งาน'}
                                                        mode={InputMode.secondary}
                                                        labelAlignClassName="text-left"
                                                        labelClassName="form-label"
                                                        inputClassName="form-control"
                                                        renderControl={field => (
                                                            <Input
                                                                type={'tel'}
                                                                {...field}
                                                                autoComplete="off"
                                                            />
                                                        )}
                                                    />
                                                </div>
                                                <div className="col-12">
                                                    <InputFieldComponent
                                                        name="password"
                                                        label={'รหัสผ่าน'}
                                                        mode={InputMode.secondary}
                                                        labelAlignClassName="text-left"
                                                        labelClassName="form-label"
                                                        inputClassName="form-control"
                                                        renderControl={field => (
                                                            <div className="input-group">
                                                                <Input
                                                                    type={showPassword ? 'text' : 'password'} // Toggle between text and password
                                                                    {...field}
                                                                    autoComplete="off"
                                                                />
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-outline-secondary"
                                                                    onClick={() => setShowPassword(!showPassword)} // Toggle the visibility
                                                                >
                                                                    {showPassword ? <BsEyeSlashFill /> : <BsEyeFill />}
                                                                </button>
                                                            </div>
                                                        )}
                                                    />
                                                </div>
                                                <div className="col-12">
                                                    <div className="d-grid">
                                                        <button
                                                            type="submit"
                                                            className="btn btn-primary"
                                                            onClick={async () => {
                                                                const isValid = await trigger();
                                                                if (isValid) {
                                                                    onSubmit(getValues());
                                                                }
                                                            }}
                                                        >
                                                            เข้าสู่ระบบ
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </form>
                                        {/* <div className="row">
                                            <div className="col-12">
                                                <hr className="mt-5 mb-4 border-secondary-subtle" />
                                                <div className="d-flex gap-2 gap-md-4 flex-column flex-md-row justify-content-md-end">
                                                    <a href="#!" className="link-secondary text-decoration-none">Create new account</a>
                                                    <a href="#!" className="link-secondary text-decoration-none">Forgot password</a>
                                                </div>
                                            </div>
                                        </div> */}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            )}
            <JsonViewerComponent data={watch()} />
            <JsonViewerComponent data={errors} />
        </FormProvider>

    );
};

export default Login;