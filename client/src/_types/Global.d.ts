declare global {
  interface OptionProps {
    value: string;
    label: any;
  }

  interface OptionMultiLanguageProps {
    value: string;
    labelTh: string;
    labelEn: string;
  }

  interface ResponseProps {
    data?: DataResponseProps;
  }
}

interface DataResponseProps {
  code: number;
  message: string;
  output: OutputResponseProps;
  status: boolean;
}
interface OutputResponseProps {
  data: any;
  messageAlert: MessageAlertResponseProps;
}
interface MessageAlertResponseProps {
  en: string;
  th: string;
}

export { };
