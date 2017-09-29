Corrected Text
--------------
*Certification Authority Processing*

   Before issuing a certificate, a compliant CA MUST check for
   publication of a relevant CAA Resource Record set.  If such a record
   set exists, a CA MUST NOT issue a certificate unless the CA
   determines that either (1) the certificate request is consistent with
   the applicable CAA Resource Record set or (2) an exception specified
   in the relevant Certificate Policy or Certification Practices
   Statement applies.

   A certificate request MAY specify more than one domain name and MAY
   specify wildcard domains.  Issuers MUST verify authorization for all
   the domains and wildcard domains specified in the request.

   The search for a CAA record climbs the DNS name tree from the
   specified label up to but not including the DNS root '.'.

   Given a request for a specific domain X, or a request for a wildcard
   domain *.X, the relevant record set R(X) is determined as follows:

   Let CAA(X) be the record set returned in response to performing a CAA
   record query on the label X, P(X) be the DNS label immediately above
   X in the DNS hierarchy, and A(X) be the target of a CNAME or DNAME
   alias record chain specified at the label X.

   o  If CAA(X) is not empty, R(X) = CAA (X), otherwise

   o  If A(X) is not null, and CAA(A(X)) is not empty, then R(X) =
      CAA(A(X)), otherwise

   o  If X is not a top-level domain, then R(X) = R(P(X)), otherwise

   o  R(X) is empty.

  Thus, when a search at node X returns a CNAME record, the CA will
  follow the CNAME record chain to its target. If the target label
  contains a CAA record, it is returned.

  Otherwise, the CA continues the search at
  the parent of node X.

  Note that the search does not include the parent of a target of a
  CNAME record (except when the CNAME points back to its own path).

  To prevent resource exhaustion attacks, CAs SHOULD limit the length of
  CNAME chains that are accepted. However CAs MUST process CNAME
  chains that contain 8 or fewer CNAME records.

  For example, if a certificate is requested for X.Y.Z the issuer will
  search for the relevant CAA record set in the following order:

      X.Y.Z

      Alias (X.Y.Z)

      Y.Z
      Alias (Y.Z)

      Z

      Alias (Z)

      Return Empty