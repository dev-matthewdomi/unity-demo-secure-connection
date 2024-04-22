using Unity.Collections;

namespace Code.SecureDriver
{
    public static class SecureParameters
    {
        /// <summary>
        /// The common name used to define the server certificate
        /// </summary>
        public static FixedString512Bytes ServerCommonName = new FixedString512Bytes("hello_netcode_secure");

        /// <summary>
        /// Game client certificate
        /// </summary>
        public static FixedString4096Bytes GameClientCA = new FixedString4096Bytes(
@"-----BEGIN CERTIFICATE-----
MIICujCCAaICCQDUBNxxYwO01jANBgkqhkiG9w0BAQsFADAfMR0wGwYDVQQDDBRo
ZWxsb19uZXRjb2RlX3NlY3VyZTAeFw0yNDA0MTYxNDQ3MDFaFw0yNzA0MTYxNDQ3
MDFaMB8xHTAbBgNVBAMMFGhlbGxvX25ldGNvZGVfc2VjdXJlMIIBIjANBgkqhkiG
9w0BAQEFAAOCAQ8AMIIBCgKCAQEAx3U0ZPyZiV+VBMEDCAWUpbzKatlfXDtfsIEk
Feu4M/Q3Q/h8aU+asLMR8DtJQS/mhOXSGyYmfRGP4FpdsnAUPnbWwmEzvNetXJiY
UxIk2/O0ZZ+Dd/jn5ec67Me1fcwH70VmO0Puyzh28Hak/Q9GvMXWTzP6zkOl4Rbt
HXM/qyIOE0BqKt7Dg4wjOh5+xbExJGpKms338ZkZMgieOh9awpx1VY6tzT96KPqG
i4vTdeuEKOeL0COTWsPhIYpB2Gr2LYede6FuTkF78oYB/W+pLsO/VdBE5YrjYMiZ
OueMn6viQCyA0y37PIdIU90cXygqquRt95M/jTQljgk/pUHNIwIDAQABMA0GCSqG
SIb3DQEBCwUAA4IBAQCuhsl7DLIPn4BPl17Q1o17GD4mm4InFjJ6uvvbciigIPAx
CrqZmyjjlRAuLeCpi3eeRsiteRWHs28IL8LPMF5Yp1JmC6Q0raswaByp1aA/j2nT
km/rGJ585XE2U0U+wmGw9RHvfSFPm5ztIpzaiwrW6Zor5cwD8vsCTup2WPO/sHHH
CHOhap1LQF1pQJDPp2E/RUia2U+2fjzy+gNB0xy4WnzKGG19P+cfw9kMOWXPBy0E
8aF21YKZYpIDM7sEC1cmzsidLNnuaRTHSkHxQ37sHFF5zSjW4Due9GtzFRdSyk98
kXd4xpgdcpOJ7WkRk5y5ZNg4dWrbHuEnB02A4Uhl
-----END CERTIFICATE-----");

        /// <summary>
        /// Server certificate
        /// </summary>
        public static FixedString4096Bytes GameServerCertificate = new FixedString4096Bytes(
@"-----BEGIN CERTIFICATE-----
MIICujCCAaICCQDKr6eE94JKYzANBgkqhkiG9w0BAQsFADAfMR0wGwYDVQQDDBRo
ZWxsb19uZXRjb2RlX3NlY3VyZTAeFw0yNDA0MTYxNDQ5MTFaFw0yNTA0MTYxNDQ5
MTFaMB8xHTAbBgNVBAMMFGhlbGxvX25ldGNvZGVfc2VjdXJlMIIBIjANBgkqhkiG
9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvcB8ESNmhZKktiMlo1tTjhl81UcJdvq7+Wfu
b7ORfFoT7XcJaLZ+s88jdvTAu5KYb9yiCcjPz6uaqErabYtIna1v3g4qV/2D8J94
qDMFWbb87Qa5Igy0yK72CW/sDK+MtwxWRJ//+k0mEOpjlSrFX7Yuz2kytiRZiOIq
+IOdIA8sogXaQvPm4Oz9rUOqSws8JYGnm9ZPfB0JQLCi4wC4vQ4ZB3SB5AluS0Sr
s/lo/rPNBX9QAyQzqD/6QiP0TkW+Eh1cQxXPMijE3683qKFRJubkLDGn1RCoetEe
+0t8LdHam+TUZG5CCHR6D/JxKPJpRDu/IEsBOcthxB+Rv68e0QIDAQABMA0GCSqG
SIb3DQEBCwUAA4IBAQBk/AsEM4X5HCWDYR898u346ykcPPxFpnlMLGvoHvirV4WM
1BMbsFem0ShHzD8Spg5G3tSpYYQyt01g3g2iSF/f4Td7f+4Q6lPHV21JSVd7W7uA
/g7VWwCG1HaGHBAOdaKZWEOpoytnTBtk12t5EAuWk+znhiCqcypC3zRbhBoHpI56
+JHP8NKLhtDGZprvBBNBY5j8NMlZrHhQIolsDVtPQnJE/EZMQf+3Zi8PCXUOKtEi
tcjfMw/fQRO3yvHisrxd0g/KaYErKvrLe2dUgvkD8pU8X85s5wotAdiIVRcmWmDM
Ajn7cOEJZJX6Wnup0lUNpa5DesExl7YkY9EaF4Sk
-----END CERTIFICATE-----");

        /// <summary>
        /// Server private key
        /// </summary>
        public static FixedString4096Bytes GameServerPrivate = new FixedString4096Bytes(
@"-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEAvcB8ESNmhZKktiMlo1tTjhl81UcJdvq7+Wfub7ORfFoT7XcJ
aLZ+s88jdvTAu5KYb9yiCcjPz6uaqErabYtIna1v3g4qV/2D8J94qDMFWbb87Qa5
Igy0yK72CW/sDK+MtwxWRJ//+k0mEOpjlSrFX7Yuz2kytiRZiOIq+IOdIA8sogXa
QvPm4Oz9rUOqSws8JYGnm9ZPfB0JQLCi4wC4vQ4ZB3SB5AluS0Srs/lo/rPNBX9Q
AyQzqD/6QiP0TkW+Eh1cQxXPMijE3683qKFRJubkLDGn1RCoetEe+0t8LdHam+TU
ZG5CCHR6D/JxKPJpRDu/IEsBOcthxB+Rv68e0QIDAQABAoIBABHMMxbcbipLJd3b
kBUxZLXoWBgdEJszS1xKTkf13MiAHmxghOZob5vn6timfklZp6ieVih6yFsfKmNs
me46aTY45Uw7oecc5To1ivijyHWwvypwPf8el/pWxsb903MhKB6nLpRDOZw9jjt5
8Js2JssiaGOV52bEJA29wPAMUDmIaWEc5pmofv+lm02YRBCGxR+cFMhXjqULQk9Y
1oLzzZq844GsxdVovt3qc9vwyFOV2CvL+VSPsJ7HK3pYadUSwgdUfH+xeZFJy23z
dgtNvEU8aYyM3SCbFMPM9i1lY1CDK6PK5Pe/p/XrwSqVTmYJZDUEzvSbyRacHC49
MHar3tECgYEA7KYdf2EVvWxPcrZhmvUwX+gglOBtgjfQPKO0hyjJ5kHw7elGQdcb
KFiMpUe0SjIglGdSHvhFj+UvQ4fL4an55wfLkisSegfdydUIR6V9SRG6GmDT16j+
OeOEIHPIH6ruhkiyklrrsQHFD+qytgKDBKKj+ZJooXIfjSL93eUu6nUCgYEAzUSm
8Mkq44Z/hJWIKN+Laeee8XsNONsa+buMCz6C7bHxDgZ45qDVm2ulgHjSClXgPEGM
jm7R7hTIvP67WauPTYU87obgbBSfKNz4ezRAPm6vpmWGI+dDedcn7hBsN9M0He+4
/msaYSaDzqpqC2NqArQf9M4yblb3zV5Qxnmov20CgYBW5ZSVTpAOE3SE+eWTYg9W
WEWGhXaQx2/mpHJI4zhoHbSbl/odeSBWy1Ux58eTKx79f4cPKjlY4l5dnMLH5YOH
Szx8Oua4+qR9VYWJ0YHUz/aXcxC28y4PEbVVuU42Gq0lkBJKXaqIP88dzh+7Z+a2
UAaIQTO8fMyLJds0nNCCdQKBgA8wz3HuUUA5SeKT9lmgAX865uZUBux4OozUtk52
t9XDX2V8USIwMN6pnrvdNR4SsN+EslQwG1UVMK3b5B2Etrwz6gh07tLQy96IS9NC
UKbOJi2YQc8SZEn2BDx39qpC9Q5qGTSq1G7wHL0Em4hwOP4uOlcxk0XbJceK/UtS
4YwZAoGAedAZ2aHKRRPX969fnrrJ5uravgQ1PFyegJLPqx7eIjFmsPw8B5t9N9ql
AgtYiWiY8QnhXnlYyH8jki3gipYQ+hb2sKzKnupvEAhKmiyGq9mgBrooX5aE2u4N
25F2jhuygG0m/O7dsp1/IIy8uORgyVhQCagvDP+J8fh330fLoLI=
-----END RSA PRIVATE KEY-----");
    }
}