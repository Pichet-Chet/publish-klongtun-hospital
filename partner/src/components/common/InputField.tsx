import { FC } from "react";
import { Controller, ControllerRenderProps, useFormContext } from "react-hook-form";
import _ from "lodash";
import { Label } from 'reactstrap';

type IFormInputProps = {
    name: string;
    validationName?: string;
    label?: string;
    renderControl: (field: ControllerRenderProps) => React.ReactNode;
    mode?: InputMode;
    layoutClassName?: string | undefined;
    layoutErrorClassName?: string | undefined;
    labelAlignClassName?: string | undefined;
    labelClassName?: string | undefined;
    secondaryLabelClassName?: string | undefined;
    inputClassName?: string | undefined;
    require?: boolean;
    tooltip?: string;
};

export enum InputMode {
    inline,
    secondary,
    nostyle,
}

const InputFieldComponent: FC<IFormInputProps> = ({
    renderControl,
    mode = InputMode.inline,
    name,
    validationName,
    label,
    labelAlignClassName = "text-right",
    layoutClassName = "md:flex md:items-baseline",
    layoutErrorClassName = "md:flex md:items-baseline mb-3",
    labelClassName = "md:w-1/3",
    inputClassName = "md:w-2/3",
    secondaryLabelClassName = "",
    require = false,
}) => {
    const {
        control,
        formState: { errors },
    } = useFormContext();
    const _validationName = validationName ? validationName : name;
    const _errors = _.get(errors, _validationName, undefined);

    return (
        <Controller
            control={control}
            name={name}
            defaultValue=""
            render={({ field }) =>
                mode === InputMode.inline ? (
                    <>
                        <div className={layoutClassName}>
                            {label && (
                                <div className={labelClassName}>
                                    <Label htmlFor={name} className={`text-sm block mb-1 mb-0 ${labelAlignClassName}`}>
                                        {`${label} `}
                                        {require && <span className="text-danger">*</span>}
                                    </Label>
                                </div>
                            )}
                            <div className={inputClassName}>{renderControl(field)}</div>
                        </div>
                        <div className={`${layoutErrorClassName}`}>
                            {label && <div className={labelClassName}></div>}
                            <div className={inputClassName}>
                                {_errors && (
                                    <Label className="text-sm text-danger">
                                        {_errors ? _errors?.message?.toString() : ""}
                                    </Label>
                                )}
                            </div>
                        </div>
                    </>
                ) : mode === InputMode.secondary ? (
                    <div className="mb-3">
                        {
                            label && (
                                <Label htmlFor={name} className={`text-sm block mb-1 ${labelAlignClassName}`}>
                                    {`${label} `}
                                    {require && <span className="text-danger">*</span>}
                                </Label>
                            )
                        }
                        {renderControl(field)}
                        {_errors && (
                            <Label className="text-sm text-danger">
                                {_errors ? _errors?.message?.toString() : ""}
                            </Label>
                        )}
                    </div>
                ) : <>{renderControl(field)}</>
            }
        />
    );
};

export default InputFieldComponent;
